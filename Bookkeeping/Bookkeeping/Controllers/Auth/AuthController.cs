using Bookkeeping.Contracts.DTOs.Auth;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bookkeeping.Controllers.Auth
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [SwaggerOperation(
            Summary = "Регистрация пользователя",
            Description = "Регистрирует пользователя по Email или номеру телефона и отправляет код подтверждения")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error.Message });

            return Ok(new { message = "Пользователь успешно зарегистрирован. Код подтверждения отправлен.", userId = result.Value });
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Аутентификация",
            Description = "Вход пользователя по Username, Email или номеру телефона")]
        [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error.Message });

            return Ok(result.Value); // Возвращаем Access и Refresh токены
        }

        [HttpPost("verify-code")]
        [SwaggerOperation(
            Summary = "Подтверждение аккаунта",
            Description = "Проверка кода, отправленного на почту или телефон")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.VerifyConfirmationCodeAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error.Message });

            return Ok(new { message = "Аккаунт успешно подтвержден! Теперь вы можете войти." });
        }

        [HttpPost("refresh-token")]
        [SwaggerOperation(
            Summary = "Обновление токена",
            Description = "Получение новой пары токенов по Refresh Token")]
        [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return BadRequest("Refresh token не может быть пустым");

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error.Message });

            return Ok(result.Value);
        }
    }
}
