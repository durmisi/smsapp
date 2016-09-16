using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mitto.SmsApp.Backend.Core.Data.Contracts;
using Mitto.SmsApp.Backend.ServiceInterface;
using Mitto.SmsApp.Backend.ServiceModel;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;

namespace Mitto.SmsApp.Backend.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private readonly ServiceStackHost appHost;

        public UnitTests()
        {
            appHost = new BasicAppHost(typeof(SendSMSService).Assembly)
            {
                ConfigureContainer = container =>
                {
                    //Add your IoC dependencies here
                    container.RegisterAs<CountryRepositoryMock, ICountryRepository>();
                    container.RegisterAs<SMSRecordRepositoryMock, ISMSRecordRepository>();
                    container.RegisterAs<DummySMSSender, ISMSSender>();
                }
            }
            .Init();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            appHost.Dispose();
        }

        [Test]
        public void GetCountries()
        {
            var service = appHost.Container.Resolve<CountryService>();

            var response = (List<CountyResponse>)service.Any(new GetCountries());

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Count, Is.EqualTo(3));
        }

        [Test]
        public void SendSMSService()
        {
            var service = appHost.Container.Resolve<SendSMSService>();

            var response = (SendSMSResponse)service.Any(new SendSMS() { from = "The Sender", text = "Hello World", to = "+49123456789" });

            Assert.That(response, Is.Not.Null);
            Assert.That(response.state, Is.EqualTo(SendSMSResponseType.Success));
            Assert.That(File.Exists(@"C:\temp\sms_records.txt"), Is.True);
        }


        [Test]
        public void GetSentSMS()
        {
            var service = appHost.Container.Resolve<GetSentSMSService>();
            var response = (GetSentSMSResponse)service.Any(new GetSentSMS() { dateTimeFrom = DateTime.UtcNow.AddDays(-10), dateTimeTo = DateTime.UtcNow.Date.AddDays(10), skip = 0, take = 50 });

            Assert.That(response, Is.Not.Null);
            Assert.That(response.totalCount, Is.EqualTo(6));
        }

        [Test]
        public void GetStatistics()
        {
            var service = appHost.Container.Resolve<StatisticsService>();
            var response = (List<StatisticsRecord>)service.Any(new GetStatistics() { dateFrom = DateTime.UtcNow.AddDays(-10), dateTo = DateTime.UtcNow.Date.AddDays(10), mccList = new[] { "262", "232" }.ToList() });

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Count, Is.EqualTo(1));
        }
    }
}