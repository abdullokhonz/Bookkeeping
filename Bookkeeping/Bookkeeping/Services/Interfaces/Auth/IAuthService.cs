using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Auth;
using Bookkeeping.Contracts.DTOs.Users;

namespace Bookkeeping.Services.Interfaces.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <returns>Возвращает Id созданного пользователя (чтобы потом отправить код подтверждения)</returns>
        Task<Result<Guid>> RegisterAsync(RegisterUserDto request);

        /// <summary>
        /// Универсальный вход (по Username, Email или Phone)
        /// </summary>
        /// <returns>Возвращает пару Access и Refresh токенов</returns>
        Task<Result<TokenResponseDto>> LoginAsync(LoginRequestDto request);

        /// <summary>
        /// Подтверждение кода из Email или SMS
        /// </summary>
        Task<Result> VerifyConfirmationCodeAsync(VerifyCodeDto request);

        /// <summary>
        /// Обновление просроченного Access токена с помощью Refresh токена
        /// </summary>
        Task<Result<TokenResponseDto>> RefreshTokenAsync(string refreshToken);
    }
}
