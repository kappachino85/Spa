using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Spa.Startup))]
namespace Spa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
