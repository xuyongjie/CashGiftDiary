using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Client
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }
        public string HeadImageUrl { get; set; }
        public string LoginProvider { get; set; }
        public bool HasRegistered { get; set; }
    }
}
