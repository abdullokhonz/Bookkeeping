using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Contracts.DTOs.Users
{
    public class RegisterUserDto : IValidatableObject
    {
        [Required(ErrorMessage = "Имя пользователя (никнейм) обязательно")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Никнейм должен быть от 3 до 50 символов")]
        public string Username { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Некорректный формат номера телефона")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля — 6 символов")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Подтверждение пароля обязательно")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение не совпадают")]
        public string ConfirmPassword { get; set; } = string.Empty;

        // --- Данные для UserProfile ---
        [Required(ErrorMessage = "Имя обязательно")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилия обязательна")]
        public string LastName { get; set; } = string.Empty;

        // --- Флаги ---
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Необходимо согласие на обработку персональных данных")]
        public bool IsPersonalDataAccepted { get; set; }

        // Умная валидация: проверяем, что хотя бы один метод связи указан
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(PhoneNumber))
            {
                yield return new ValidationResult(
                    "Необходимо указать либо Email, либо номер телефона.",
                    new[] { nameof(Email), nameof(PhoneNumber) });
            }
        }
    }
}
