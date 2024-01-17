using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ext_FraudingSystem.Startup))]
namespace Ext_FraudingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
