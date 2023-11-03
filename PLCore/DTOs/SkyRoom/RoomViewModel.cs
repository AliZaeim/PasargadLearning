using PLDataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace PLCore.DTOs.SkyRoom
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// نام اتاق
        /// </summary>
        [Display(Name = "نام اتاق")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string name { get; set; }
        /// <summary>
        /// عنوان اتاق
        /// </summary>
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان اتاق")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string title { get; set; }
        /// <summary>
        /// شرح اتاق
        /// </summary>
        [Display(Name = "شرح اتاق")]
        [StringLength(512, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string description { get; set; }
        /// <summary>
        /// وضعیت اتاق
        /// </summary>
        [Display(Name = "وضعیت اتاق")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? status { get; set; }
        /// <summary>
        /// ورود به صورت مهمان
        /// </summary>
        [Display(Name = "ورود به صورت مهمان")]
        public bool guest_login { get; set; }
        /// <summary>
        /// محدودیت تعداد مهمان
        /// 0 نامحدود
        /// </summary>
        [Display(Name = "محدودیت تعداد مهمان")]

        public int? guest_limit { get; set; }
        /// <summary>
        /// ابتدا اپراتور وارد شود؟
        /// </summary>
        [Display(Name = "ابتدا اپراتور وارد شود")]
        public bool op_login_first { get; set; }
        /// <summary>
        /// سقف تعداد کاربران
        /// </summary>
        [Display(Name = "سقف تعداد کاربران")]
        public int? max_users { get; set; }
        /// <summary>
        /// محدودیت طول نشست
        /// </summary>
        [Display(Name = "محدودیت طول نشست")]
        public int? session_duration { get; set; }
        /// <summary>
        /// محدودیت نفر ساعت
        /// </summary>
        [Display(Name = "محدودیت نفر ساعت")]
        public int? time_limit { get; set; }
        /// <summary>
        /// نفر ساعت مصرف شده
        /// </summary>
        [Display(Name = "نفر ساعت مصرف شده")]
        public int time_usage { get; set; }
        /// <summary>
        /// مجموع نفر ساعت مصرف شده
        /// </summary>
        [Display(Name = "مجموع نفر ساعت مصرف شده")]
        public int time_total { get; set; }
        /// <summary>
        /// زمان ایجاد
        /// </summary>
        [Display(Name = "زمان ایجاد")]
        public DateTime? create_time { get; set; }
        /// <summary>
        /// آخرین بروزرسانی
        /// </summary>
        [Display(Name = "آخرین بروزرسانی")]
        public DateTime? update_time { get; set; }

        [Display(Name = "مدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? TeacherurId { get; set; }

        public List<UserRole> Teachers { get; set; } = new List<UserRole>();
    }
}

