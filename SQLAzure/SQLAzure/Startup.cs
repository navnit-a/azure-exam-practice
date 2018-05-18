using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SQLAzure.Startup))]
namespace SQLAzure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
