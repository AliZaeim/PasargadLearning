using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLDataLayer.Entities.User
{
    public class UserMessage
    {
        [Key]
        public int UserMessages_Id { get; set; }
        [Display(Name ="تاریخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime UserMessages_Date { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name ="عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserMessages_Title { get; set; }
        [Display(Name ="سوال")]
        
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string UserMessages_Question { get; set; }
        [Display(Name ="پاسخ")]
        public string UserMessages_Response { get; set; }
        public int SenderId { get; set; }
        public int ReciverId { get; set; }

        public int? ParentId { get; set; }
        #region Relations
        [ForeignKey("ParentId")]
        public UserMessage Parent { get; set; }
        #endregion

    }
}
