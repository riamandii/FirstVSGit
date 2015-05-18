using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OneNoteMvcRepo.Startup))]
namespace OneNoteMvcRepo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
