using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Contracts.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Введите логин, почту или номер телефона")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; } = string.Empty;
    }
}
