using Mitto.SmsApp.Backend.ServiceModel;
using ServiceStack;
using System.Linq;
using Mitto.SmsApp.Backend.Core.Data.Contracts;

namespace Mitto.SmsApp.Backend.ServiceInterface
{
    public class StatisticsService : Service
    {
        private readonly ISMSRecordRepository _smsRecordRepository;

        public StatisticsService(ISMSRecordRepository smsRecordRepository)
        {
            _smsRecordRepository = smsRecordRepository;
        }

        public object Any(GetStatistics request)
        {
            var records = _smsRecordRepository.GetAll(request.dateFrom, request.dateTo, request.mccList);

            var result = (from record in records
                          group record by new
                          {
                              record.SentTime.Date,
                              record.country.Mcc,
                              record.country.PricePerSMS,
                          }
                into gr
                          select new StatisticsRecord()
                          {
                              day = gr.Key.Date.ToString("yyyy-MM-dd"),
                              mcc = gr.Key.Mcc,
                              pricePerSMS = gr.Key.PricePerSMS,
                              count = gr.Count(),
                              totalPrice = gr.Count() * gr.Key.PricePerSMS
                          }).ToList();

            return result;
        }
    }
}