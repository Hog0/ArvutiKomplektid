using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArvutiKomplektid.Startup))]
namespace ArvutiKomplektid
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
