using Mitto.SmsApp.Backend.Data.Repository;
using Mitto.SmsApp.Backend.ServiceModel;
using ServiceStack;
using System;
using System.Linq;
using Mitto.SmsApp.Backend.Core.Data.Contracts;

namespace Mitto.SmsApp.Backend.ServiceInterface
{
    public class GetSentSMSService : Service
    {
        private readonly ISMSRecordRepository _smsRecordRepository;

        public GetSentSMSService(ISMSRecordRepository smsRecordRepository)
        {
            if (smsRecordRepository == null) throw new ArgumentNullException(nameof(smsRecordRepository));
            _smsRecordRepository = smsRecordRepository;
        }

        public object Any(GetSentSMS request)
        {
            int totalCount;
            var smsrecords = _smsRecordRepository.GetAll(request.dateTimeFrom, request.dateTimeTo, request.skip, request.take, out totalCount);
            var items = smsrecords.Select(x => x.MapToSendSMSRecord()).ToList();

            return new GetSentSMSResponse()
            {
                totalCount = totalCount,
                items = items
            };
        }
    }
}