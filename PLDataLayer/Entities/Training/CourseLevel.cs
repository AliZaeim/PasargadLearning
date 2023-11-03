using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.Training
{
    public class CourseLevel
    {
        [Key]
        public int CourseLevel_Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseLevel_Title { get; set; }

        public bool CourseLevel_HasUploadFile { get; set; }
        public bool IsDeleted { get; set; }
        public List<Course> Courses { get; set; }
       
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
        
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        
    }
}
