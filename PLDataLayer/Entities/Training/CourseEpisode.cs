using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.Training
{
    public class CourseEpisode
    {
        [Key]
        public int CourseEpisode_Id { get; set; }

        public int Course_Id { get; set; }

        [Display(Name = "عنوان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseEpisode_Title { get; set; }

        [Display(Name = "زمان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan CourseEpisode_Time { get; set; }

        [Display(Name = "فایل")]
        public string CourseEpisode_FileName { get; set; }

        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }
        public bool IsDeleted { get; set; }
        
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
       
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        public Course Course { get; set; }
    }
}
