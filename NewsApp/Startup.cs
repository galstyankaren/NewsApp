using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsApp.Startup))]
namespace NewsApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
