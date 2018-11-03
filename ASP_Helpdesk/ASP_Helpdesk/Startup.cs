using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASP_Helpdesk.Startup))]
namespace ASP_Helpdesk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
