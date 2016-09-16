using System;
using log4net.Config;
using NHibernate;
using NUnit.Framework;

namespace Mitto.SmsApp.Backend.Data.Tests
{
    [SetUpFixture]
    public class TestBase
    {
        private ISession _session;

        [SetUp]
        public void StartUp()
        {
            XmlConfigurator.Configure();

            try
            {
                var sessionFactory = new SessionFactoryManager().CreateSessionFactory();
                _session = sessionFactory.OpenSession();
                _session.Transaction.Begin();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        
        }

        [TearDown]
        public void Cleanup()
        {
            _session.Transaction.Rollback();
        }

        public ISession Session
        {
            get { return _session; }
        }
    }
}