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
    }

    public class AccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
    }
}
