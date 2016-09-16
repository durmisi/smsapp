using Mitto.SmsApp.Backend.Domain;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Mitto.SmsApp.Backend.ServiceModel
{
    [Route("/sms/sent")]
    public class GetSentSMS : IReturn<GetSentSMSResponse>
    {
        public DateTime dateTimeFrom { get; set; }
        public DateTime dateTimeTo { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }

    public class GetSentSMSResponse
    {
        public int totalCount { get; set; }

        public List<SendSmsRecord> items { get; set; }
    }

    public enum SendSmsMessageState
    {
        Success = 1,
        Failed = 0
    }

    public class SendSmsRecord
    {
        public DateTime dateTime { get; set; }
        public string mcc { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public decimal price { get; set; }
        public SendSmsMessageState state { get; set; }
    }

    public static class SMSRecordMapper
    {
        public static SendSmsRecord MapToSendSMSRecord(this SMSRecord smsRecord)
        {
            return new SendSmsRecord()
            {
                dateTime = smsRecord.SentTime,
                from = smsRecord.From,
                to = smsRecord.To,
                mcc = smsRecord.country.Mcc,
                price = smsRecord.country.PricePerSMS,
                state = smsRecord.State ? SendSmsMessageState.Success : SendSmsMessageState.Failed
            };
        }
    }
}