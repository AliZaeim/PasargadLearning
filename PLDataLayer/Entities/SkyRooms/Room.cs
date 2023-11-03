using System;
using System.ComponentModel.DataAnnotations;
namespace PLDataLayer.Entities.SkyRooms
{
    public class Room
    {
        public int Id { get; set; }
        /// <summary>
        /// نام اتاق
        /// </summary>
        [Display(Name = "نام اتاق")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Name { get; set; }
        /// <summary>
        /// عنوان اتاق
        /// </summary>
        [Display(Name = "عنوان اتاق")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Title { get; set; }
        /// <summary>
        /// شرح اتاق
        /// </summary>
        [Display(Name = "شرح اتاق")]
        [StringLength(512, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Description { get; set; }
        /// <summary>
        /// وضعیت اتاق
        /// </summary>
        [Display(Name = "وضعیت اتاق")]
        public int Status { get; set; }
        /// <summary>
        /// ورود به صورت مهمان
        /// </summary>
        [Display(Name = "ورود به صورت مهمان")]
        public bool Guest_Login { get; set; }
        /// <summary>
        /// محدودیت تعداد مهمان
        /// 0 نامحدود
        /// </summary>
        [Display(Name = "محدودیت تعداد مهمان")]
        public int Guest_Limit { get; set; }
        /// <summary>
        /// ابتدا اپراتور وارد شود؟
        /// </summary>
        [Display(Name = "ابتدا اپراتور وارد شود")]
        public bool Op_Login_First { get; set; }
        /// <summary>
        /// سقف تعداد کاربران
        /// </summary>
        [Display(Name = "سقف تعداد کاربران")]
        public int Max_Users { get; set; }
        /// <summary>
        /// محدودیت طول نشست
        /// </summary>
        [Display(Name = "محدودیت طول نشست")]
        public int Session_Duration { get; set; }
        /// <summary>
        /// محدودیت نفر ساعت
        /// </summary>
        [Display(Name = "محدودیت نفر ساعت")]
        public int Time_Limit { get; set; }
        /// <summary>
        /// نفر ساعت مصرف شده
        /// </summary>
        [Display(Name = "نفر ساعت مصرف شده")]
        public int Time_Usage { get; set; }
        /// <summary>
        /// مجموع نفر ساعت مصرف شده
        /// </summary>
        [Display(Name = "مجموع نفر ساعت مصرف شده")]
        public int Time_Total { get; set; }
        /// <summary>
        /// زمان ایجاد
        /// </summary>
        [Display(Name = "زمان ایجاد")]
        public DateTime? Create_Time { get; set; }
        /// <summary>
        /// آخرین بروزرسانی
        /// </summary>
        [Display(Name = "آخرین بروزرسانی")]
        public DateTime? Update_Time { get; set; }
    }
}
