using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CargoMate.Web.Startup))]
namespace CargoMate.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
