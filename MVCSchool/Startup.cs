using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCSchool.Startup))]
namespace MVCSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
