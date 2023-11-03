using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLDataLayer.Entities.Training
{
    public class CourseGroup
    {
        [Key]
        public int CourseGroup_Id { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "عنوان")]
        public string CourseGroup_Title { get; set; }
        [MaxLength(100, ErrorMessage = "نام فایل تصویر حداکثر 100 کاراکتر می تواند باشد !")]
        [Display(Name = "تصویر")]
        public string CourseGroup_Image { get; set; }
        [Display(Name = "توضیحات")]
        public string CourseGroup_Comment { get; set; }
        [Display(Name = "حذف شده ؟")]
        public bool IsDeleted { get; set; }
       
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
        
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        [Display(Name = "گروه")]
        public int? ParentId { get; set; }
        #region Relations
        [ForeignKey("ParentId")]
        public List<CourseGroup> CourseGroups { get; set; }
        public List<Course> Courses { get; set; }
        #endregion

    }
}
