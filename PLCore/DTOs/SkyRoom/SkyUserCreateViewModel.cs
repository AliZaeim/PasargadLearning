using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.SkyRoom
{
    public class SkyUserCreateViewModel
    {
        [Display(Name = "نام کاربری")]
        [StringLength(32, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string UserName { get; set; }
        /// <summary>
        /// نام نمایشی
        /// </summary>
        [Display(Name = "نام نمایشی")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string NickName { get; set; }
        /// <summary>
        /// رمز عبور
        /// </summary>
        [Display(Name = "رمز عبور")]
        [StringLength(24, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Password { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        [Display(Name = "ایمیل")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Email { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        [Display(Name = "نام")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string FName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Display(Name = "نام خانوادگی")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string LName { get; set; }
        /// <summary>
        /// کاربر عمومی ؟
        /// </summary>
        [Display(Name = "کاربر عمومی؟")]
        public bool Is_Public { get; set; }
        /// <summary>
        /// وضعیت کاربر
        /// صفر غیر فعال
        /// یک فعال
        /// </summary>
        [Display(Name = "وضعیت کاربر")]
        public int Status { get; set; }

    }
}
