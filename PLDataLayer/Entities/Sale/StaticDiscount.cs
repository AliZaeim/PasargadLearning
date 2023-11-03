using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace PLDataLayer.Entities.Sale
{
    public class StaticDiscount
    {
        [Key]
        public int SD_Id { get; set; }
        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string SD_Code { get; set; }
        [Display(Name = "عمومی است؟")]
        public bool SD_IsGeneral { get; set; }
        [Display(Name = "شروع")]
        public DateTime? SD_StartDate { get; set; }
        [Display(Name = "پایان")]
        public DateTime? SD_EndDate { get; set; }
        [Display(Name = "تعداد کوپن")]
        public int? SD_UsableCount { get; set; }
        [Display(Name = "باقی مانده")]
        public int? SD_Remain { get; set; }
        [Display(Name = "استفاده شده")]
        public int SD_Used { get; set; }
        [Display(Name = "حداقل مبلغ دوره")]   
        public int? SD_MinCourseValue { get; set; }
        [Display(Name = "حداکثر مبلغ دوره")]
        public int? SD_MaxCourseValue { get; set; }
        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("$[\\-\\+]?[0-9]*(\\.[0-9]+)?", ErrorMessage = "عدد نامعتبر می باشد")]
        public float SD_Percent { get; set; }
        [Display(Name = "فعال / غیرفعال")]
        public bool SD_IsActive { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime RegisterDate { get; set; }
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "توضیحات")]
        public string SD_Comment { get; set; }
        [Display(Name = "حذف شده؟")]
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ثبت کننده")]
       
        public int? Op_Creator_urID { get; set; }

        #region Relations
        public virtual ICollection<UserRoleStaticDiscount> UserRoleStaticDiscounts { get; set; }
        public virtual ICollection<CourseStaticDiscount> CourseStaticDiscounts { get; set; }
        #endregion

    }
}
