using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(prjVendeCarro.Startup))]
namespace prjVendeCarro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
