using Bookkeeping.Services.Interfaces.Notifications;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace Bookkeeping.Services.Implementations.Notifications
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SmsService> _logger;

        public SmsService(IConfiguration configuration, ILogger<SmsService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendSmsAsync(string phoneNumber, string message)
        {
            try
            {
                var dlm = _configuration["SmsSettings:Dlm"];
                var login = _configuration["SmsSettings:Login"];
                var passHash = _configuration["SmsSettings:PassHash"];
                var sender = _configuration["SmsSettings:Sender"];
                var t = _configuration["SmsSettings:T"];

                if (string.IsNullOrEmpty(dlm) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(passHash))
                {
                    _logger.LogError("SMS settings missing");
                    return false;
                }

                var txnId = new Random().Next(100000, 100000000).ToString();

                var hashString = $"{txnId}{dlm}{login}{dlm}{sender}{dlm}{phoneNumber}{dlm}{passHash}";
                var strHash = Sha256Hash(hashString);

                var client = new RestClient("https://api.osonsms.com/sendsms_v1.php");
                var request = new RestRequest();
                request.Method = Method.Get;

                request.AddParameter("from", sender);
                request.AddParameter("login", login);
                request.AddParameter("t", t);
                request.AddParameter("phone_number", phoneNumber);
                request.AddParameter("msg", message);
                request.AddParameter("str_hash", strHash);
                request.AddParameter("txn_id", txnId);

                var response = await client.ExecuteAsync(request);
                var content = response.Content;

                if (string.IsNullOrWhiteSpace(content))
                {
                    _logger.LogWarning("Empty response from SMS service");
                    return false;
                }

                _logger.LogDebug("SMS response: {Content}", content);

                var joResponse = JObject.Parse(content);

                if (joResponse["msg_id"] != null)
                {
                    return true;
                }

                if (joResponse["error"] != null)
                {
                    var errorMsg = joResponse["error"]?["msg"]?.ToString() ?? "Unknown error";
                    _logger.LogWarning("SMS error: {ErrorMsg}", errorMsg);
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while sending SMS");
                return false;
            }
        }

        private string Sha256Hash(string value)
        {
            using var hash = SHA256.Create();
            var result = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sb = new StringBuilder();
            foreach (var b in result)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
