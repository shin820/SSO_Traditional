using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthorizationServer.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace AuthorizationServer
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();

            // 有关配置身份验证的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301864
            OAuthAuthorizationServerOptions oAuthOptions = new OAuthAuthorizationServerOptions
            {
                AuthorizeEndpointPath = new PathString("/authorize"),
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthAuthorizationServerProvider
                {
                    OnGrantAuthorizationCode = ctx =>
                    {
                        if (ctx.Ticket != null && ctx.Ticket.Identity != null && ctx.Ticket.Identity.IsAuthenticated)
                        {
                            ctx.Validated();
                        }
                        return Task.FromResult(0);
                    },
                    OnGrantRefreshToken = ctx =>
                    {
                        if (ctx.Ticket != null && ctx.Ticket.Identity != null && ctx.Ticket.Identity.IsAuthenticated)
                        {
                            ctx.Validated();
                        }
                        return Task.FromResult(0);
                    },
                    OnValidateClientRedirectUri = ctx =>
                    {
                        if (ctx.ClientId == "test1")
                        {
                            ctx.Validated();
                            //ctx.Validated("http://website1:30002/Account/CallBack");
                        }

                        if (ctx.ClientId == "test2")
                        {
                            ctx.Validated();
                            //ctx.Validated("http://website2:30003/Account/CallBack");
                        }

                        return Task.FromResult(0);
                    },
                    OnValidateClientAuthentication = ctx =>
                    {
                        string clientId;
                        string clientSecret;
                        if (ctx.TryGetBasicCredentials(out clientId, out clientSecret) ||
                            ctx.TryGetFormCredentials(out clientId, out clientSecret))
                        {
                            if (clientId == "test1" || clientId == "test2")
                            {
                                ctx.Validated();
                            }
                        }
                        return Task.FromResult(0);
                    }
                },
                AuthorizationCodeProvider = new InMemorySingleUseReferenceProvider(),
                AllowInsecureHttp = true
            };

            var bearerOptions = new OAuthBearerAuthenticationOptions
            {
                Provider = new OAuthBearerAuthenticationProvider(),
                AccessTokenProvider = oAuthOptions.AccessTokenProvider,
            };

            app.UseOAuthBearerAuthentication(bearerOptions);
            app.UseOAuthAuthorizationServer(oAuthOptions);

            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Path == oAuthOptions.AuthorizeEndpointPath)
                {
                    ctx.Authentication.SignIn(new AuthenticationProperties(), CreateIdentity("epsilon"));
                }
                //else if (ctx.Request.Path == new PathString("/testpath") && OnTestpathEndpoint != null)
                //{
                //    await OnTestpathEndpoint(ctx);
                //}
                //else if (ctx.Request.Path == new PathString("/me"))
                //{
                //    await MeEndpoint(ctx);
                //}
                else
                {
                    await next();
                }
            });
        }

        private static ClaimsIdentity CreateIdentity(string name, params string[] scopes)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, name)
            };
            foreach (var scope in scopes)
            {
                claims.Add(new Claim("scope", scope));
            }
            return new ClaimsIdentity(
                claims,
                "Bearer");
        }
    }
}