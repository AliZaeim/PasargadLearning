using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLDataLayer.Entities.SubEntities
{
    public class TimelineComponent
    {
        [Key]
        public int TC_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تاریخ")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string TC_Date { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "عنوان")]
        public string TC_Title { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "شرح")]
        public string TC_Text { get; set; }
        [Display(Name = "تصویر")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string TC_Image { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "رتبه نمایش")]
        public int TC_Rank { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }

        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }

        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        public int TL_Id { get; set; }
        #region Relations
        [ForeignKey("TL_Id")]
         [Display(Name = "جدول زمانی")]
        public Timeline Timeline { get; set; }
        #endregion
    }
}
