using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBaoTanNgheThuat.Startup))]
namespace WebBaoTanNgheThuat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
