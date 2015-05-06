using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DataApiOAuth2AuthenticationProvider : IDataApiOAuth2AuthenticationProvider
    {
        public DataApiOAuth2AuthenticationProvider()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
            OnReturnEndpoint = context => Task.FromResult<object>(null);
            OnApplyRedirect = context =>
                context.Response.Redirect(context.RedirectUri);
        }

        public Func<DataApiOAuth2AuthenticatedContext, Task> OnAuthenticated { get; set; }
        public Func<DataApiOAuth2ReturnEndpointContext, Task> OnReturnEndpoint { get; set; }

        public Action<DataApiOAuth2ApplyRedirectContext> OnApplyRedirect { get; set; }

        public virtual Task Authenticated(DataApiOAuth2AuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }

        public virtual Task ReturnEndpoint(DataApiOAuth2ReturnEndpointContext context)
        {
            return OnReturnEndpoint(context);
        }

        public virtual void ApplyRedirect(DataApiOAuth2ApplyRedirectContext context)
        {
            OnApplyRedirect(context);
        }
    }
}
