using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Client
{
    public class RegisterUser
    {
        public RegisterUser()
        {
        }

        public RegisterUser(string userName, string password,string verifyCode)
            : this(userName, password, password,verifyCode)
        {
        }

        public RegisterUser(string userName, string password, string confirmPassword,string verifyCode)
        {
            UserName = userName;
            Password = password;
            ConfirmPassword = confirmPassword;
            VerifyCode = verifyCode;
        }
        
        [JsonProperty("Phone")]
        public string UserName { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        [JsonProperty("VerifyCode")]
        public string VerifyCode { get; set; }
    }
}
