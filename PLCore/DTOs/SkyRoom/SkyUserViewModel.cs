using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.SkyRoom
{
    public class SkyUserViewModel
    {
        [Display(Name = "شناسه")]
        public int id { get; set; }
        /// <summary>
        /// نام کاربری
        /// </summary>
        [Display(Name = "نام کاربری")]
        [StringLength(32, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string username { get; set; }
        /// <summary>
        /// نام نمایشی
        /// </summary>
        [Display(Name = "نام نمایشی")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string nickname { get; set; }
        /// <summary>
        /// دسترسی به اتاق
        /// 1- کاربر عادی
        /// 2- ارایه کننده
        /// 3- اپراتور
        /// 4- مدیر
        /// </summary>
        [Display(Name = "وضعیت کاربر")]
        public int status { get; set; }
    }
}
