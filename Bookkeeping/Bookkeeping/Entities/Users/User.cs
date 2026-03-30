using Bookkeeping.Contracts.Enums.Users;
using Bookkeeping.Entities.Base;

namespace Bookkeeping.Entities.Users
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public UserType UserType { get; set; } = UserType.Client;

        public UserRole UserRole { get; set; } = UserRole.Guest;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public string ConfirmationCode { get; set; } = string.Empty;

        public bool IsConfirmed { get; set; } = false;

        public bool IsPersonalDataAccepted { get; set; } = false;

        public bool IsBlocked { get; set; } = false;

        public virtual UserProfile? Profile { get; set; }

        public User() { }

        public User(Guid id, string username, string password, string? email = null, string? phoneNumber = null)
        {
            Id = id;
            Username = username;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = HashPassword(password);
            IsConfirmed = false;

            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException("Необходимо указать либо Email, либо номер телефона.");
            }
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
