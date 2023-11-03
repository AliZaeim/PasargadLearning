
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PLDataLayer.Entities.Training
{
    public class Course
    {
        [Key]
        public int Course_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string Course_Title { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "مدت")]
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string Course_Duration { get; set; }
        [Display(Name = "تاریخ شروع ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime Course_StartDate { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تاریخ پایان")]
        public DateTime Course_EndDate { get; set; }
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
        public DateTime Course_EndDateRegistration { get; set; }
        [Display(Name = "ظرفیت")]
        public int Course_Capacity { get; set; }
        [Display(Name = "شهریه")]
        public int Course_Fee { get; set; }
        [Display(Name = "درباره دوره")]
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string Course_about { get; set; }
        [Display(Name = "خلاصه دوره")]
        [StringLength(500, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string Course_abstract { get; set; }

        [Display(Name = "تگها")]
        [StringLength(500, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string Course_Tags { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime Course_CreateDate { get; set; }
        [Display(Name = "آخرین تاریخ ویرایش")]
        public DateTime? Course_UpdateDate { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool Course_IsActive { get; set; }
        [Display(Name = "تعداد بازدید")]
        public int Course_Visits { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CourseStatus_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "گروه")]
        public int CourseGroup_Id { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سطح")]
        public int CourseLevel_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نوع سنجش")]
        public int CTM_Id { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        [Display(Name = "لینک آموزش")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string CourseLink { get; set; }
        [Display(Name = "کد تخفیف پلکانی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string SteppedDiscountCode { get; set; }
        #region Relations
        [ForeignKey("CourseGroup_Id")]
        [Display(Name = "گروه")]
        public CourseGroup CourseGroup { get; set; }
        [ForeignKey("CourseStatus_Id")]
        [Display(Name = "وضعیت")]
        public CourseStatus CourseStatus { get; set; }
        [ForeignKey("CourseLevel_Id")]
        [Display(Name = "سطح")]
        public CourseLevel CourseLevel { get; set; }
        [ForeignKey("CTM_Id")]
        [Display(Name = "نوع سنجش")]
        public CourseTypeofMeasurment CourseTypeofMeasurment { get; set; }
        public virtual ICollection<CourseEpisode> CourseEpisodes { get; set; }
        public virtual ICollection<CourseUser> CourseUsers { get; set; }
        public virtual ICollection<CourseFile> CourseFiles { get; set; }
        #endregion
    }
}
