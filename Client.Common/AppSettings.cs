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
        private static AppSettings _instance;
        private string _accessToken;
        private AppSettings()
        {

        }
        public static AppSettings GetInstance()
        {
            if(_instance==null)
            {
                _instance = new AppSettings();
            }
            return _instance;
        }
        public string AccessToken
        {
            get
            {
                return _accessToken;
            }
            set
            {
                _accessToken = value;
            }
        }

        internal void ClearPasswordCredentials()
        {
            throw new NotImplementedException();
        }
        public Tuple<string,string> GetPasswordCredentials()
        {
            return new Tuple<string, string>("18867101652", "123456");
        }
    }
}
