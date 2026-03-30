namespace Bookkeeping.Contracts.DTOs.Auth
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}
