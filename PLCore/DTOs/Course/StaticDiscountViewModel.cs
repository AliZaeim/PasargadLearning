using Newtonsoft.Json.Serialization;
using PLDataLayer.Entities.Training;
using PLDataLayer.Entities.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PLCore.DTOs.Course
{
    public class StaticDiscountViewModel
    {
        public int Id { get; set; }
        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [MinLength(4, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد!")]
        public string Code { get; set; }
        [Display(Name = "عمومی است؟")]
        public bool IsGeneral { get; set; }
        [RegularExpression(@"^(\d{4})\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "تاریخ نامعتبر است !")]
        [Display(Name = "از تاریخ")]
        public string StartDate { get; set; }
        [Display(Name = "از زمان")]
        [RegularExpression("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "زمان نامعتبر است !")]
        public string StartTime { get; set; }
        [RegularExpression(@"^(\d{4})\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "تاریخ نامعتبر است !")]
        [Display(Name = "تا تاریخ")]
        public string EndDate { get; set; }
        [Display(Name = "تا زمان")]
        [RegularExpression("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "زمان نامعتبر است !")]
        public string EndTime { get; set; }
        [Display(Name = "تعداد کوپن")]
        public int? UsableCount { get; set; }
        [Display(Name = "حداقل مبلغ دوره")]
        public int? SD_MinCourseValue { get; set; }
        [Display(Name = "حداکثر مبلغ دوره")]
        public int? SD_MaxCourseValue { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, 100, ErrorMessage = "درصد باید در محدوده صفر تا 100 باشد !")]
        [RegularExpression(@"[+]?([0-9]*[.])?[0-9]+$", ErrorMessage = "عدد نامعتبر است !")]
        public float SD_Percent { get; set; }
        [Display(Name = "فعال / غیرفعال")]
        public bool SD_IsActive { get; set; }

        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "توضیحات")]
        public string SD_Comment { get; set; }
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public List<int> SelectedUserRoles { get; set; } = new List<int>();
        public List<int> SelectedCourses { get; set; } = new List<int>();
        public List<PLDataLayer.Entities.Training.Course> Courses { get; set; } = new List<PLDataLayer.Entities.Training.Course>();
        public List<CourseGroup> CourseGroups { get; set; } = new List<CourseGroup>();

        public User LoginUser { get; set; }
        public Role LoginRole { get; set; }
        public UserRole LoginUserRole { get; set; }
    }
}
