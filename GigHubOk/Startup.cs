using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GigHubOk.Startup))]
namespace GigHubOk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
