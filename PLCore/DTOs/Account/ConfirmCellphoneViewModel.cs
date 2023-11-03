using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.Account
{
    public class ConfirmCellphoneViewModel
    {
        [Display(Name = "کد ملی")]
        public string  UserNC { get; set; }
        [Display(Name = "تلفن همراه")]
        public string UserCellphone { get; set; }
         [Display(Name = "کد اعتبارسنجی")]
         
        public string ConfirmCode { get; set; }
        public string Key { get; set; }
        public string CId { get; set; }
        public int RoleId { get; set; }
    }
}
