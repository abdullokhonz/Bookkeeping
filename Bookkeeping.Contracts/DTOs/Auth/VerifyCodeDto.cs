namespace Bookkeeping.Contracts.DTOs.Auth
{
    public class VerifyCodeDto
    {
        public string Identifier { get; set; } = string.Empty; // Email или Телефон

        public string Code { get; set; } = string.Empty;
    }
}
