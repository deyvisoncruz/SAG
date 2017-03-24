using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nerdtime.Startup))]
namespace nerdtime
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
