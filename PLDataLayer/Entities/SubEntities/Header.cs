using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.SubEntities
{
    public class Header
    {
        [Key]
        public int Header_id { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Header_Title { get; set; }
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "شعار")]
        
        public string Header_Text { get; set; }
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "درباره سایت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Header_About { get; set; }

        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "متن دکمه اول")]
        public string Header_Btn1Text { get; set; }
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "لینک دکمه اول")]
        public string Header_Btn1Link { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "متن دکمه دوم")]
        public string Header_Btn2Text { get; set; }
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "لینک دکمه دوم")]
        public string Header_Btn2Link { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "عنوان شبکه اجتماعی اول")]
        public string Header_Social1Text { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نوع شبکه اجتماعی اول")]
        public string Header_Social1Class { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "لینک شبکه اجتماعی اول")]
        public string Header_Social1Link { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "عنوان شبکه اجتماعی دوم")]
        public string Header_Social2Text { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نوع شبکه اجتماعی دوم")]
        public string Header_Social2Class { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "لینک شبکه اجتماعی دوم")]
        public string Header_Social2Link { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "عنوان شبکه اجتماعی سوم")]
        public string Header_Social3Text { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نوع شبکه اجتماعی سوم")]
        public string Header_Social3Class { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "لینک شبکه اجتماعی سوم")]
        public string Header_Social3Link { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "عنوان شبکه اجتماعی چهارم")]
        public string Header_Social4Text { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نوع شبکه اجتماعی چهارم")]
        public string Header_Social4Class { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "لینک شبکه اجتماعی چهارم")]
        public string Header_Social4Link { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "عنوان شبکه اجتماعی پنجم")]
        public string Header_Social5Text { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نوع شبکه اجتماعی پنجم")]
        public string Header_Social5Class { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "لینک شبکه اجتماعی پنجم")]
        public string Header_Social5Link { get; set; }
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
