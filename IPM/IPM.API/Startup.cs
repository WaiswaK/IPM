using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IPM.API.Startup))]
namespace IPM.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
