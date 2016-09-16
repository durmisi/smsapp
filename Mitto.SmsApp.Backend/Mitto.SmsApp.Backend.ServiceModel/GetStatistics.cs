using ServiceStack;
using System;
using System.Collections.Generic;

namespace Mitto.SmsApp.Backend.ServiceModel
{
    [Route("/statistics")]
    public class GetStatistics : IReturn<List<StatisticsRecord>>
    {
        public DateTime dateFrom { get; set; }

        public DateTime dateTo { get; set; }

        public List<string> mccList { get; set; }
    }

    public class StatisticsRecord
    {
        public string day { get; set; }

        public string mcc { get; set; }

        public decimal pricePerSMS { get; set; }

        public int count { get; set; }

        public decimal totalPrice { get; set; }
    }
}