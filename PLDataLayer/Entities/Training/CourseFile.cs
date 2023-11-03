using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLDataLayer.Entities.Training
{
    public class CourseFile
    {
        [Key]
        public int CF_Id { get; set; }

        [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "فایل")]
        public string CF_File { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = "نام فایل تصویر حداکثر 100 کاراکتر می تواند باشد !")]
        public string CF_Title { get; set; }

        [StringLength(300, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "توضیحات")]
        public string CF_Comment { get; set; }

        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نوع فایل")]
        public string CF_Type { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? CF_Date { get; set; }
        [Display(Name = "حذف شود؟")]
        public bool IsDeleted { get; set; }
        [Display(Name = "نوع دسترسی")]
        public bool CF_IsFree { get; set; }
        [Display(Name ="تعداد دانلود")]
        public int CF_Download { get; set; }
        [Display(Name ="تلفن دانلود کننده ها")]
        public string CF_DownloadersPhone { get; set; }
        [Display(Name = "ایمیل دانلود کننده ها")]
        public string CF_DownloadersEmail { get; set; }
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
       
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
       
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "دوره")]
        public int Course_Id { get; set; }

        public IEnumerable<string> DownPhoneList
        {
            get { return (CF_DownloadersPhone ?? string.Empty).Split(Environment.NewLine); }
        }
        public IEnumerable<string> DownEmailList
        {
            get { return (CF_DownloadersEmail ?? string.Empty).Split(Environment.NewLine); }
        }

        #region Relations
        [ForeignKey("Course_Id")]
        [Display(Name = "دوره")]
        public Course Course { get; set; }
        #endregion

    }
}
