using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bookkeeping.Server.Infrastructure.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "BookkeepingServer"; // Изменили имя
        public const string AUDIENCE = "BookkeepingClient";

        // В реальном проекте это значение будет приходить из конфигурации
        public string Key { get; set; } = "SUPER_SECRET_KEY_2026_DO_NOT_SHARE";

        public int LifetimeMinutes { get; set; } = 15;

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
