using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PLDataLayer.Entities.Training
{
    public class CourseUser
    {
        [Key]
        public int CU_Id { get; set; }
        [Display(Name = "دوره")]
        public int Course_Id { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime CU_CreateDate { get; set; }
        [Display(Name = "کد تخفیف")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string StaticDiscountCode { get; set; }
        [Display(Name = "حذف شده؟")]
        public bool IsDeleted { get; set; }
        [StringLength(50)]
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
        [StringLength(50)]
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        [StringLength(50)]
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "پرداختی")]
        public int PayValue { get; set; }
        [Display(Name = "تاریخ پرداخت")]
        public DateTime? PayDate { get; set; }
        [Display(Name = "تخفیف ثابت")]
        public int DisValue { get; set; }
        [Display(Name = "کاربر")]
        public int URId { get; set; }
        #region Relations
        [ForeignKey("Course_Id")]
        [Display(Name = "دوره")]
        public Course Course { get; set; }
        [ForeignKey("URId")]
        [Display(Name = "کاربر")]
        public User.UserRole UserRole { get; set; }
        #endregion
    }
}
