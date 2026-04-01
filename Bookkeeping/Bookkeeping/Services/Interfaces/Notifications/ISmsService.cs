namespace Bookkeeping.Services.Interfaces.Notifications
{
    public interface ISmsService
    {
        Task<bool> SendSmsAsync(string phoneNumber, string message);
    }
}
