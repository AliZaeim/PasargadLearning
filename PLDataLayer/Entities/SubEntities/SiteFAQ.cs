using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.SubEntities
{
    public class SiteFAQ
    {
        [Key]
        public int SiteFAQ_Id { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime? SiteFAQ_Date { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نــام")]
        public string SiteFAQ_Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "ایمیل")]
        [RegularExpression(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$", ErrorMessage = "ایمیل نامعتبر است!")]
        public string SiteFAQ_Email { get; set; }
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "موضوع")]
        public string SiteFAQ_Subject { get; set; }
        
        [StringLength(500, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "سوال")]
        public string SiteFAQ_Question { get; set; }
        
        [StringLength(2000, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "پاسخ")]
        public string SiteFAQ_Reply { get; set; }
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
       
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
    }
}
