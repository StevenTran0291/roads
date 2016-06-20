using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Roads.Startup))]
namespace Roads
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
