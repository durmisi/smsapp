using Mitto.SmsApp.Backend.ServiceModel;
using ServiceStack;
using System.Linq;
using Mitto.SmsApp.Backend.Core.Data.Contracts;

namespace Mitto.SmsApp.Backend.ServiceInterface
{
    public class CountryService : Service
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public object Any(GetCountries request)
        {
            var countries = _countryRepository.GetAll()
                .Select(x => x.ConvertTo<CountyResponse>())
                .ToList();
            return countries;
        }
    }
}