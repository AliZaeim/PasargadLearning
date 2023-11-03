
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PLDataLayer.Entities.Training
{
    public class CourseStatus
    {
        [Key]
        public int CourseStatus_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string CourseStatus_Title { get; set; }
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        public bool IsDeleted { get; set; }
        #region Relatoins
        public List<Course> Courses { get; set; }
        #endregion
    }
}
