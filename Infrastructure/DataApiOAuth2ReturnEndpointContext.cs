using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace Infrastructure
{
    public class DataApiOAuth2ReturnEndpointContext : ReturnEndpointContext
    {
        public DataApiOAuth2ReturnEndpointContext(
             IOwinContext context,
             AuthenticationTicket ticket)
            : base(context, ticket)
        {
        }
    }
}
