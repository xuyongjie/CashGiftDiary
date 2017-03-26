using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CashGiftDiary.Web.Models
{
    public class UserIdentity : IIdentity
    {
        private string _name;
        private string _authenticationType;
        private bool _isAuthenticated;
        public UserIdentity(string name,string authenticationType,bool isAuthenticated)
        {
            _name = name;
            _authenticationType = authenticationType;
            _isAuthenticated = isAuthenticated;
        }
        public string AuthenticationType
        {
            get
            {
                return _authenticationType;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
