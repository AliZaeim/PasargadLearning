using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.SkyRooms
{
    public class SkyUserViewModel
    {
        public int user_id { get; set; }
        /// <summary>
        /// نام کاربری
        /// </summary>
        [Display(Name = "نام کاربری")]
        [StringLength(32, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string username { get; set; }
        /// <summary>
        /// نام نمایشی
        /// </summary>
        [Display(Name = "نام نمایشی")]
        [StringLength(128, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string nickname { get; set; }
        public int access { get; set; }
    }
}
