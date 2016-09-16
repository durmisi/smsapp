using System;
using Mitto.SmsApp.Backend.Domain;
using NHibernate;
using NUnit.Framework;

namespace Mitto.SmsApp.Backend.Data.Tests
{
    [TestFixture]
    public class SMSRecordTest : TestBase
    {
        [Test]
        public void can_create_sms_record()
        {

            var country = new Country("262", "49", "Germany", Convert.ToDecimal(0.006));
            base.Session.Save(country);

            var smsRecord = new SMSRecord(country, "The Sender", "+49123456789", "Hello World");
            base.Session.Save(smsRecord);
            base.Session.Flush();


            Assert.IsTrue(smsRecord.Id > 0);
            Assert.Pass();
        }


    }
}