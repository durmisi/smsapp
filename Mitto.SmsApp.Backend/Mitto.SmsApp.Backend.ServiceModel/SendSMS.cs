using ServiceStack;

namespace Mitto.SmsApp.Backend.ServiceModel
{
    [Route("/sms/send")]
    public class SendSMS : IReturn<SendSMSResponse>
    {
        public string from { get; set; }
        public string to { get; set; }
        public string text { get; set; }
    }

    public class SendSMSResponse
    {
        public SendSMSResponseType state { get; set; }
    }

    public enum SendSMSResponseType
    {
        Failed,
        Success
    }
}