using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;

namespace Mitto.SmsApp.Backend.Data
{
    public class SessionFactoryManager
    {
        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(
                        c => c.FromConnectionStringWithKey("ConnectionString"))
                    .AdoNetBatchSize(50))
                .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(x =>
                {
                    x.SetProperty("generate_statistics", "true");
                    new SchemaUpdate(x).Execute(false, true);
                })
                .BuildSessionFactory();
        }
    }
}