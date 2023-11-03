using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [Display(Name = "مرا بخاطر بسپار ؟")]
        public bool RememberMe { get; set; }
        public string RoleTitle { get; set; }
        public string LoginKey { get; set; }
        public int RoleId { get; set; }
        public string LoginReturnPath { get; set; }
    }
}
