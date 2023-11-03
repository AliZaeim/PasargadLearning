using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.User
{
    public class UserLevel
    {
        [Key]
        public int UserLevel_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "سطح کاربر")]
        public string UserLevel_Title { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(50)]
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
        [StringLength(50)]
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        [StringLength(50)]
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        public bool UserLevel_HasUpload { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
