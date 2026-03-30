namespace Bookkeeping.Server.Infrastructure.Notifications.Sms
{
    /// <summary>
    /// Внутренняя модель для формирования запроса к SMS-шлюзу
    /// </summary>
    public class SmsRequest
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
