using Account.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Common
{
    public class AppSettings : IAccessTokenStore
    {
        public string AccessToken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        internal void ClearPasswordCredentials()
        {
            throw new NotImplementedException();
        }
        public Tuple<string,string> GetPasswordCredentials()
        {
            throw new NotImplementedException();
        }
    }
}
