using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GetLogoGear.Startup))]
namespace GetLogoGear
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
