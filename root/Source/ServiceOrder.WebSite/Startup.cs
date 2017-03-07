using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServiceOrder.WebSite.Startup))]
namespace ServiceOrder.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
