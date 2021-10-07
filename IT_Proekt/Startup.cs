using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IT_Proekt.Startup))]
namespace IT_Proekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
