using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public string RetailterId { get; set; }
        public string Atype { get; set; }
        public string PropertyId { get { return "63018"; } }
        public string Phone { get { return "312-606-3876"; } }
        public string Email { get { return "test@sms-assist.com"; } }
        public string FirstName { get { return "FirstName"; } }
        public string LastName { get { return "LastName"; } }
    }

    public class AccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
    }
}
