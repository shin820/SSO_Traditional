using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;

namespace Infrastructure
{
    public class DataApiOAuth2AuthenticatedContext : BaseContext
    {
        public DataApiOAuth2AuthenticatedContext(IOwinContext context, JObject user, string accessToken,
            string refreshToken, string expires)
            : base(context)
        {
            User = user;
            AccessToken = accessToken;
            RefreshToken = refreshToken;

            int expiresValue;
            if (Int32.TryParse(expires, NumberStyles.Integer, CultureInfo.InvariantCulture, out expiresValue))
            {
                ExpiresIn = TimeSpan.FromSeconds(expiresValue);
            }

            Id = TryGetValue(user, "Id");
            Name = TryGetValue(user, "Name");
            GivenName = TryGetValue(user, "Name", "givenName");
            FamilyName = TryGetValue(user, "Name", "familyName");
            Profile = TryGetValue(user, "url");
            Email = TryGetFirstValue(user, "emails", "value"); // TODO:
        }

        public JObject User { get; private set; }

        public string AccessToken { get; private set; }

        public string RefreshToken { get; private set; }

        public TimeSpan? ExpiresIn { get; set; }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }
        public string Profile { get; private set; }

        public string Email { get; private set; }

        public ClaimsIdentity Identity { get; set; }

        public AuthenticationProperties Properties { get; set; }

        private static string TryGetValue(JObject user, string propertyName)
        {
            JToken value;
            return user.TryGetValue(propertyName, out value) ? value.ToString() : null;
        }

        // Get the given subProperty from a property.
        private static string TryGetValue(JObject user, string propertyName, string subProperty)
        {
            JToken value;
            if (user.TryGetValue(propertyName, out value))
            {
                var subObject = JObject.Parse(value.ToString());
                if (subObject != null && subObject.TryGetValue(subProperty, out value))
                {
                    return value.ToString();
                }
            }
            return null;
        }

        // Get the given subProperty from a list property.
        private static string TryGetFirstValue(JObject user, string propertyName, string subProperty)
        {
            JToken value;
            if (user.TryGetValue(propertyName, out value))
            {
                var array = JArray.Parse(value.ToString());
                if (array != null && array.Count > 0)
                {
                    var subObject = JObject.Parse(array.First.ToString());
                    if (subObject != null)
                    {
                        if (subObject.TryGetValue(subProperty, out value))
                        {
                            return value.ToString();
                        }
                    }
                }
            }
            return null;
        }
    }
}
