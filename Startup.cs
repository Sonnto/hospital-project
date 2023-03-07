using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hospital_project.Startup))]
namespace hospital_project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
