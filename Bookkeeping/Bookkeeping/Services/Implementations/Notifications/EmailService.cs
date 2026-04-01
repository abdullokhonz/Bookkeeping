using Bookkeeping.Services.Interfaces.Notifications;
using System.Net;
using System.Net.Mail;

namespace Bookkeeping.Services.Implementations.Notifications
{
    public class EmailService : IEmailService
    {
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly string _password;
        private readonly string _smtpHost;
        private readonly int _smtpPort;

        public EmailService(IConfiguration configuration)
        {
            _fromEmail = configuration["EmailSettings:FromEmail"] ?? string.Empty;
            _fromName = configuration["EmailSettings:FromName"] ?? string.Empty;
            _password = configuration["EmailSettings:EmailPassword"] ?? string.Empty;
            _smtpHost = configuration["EmailSettings:SmtpHost"] ?? string.Empty;
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"] ?? "587");
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var from = new MailAddress(_fromEmail, _fromName);
                var to = new MailAddress(toEmail);

                using var msg = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                using var smtpClient = new SmtpClient(_smtpHost, _smtpPort)
                {
                    Credentials = new NetworkCredential(_fromEmail, _password),
                    EnableSsl = true
                };

                await smtpClient.SendMailAsync(msg);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendConfirmationCodeAsync(string toEmail, string code)
        {
            string subject = "Подтверждение регистрации";
            string body = $@"
            <div style='font-family: Arial, sans-serif; padding: 20px;'>
                <h2>Подтверждение регистрации в Bookkeeping</h2>
                <p>Ваш код подтверждения: <strong style='font-size: 24px; color: #007bff;'>{code}</strong></p>
                <p>Никому не сообщайте этот код.</p>
            </div>";

            return await SendEmailAsync(toEmail, subject, body);
        }
    }
}
