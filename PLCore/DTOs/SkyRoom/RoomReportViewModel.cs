using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.SkyRoom
{
    public class RoomReportViewModel
    {
        [Display(Name = "شناسه اتاق")]
        public int room_id { get; set; }
        /// <summary>
        /// نام اتاق
        /// </summary>
        [Display(Name = "نام اتاق")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string name { get; set; }
        /// <summary>
        /// عنوان اتاق
        /// </summary>
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان اتاق")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string title { get; set; }
        /// <summary>
        /// شرح اتاق
        /// </summary>
        [Display(Name = "شرح اتاق")]
        [StringLength(512, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string description { get; set; }
        /// <summary>
        /// وضعیت اتاق
        /// </summary>
        [Display(Name = "دسترسی")]
        
        public int? access { get; set; }
    }
}
