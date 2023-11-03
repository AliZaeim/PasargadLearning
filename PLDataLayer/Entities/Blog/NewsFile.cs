
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLDataLayer.Entities.Blog
{
    public class NewsFile
    {
        [Key]
        public int NF_Id { get; set; }
        
        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "فایل")]
        public string NF_File { get; set; }
        
        [StringLength(300, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "توضیحات")]
        public string NF_Comment { get; set; }
        
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نوع فایل")]
        public string NF_Type { get; set; }
         [Display(Name = "تاریخ ثبت")]
        public DateTime? NF_Date { get; set; }
        public bool IsDeleted { get; set; }
        
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
        
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        public int News_Id { get; set; }
        
        #region Relations
        [ForeignKey("News_Id")]
        [Display(Name = "خبر")]
        public News News { get; set; }
        #endregion
    }
}
