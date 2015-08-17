using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BikeMates.Web.Startup))]
namespace BikeMates.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
