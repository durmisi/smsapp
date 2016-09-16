using Funq;
using Mitto.SmsApp.Backend.Data;
using Mitto.SmsApp.Backend.Data.Repository;
using Mitto.SmsApp.Backend.ServiceInterface;
using Mitto.SmsApp.Backend.ServiceModel;
using NHibernate;
using ServiceStack;
using ServiceStack.Mvc;
using System.Web.Mvc;
using Mitto.SmsApp.Backend.Core.Data.Contracts;

namespace Mitto.SmsApp.Backend
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("Mitto.SmsApp.Backend", typeof(SendSMSService).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                HandlerFactoryPath = "api",
            });
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());

            var sessionFactory = new SessionFactoryManager().CreateSessionFactory();
            base.Container.Register<ISessionFactory>(sessionFactory);

            container.RegisterAutoWiredAs<CountryRepository, ICountryRepository>();
            container.RegisterAutoWiredAs<SMSRecordRepository, ISMSRecordRepository>();

            container.RegisterAutoWiredAs<DummySMSSender, ISMSSender>();

            //Set MVC to use the same Funq IOC as ServiceStack
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
        }
    }
}