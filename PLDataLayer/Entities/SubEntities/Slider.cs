using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.SubEntities
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }

        [MaxLength(100, ErrorMessage = "نام فایل تصویر حداکثر 100 کاراکتر می تواند باشد !")]
        [Display(Name = "تصویر")]
        public string SliderImage { get; set; }
        
        [MaxLength(200, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [Display(Name = "متن اول")]
        public string SliderText1 { get; set; }
        [MaxLength(100, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [Display(Name = "رنگ متن اول")]
        public string SliderText1Class { get; set; }
       
        [MaxLength(200, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [Display(Name = "متن دوم")]
        public string SliderText2 { get; set; }
        [MaxLength(100, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [Display(Name = "رنگ متن دوم")]
        public string SliderText2Class { get; set; }
        [MaxLength(100, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [Display(Name = "متن دکمه اول")]
        public string SliderButton1Text { get; set; }
        [MaxLength(100, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [Display(Name = "لینک دکمه اول")]
        public string SliderButton1Link { get; set; }
        [MaxLength(100, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [Display(Name = "متن دکمه دوم")]
        public string SliderButton2Text { get; set; }
        [MaxLength(100, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [Display(Name = "لینک دکمه دوم")]
        public string SliderButton2Link { get; set; }
        
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
        
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        public bool IsDeleted { get; set; }
    }
}
