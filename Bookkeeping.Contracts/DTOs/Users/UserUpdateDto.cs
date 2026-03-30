using Bookkeeping.Contracts.Enums.Users;
using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Contracts.DTOs.Users
{
    public class UserUpdateDto
    {
        // Данные аккаунта
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Данные профиля
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public UserGender? Gender { get; set; }

        // Поля, которые обычно меняет только админ (или через спец. методы)
        public UserRole? UserRole { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
