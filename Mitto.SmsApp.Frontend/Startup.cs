using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mitto.SmsApp.Frontend.Startup))]
namespace Mitto.SmsApp.Frontend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
