using Newtonsoft.Json;
using System.Collections.Generic;

namespace Entity
{
    public class User : ModelBase
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public List<Activity> Activities { get; set; }
        public List<CashGiftOut> CashGiftOuts { get; set; }
        public void ModifyInfo(User another)
        {
            Email = another.Email??Email;
            NickName = another.NickName??NickName;
            Avatar = another.Avatar??Avatar;
        }
    }
}
