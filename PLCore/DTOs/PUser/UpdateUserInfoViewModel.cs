using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.PUser
{
    public class UpdateUserInfoViewModel
    {
       
        [Display(Name ="تصویر پرسنلی")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Avatar { get; set; }

        public string OldAvatar { get; set; }
        [Display(Name = "سوابق و مهارتها")]
        [StringLength(2000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Biography { get; set; }

        public bool IsSuccess { get; set; }
    }
}
