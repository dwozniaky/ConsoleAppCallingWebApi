using DotNetFrameworkWebApi;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace DotNetFrameworkWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Invoke the ConfigureAuth method, which will set up
            // the OWIN authentication pipeline using OAuth 2.0

            ConfigureAuth(app);
        }
    }
}
