using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashGiftDiary.Web.Models
{
    public class RegisterModel
    {
        [Required]
        [RegularExpression("1[0-9]{10}",ErrorMessage ="手机号格式错误")]
        public string Phone { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="密码长度最少为6位")]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="两次密码不一致")]
        public string PasswordConfirm { get; set; }
        [Required]
        public string VerifyCode { get; set; }
    }
}
