using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.SubEntities
{
    public class PageInfo
    {
        [Key]
        public int PInfo_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "نام فایل تصویر حداکثر 100 کاراکتر می تواند باشد !")]
        public string PInfo_PageName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "عنوان")]
        public string PInfo_Title { get; set; }        
        [Display(Name = "توضیحات")]
        public string PInfo_Comment { get; set; }
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        
        [Display(Name = "تصویر هدر")]
        public string PInfo_Image { get; set; }
    }
}
