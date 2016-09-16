using Mitto.SmsApp.Backend.Domain;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using Mitto.SmsApp.Backend.Core.Data.Contracts;

namespace Mitto.SmsApp.Backend.Data.Repository
{
    public class CountryRepository : ICountryRepository
    {
        public ISessionFactory NHSessionFactory { get; set; }

        public CountryRepository(ISessionFactory sessionFactory)
        {
            NHSessionFactory = sessionFactory;
        }

        public IEnumerable<Country> GetAll()
        {
            using (var session = NHSessionFactory.OpenSession())
            {
                var result = session.Query<Country>().ToList();

                return result;
            }
        }

        public Country GetCountryByCode(string cc)
        {
            using (var session = NHSessionFactory.OpenSession())
            {
                var result = session.Query<Country>().FirstOrDefault(x => x.Cc == cc);
                return result;
            }
        }
    }
}