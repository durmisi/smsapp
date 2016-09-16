using Mitto.SmsApp.Backend.Core.Data.Contracts;
using Mitto.SmsApp.Backend.Domain;
using Mitto.SmsApp.Backend.ServiceModel;
using ServiceStack;

namespace Mitto.SmsApp.Backend.ServiceInterface
{
    public class SendSMSService : Service
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ISMSRecordRepository _smsRecordRepository;
        private readonly ISMSSender _smSender;

        public SendSMSService(ICountryRepository countryRepository, ISMSRecordRepository smsRecordRepository,
            ISMSSender smSender)
        {
            _countryRepository = countryRepository;
            _smsRecordRepository = smsRecordRepository;
            _smSender = smSender;
        }

        public object Any(SendSMS request)
        {
            var cc = request.to.Substring(1, 2);
            var country = _countryRepository.GetCountryByCode(cc);

            var smsRecord = new SMSRecord(country, request.from, request.to, request.text);
            var result = _smSender.SendSMS(smsRecord);

            smsRecord.SetState(result);
            _smsRecordRepository.Save(smsRecord);

            var response = new SendSMSResponse
            {
                state = result ? SendSMSResponseType.Success : SendSMSResponseType.Failed
            };
            return response;
        }
    }
}