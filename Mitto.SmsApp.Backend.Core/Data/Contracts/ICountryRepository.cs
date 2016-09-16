using System.Collections.Generic;
using Mitto.SmsApp.Backend.Domain;

namespace Mitto.SmsApp.Backend.Core.Data.Contracts
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAll();

        Country GetCountryByCode(string cc);
    }
}