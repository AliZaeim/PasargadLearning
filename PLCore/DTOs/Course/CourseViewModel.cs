using PLDataLayer.Entities.Sale;
using PLDataLayer.Entities.Training;
using PLDataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLCore.DTOs.Course
{
    public class CourseViewModel
    {
        public int Course_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string Course_Title { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "مدت")]
        public string Course_Duration { get; set; }
        [Display(Name = "تاریخ شروع ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.")]

        public string Course_StartDate { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تاریخ پایان")]
        [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.")]
        public string Course_EndDate { get; set; }
        [Display(Name = "ساعت شروع")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Course_StartTime { get; set; }
        [Display(Name = "ساعت پایان")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Course_EndTime { get; set; }
        [Display(Name = "شرح")]
        public string Course_Comment { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "تصویر")]
        public string Course_Image { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تاریخ پایان ثبت نام")]
        [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.")]
        public string Course_EndDateRegistration { get; set; }
        [Display(Name = "ظرفیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "فقط مجاز به وارد کردن عدد می باشید")]
        public string Course_Capacity { get; set; }
        [Display(Name = "شهریه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "فقط مجاز به وارد کردن عدد می باشید")]
        public string Course_Fee { get; set; }

        [Display(Name = "تگها")]
        [StringLength(500, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string Course_Tags { get; set; }
        [Display(Name = "درباره دوره")]
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string Course_about { get; set; }
        [Display(Name = "خلاصه دوره")]
        [StringLength(500, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string Course_abstract { get; set; }
        
        [Display(Name = "لینک آموزش")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CourseLink { get; set; }
        [Display(Name = "کد تخفیف پلکانی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SteppedDiscountCode { get; set; }
        public List<SteppedDiscount> SteppedDisCodes { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int CourseStatus_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "گروه")]
        public int CourseGroup_Id { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "سطح")]
        public int CourseLevel_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "مدرس")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "نوع سنجش")]
        public int CTM_Id { get; set; }

        public List<CourseGroup> CourseGroups { get; set; }
        public List<CourseLevel> CourseLevels { get; set; }
        public List<CourseStatus> CourseStatuses { get; set; }
        
        public List<UserRole> CourseTeachers { get; set; }
        public List<CourseTypeofMeasurment> TypeofMeurements { get; set; }
    }
}
