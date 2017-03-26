using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashGiftDiary.Web.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string FormerPassword { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "密码长度最少为6位")]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword")]
        public string NewPasswordConfirm { get; set; }
    }
}
