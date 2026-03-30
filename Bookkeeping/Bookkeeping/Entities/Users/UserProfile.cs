using Bookkeeping.Contracts.Enums.Users;
using Bookkeeping.Entities.Base;

namespace Bookkeeping.Entities.Users
{
    public class UserProfile : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public UserGender Gender { get; set; } = UserGender.Unknown;

        public string? Location { get; set; }
    }
}
