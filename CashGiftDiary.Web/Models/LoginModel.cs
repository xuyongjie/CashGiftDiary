using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashGiftDiary.Web.Models
{
    public class LoginModel
    {
        [Required]
        [RegularExpression("1[0-9]{10}",ErrorMessage ="手机号格式错误")]
        public string Phone { get; set; }
        [Required(ErrorMessage ="密码不能为空")]
        public string Password { get; set; }
    }
}
