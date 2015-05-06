using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace Infrastructure
{
    public static class DataApiAuthenticationExtensions
    {

        public static IAppBuilder UseDataApiAuthentication(this IAppBuilder app, DataApiOAuth2AuthenticationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            app.Use(typeof(DataApiOAuth2AuthenticationMiddleware), app, options);
            return app;
        }

        public static IAppBuilder UseDataApiAuthentication(
            this IAppBuilder app,
            string clientId,
            string clientSecret)
        {
            return UseDataApiAuthentication(
                app,
                new DataApiOAuth2AuthenticationOptions
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                });
        }
    }
}
