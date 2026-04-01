using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Auth;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;
using Bookkeeping.Infrastructure.Auth;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Services.Interfaces.Auth;
using Bookkeeping.Services.Interfaces.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bookkeeping.Services.Implementations.Auth
{
    public class AuthService : IAuthService
    {
        private readonly PostgreSQLDbContext _context;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly AuthOptions _authOptions;

        public AuthService(
            PostgreSQLDbContext context,
            IEmailService emailService,
            ISmsService smsService,
            IOptions<AuthOptions> authOptions)
        {
            _context = context;
            _emailService = emailService;
            _smsService = smsService;
            _authOptions = authOptions.Value;
        }

        public async Task<Result<Guid>> RegisterAsync(RegisterUserDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return Result<Guid>.Failure(DomainErrors.General.AlreadyExists("User", request.Username));

            if (!string.IsNullOrEmpty(request.Email) && await _context.Users.AnyAsync(u => u.Email == request.Email))
                return Result<Guid>.Failure(DomainErrors.General.AlreadyExists("Email", request.Email));

            var user = new User(
                Guid.NewGuid(),
                request.Username,
                request.Password,
                request.Email,
                request.PhoneNumber
            )
            {
                IsPersonalDataAccepted = request.IsPersonalDataAccepted,
                ConfirmationCode = new Random().Next(10000, 99999).ToString()
            };

            user.Profile = new UserProfile
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _context.Users.Add(user);
            var saved = await _context.SaveChangesAsync() > 0;

            if (!saved) return Result<Guid>.Failure(DomainErrors.General.UpdateFailed);

            await SendCodeAsync(user);

            return Result<Guid>.Success(user.Id);
        }

        public async Task<Result<TokenResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Login
                                     || u.Email == request.Login
                                     || u.PhoneNumber == request.Login);

            if (user == null)
                return Result<TokenResponseDto>.Failure(DomainErrors.Auth.InvalidCredentials);

            if (!User.VerifyPassword(request.Password, user.PasswordHash))
                return Result<TokenResponseDto>.Failure(DomainErrors.Auth.InvalidCredentials);

            if (!user.IsConfirmed) return Result<TokenResponseDto>.Failure(DomainErrors.Auth.NotConfirmed);
            if (user.IsBlocked) return Result<TokenResponseDto>.Failure(DomainErrors.Auth.Blocked);

            return await GenerateTokensAsync(user);
        }

        public async Task<Result> VerifyConfirmationCodeAsync(VerifyCodeDto request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Identifier || u.PhoneNumber == request.Identifier);

            if (user == null || user.ConfirmationCode != request.Code)
                return Result.Failure(DomainErrors.Auth.InvalidConfirmationCode);

            user.IsConfirmed = true;
            user.ConfirmationCode = string.Empty;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<TokenResponseDto>> RefreshTokenAsync(string refreshToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return Result<TokenResponseDto>.Failure(DomainErrors.Auth.InvalidToken);

            return await GenerateTokensAsync(user);
        }

        private async Task<Result<TokenResponseDto>> GenerateTokensAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            var key = _authOptions.GetSymmetricSecurityKey();
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_authOptions.LifetimeMinutes),
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid().ToString();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id
            };
        }

        private async Task SendCodeAsync(User user)
        {
            if (!string.IsNullOrEmpty(user.Email))
            {
                await _emailService.SendConfirmationCodeAsync(user.Email, user.ConfirmationCode);
            }
            else if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                await _smsService.SendSmsAsync(user.PhoneNumber, $"Ваш код подтверждения: {user.ConfirmationCode}");
            }
        }
    }
}
