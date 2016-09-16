using System;
using Mitto.SmsApp.Backend.Domain;
using NHibernate;
using NUnit.Framework;

namespace Mitto.SmsApp.Backend.Data.Tests
{
    [TestFixture]
    public class CountryTest : TestBase
    {
        [Test]
        public void can_create_country()
        {

            var country = new Country("262", "49", "Germany", Convert.ToDecimal(0.006));
            base.Session.Save(country);
            base.Session.Flush();


            Assert.IsTrue(country.Id > 0);
            Assert.Pass();
        }


    }
}