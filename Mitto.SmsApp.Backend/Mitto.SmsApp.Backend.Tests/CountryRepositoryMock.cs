using System;
using System.Collections.Generic;
using System.Linq;
using Mitto.SmsApp.Backend.Core.Data.Contracts;
using Mitto.SmsApp.Backend.Domain;

namespace Mitto.SmsApp.Backend.Tests
{
    public class CountryRepositoryMock : ICountryRepository
    {
        public IEnumerable<Country> GetAll()
        {
            return new Country[]
            {
                new Country("Germany", "49", "262", Convert.ToDecimal(0.055)),
                new Country("Austria", "43", "232", Convert.ToDecimal(0.053)),
                new Country("Poland", "48", "260", Convert.ToDecimal(0.032)),
            };
        }

        public Country GetCountryByCode(string cc)
        {
            return GetAll().FirstOrDefault(x => x.Cc == cc);
        }
    }
}