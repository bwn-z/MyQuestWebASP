using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyQuestWebASP.Startup))]
namespace MyQuestWebASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
