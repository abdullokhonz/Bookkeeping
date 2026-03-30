using Bookkeeping.Contracts.Enums.Users;

namespace Bookkeeping.Contracts.DTOs.Users
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public UserType UserType { get; set; }
        public UserRole UserRole { get; set; }

        // Статусы
        public bool IsConfirmed { get; set; }
        public bool IsBlocked { get; set; }

        // Данные из профиля (разворачиваем для удобства фронтенда)
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? Description { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public UserGender Gender { get; set; }
        public string? Location { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
