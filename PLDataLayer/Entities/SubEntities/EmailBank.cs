using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.SubEntities
{
    public class EmailBank
    {
        [Key]
        public int EBId { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$", ErrorMessage = "ایمیل معتبر نمی باشد!")]
        public string EBEmail { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegisterDate { get; set; }
    }
}
