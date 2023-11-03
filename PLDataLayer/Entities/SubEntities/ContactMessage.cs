using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.SubEntities
{
    public class ContactMessage
    {
        [Key]
        public int CM_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام کامل")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string CM_FullName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "موضوع")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string CM_Subject { get; set; }
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress, ErrorMessage = "آدرس ایمیل نامعتبر است !")]
        public string CM_Email { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "پیام")]
        public string CM_Message { get; set; }
        [Display(Name ="تاریخ")]
        public DateTime CM_Date { get; set; }
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }

    }
}
