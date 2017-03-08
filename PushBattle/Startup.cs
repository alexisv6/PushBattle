using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PushBattle.Startup))]
namespace PushBattle
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
