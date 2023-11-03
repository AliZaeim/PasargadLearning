using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.Account
{
    public class RegisterValidationViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کد ملی")]
        [MaxLength(10, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [MinLength(10, ErrorMessage = "{0} باید {1} رقم باشد!")]
        public string UserNC { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        public string UserCellphone { get; set; }
        [Display(Name = "کد اعتبارسنجی")]
        public string ConfirmCode { get; set; }
        public string Type { get; set; }
        public int RoleId { get; set; }
        public bool StartValidation { get; set; }
        public bool ValidatationCompleted { get; set; }
    }
}
