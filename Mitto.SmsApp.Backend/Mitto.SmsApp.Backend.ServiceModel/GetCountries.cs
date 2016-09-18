using ServiceStack;
using System.Collections.Generic;

namespace Mitto.SmsApp.Backend.ServiceModel
{
    [Route("/countries")]
    public class GetCountries : IReturn<List<CountyResponse>>
    {
    }

    public class CountyResponse
    {
        public string Mcc { get; set; }
        public string Cc { get; set; }
        public string Name { get; set; }
        public decimal PricePerSMS { get; set; }
    }
}