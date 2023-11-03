using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.SubEntities
{
    public class ContactInfo
    {
        [Key]
        public int CI_Id { get; set; }
        [Display(Name = "تلفن 1")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "{0} باید حداکثر {1} رقم باشد!")]
        public string CI_Phone1 { get; set; }
        [Display(Name = "تلفن 2")]
        [MaxLength(10, ErrorMessage = "{0} باید حداکثر {1} رقم باشد!")]
        public string CI_Phone2 { get; set; }
        [Display(Name = "فاکس 1")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "{0} باید حداکثر {1} رقم باشد!")]
        public string CI_Fax1 { get; set; }
        [Display(Name = "فاکس 2")]
        [MaxLength(10, ErrorMessage = "{0} باید حداکثر {1} رقم باشد!")]
        public string CI_Fax2 { get; set; }
        [Display(Name = "ایمیل 1")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string CI_Email1 { get; set; }
        [Display(Name = "ایمیل 2")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string CI_Email2 { get; set; }
        [Display(Name = "آدرس 1")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string CI_Address1 { get; set; }
        [Display(Name = "آدرس 2")]
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string CI_Address2 { get; set; }
        [Display(Name = "زمان پاسخگویی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string CI_ContactTime { get; set; }
        [Display(Name = "حذف شده؟")]
        public bool IsDeleted { get; set; }
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }

        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }

        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
    }
}
