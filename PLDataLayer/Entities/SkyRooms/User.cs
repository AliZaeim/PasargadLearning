using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.SkyRooms
{
    public class User
    {
        public int user_id { get; set; }
        /// <summary>
        /// نام کاربری
        /// </summary>
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
        /// جنسیت
        /// صفر نامعلوم، 1 مرد، 2 زن
        /// </summary>
        [Display(Name = "جنسیت")]
        public int Gender { get; set; }
        /// <summary>
        /// وضعیت کاربر
        /// صفر غیرفعال / 1 فعال
        /// </summary>
        [Display(Name = "وضعیت کاربر")]
        public int Status { get; set; }
        /// <summary>
        /// کاربر عمومی ؟
        /// </summary>
        [Display(Name = "کاربر عمومی؟")]
        public bool Is_Public { get; set; }
        /// <summary>
        /// محدودیت استفاده همزمان
        /// صفر نامحدود
        /// </summary>
        [Display(Name = "محدودیت استفاده همزمان")]
        public int Concurrent { get; set; }
        /// <summary>
        /// محدودیت زمانی
        /// ساعت
        /// </summary>
        [Display(Name = "محدودیت زمانی")]
        public int Time_Limit { get; set; }
        /// <summary>
        /// زمان مصرف شده
        /// ساعت
        /// </summary>
        [Display(Name = "زمان مصرف شده")]
        public int Time_Usage { get; set; }
        /// <summary>
        /// مجموع زمان مصرف شده
        /// ساعت
        /// </summary>
        [Display(Name = "مجموع زمان مصرف شده")]
        public int Time_Total { get; set; }
        /// <summary>
        /// تاریخ انقضاء
        /// </summary>
        [Display(Name = "تاریخ انقضاء")]
        public DateTime Expiry_Date { get; set; }
        /// <summary>
        /// زمان ایجاد
        /// </summary>
        [Display(Name = "زمان ایجاد")]
        public DateTime Create_Time { get; set; }
        /// <summary>
        /// آخرین تاریخ بروزرسانی
        /// </summary>
        [Display(Name = "آخرین بروزرسانی")]
        public DateTime Update_Time { get; set; }
    }
}
