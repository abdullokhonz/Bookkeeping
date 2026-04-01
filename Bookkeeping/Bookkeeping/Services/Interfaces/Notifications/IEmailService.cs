namespace Bookkeeping.Services.Interfaces.Notifications
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);

        Task<bool> SendConfirmationCodeAsync(string toEmail, string code);
    }
}
