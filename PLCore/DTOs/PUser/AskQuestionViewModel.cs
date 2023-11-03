using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.PUser
{
    public class AskQuestionViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام کامل")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string SiteFAQ_Name { get; set; }
        
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "ایمیل")]
        [RegularExpression(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$", ErrorMessage = "ایمیل نامعتبر است!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SiteFAQ_Email { get; set; }
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "موضوع سئوال")]
        public string SiteFAQ_Subject { get; set; }

        [StringLength(500, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سئوال")]
        public string SiteFAQ_Question { get; set; }
        public bool IsSuccess { get; set; }

    }
}
