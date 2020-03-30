using System.Configuration;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace DotNetFrameworkWebApi
{
    public partial class Startup
    {
        private static readonly string ClientId = ConfigurationManager.AppSettings["ida:ClientId"]; 
        private static readonly string Tenant = "";
        public void ConfigureAuth(IAppBuilder app)
        {
            // NOTE: The usual WindowsAzureActiveDirectoryBearerAuthentication middleware uses a
            // metadata endpoint which is not supported by the Microsoft identity platform endpoint.  Instead, this 
            // OpenIdConnectSecurityTokenProvider implementation can be used to fetch & use the OpenIdConnect
            // metadata document - which for the identity platform endpoint is https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AccessTokenFormat = new JwtFormat(
                    new TokenValidationParameters
                    {
                        // Check if the audience is intended to be this application
                        ValidAudiences = new [] { ClientId, $"api://{ClientId}" },

                        // Change below to 'true' if you want this Web API to accept tokens issued to one Azure AD tenant only (single-tenant)
                        // Note that this is a simplification for the quickstart here. You should validate the issuer. For details, 
                        // see https://github.com/Azure-Samples/active-directory-dotnet-native-aspnetcore
                        ValidateIssuer = false,

                    },
                    new OpenIdConnectSecurityTokenProvider($"https://login.microsoftonline.com/{Tenant}/v2.0/.well-known/openid-configuration")
                    //for multitenant and work/school accounts replace Tenant with 'common' value
                ),
            });
        }
    }
}
