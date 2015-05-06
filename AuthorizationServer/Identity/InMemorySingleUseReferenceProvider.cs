using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Infrastructure;

namespace AuthorizationServer.Identity
{
    public class InMemorySingleUseReferenceProvider : AuthenticationTokenProvider
    {
        private readonly ConcurrentDictionary<string, string> _database = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        public override void Create(AuthenticationTokenCreateContext context)
        {
            string tokenValue = Guid.NewGuid().ToString("n");

            _database[tokenValue] = context.SerializeTicket();

            context.SetToken(tokenValue);
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_database.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }
    }
}