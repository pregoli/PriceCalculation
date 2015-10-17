using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BroadbandChoices.Web.Startup))]
namespace BroadbandChoices.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
