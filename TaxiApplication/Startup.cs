using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaxiApplication.Startup))]
namespace TaxiApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
