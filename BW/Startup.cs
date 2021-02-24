using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BW.Startup))]
namespace BW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
