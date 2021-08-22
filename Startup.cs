using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarProject.Startup))]
namespace CarProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
