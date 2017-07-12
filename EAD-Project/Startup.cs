using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EAD_Project.Startup))]
namespace EAD_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
