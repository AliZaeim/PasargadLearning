using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLDataLayer.Entities.User
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Display(Name = "نام نقش")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RoleName { get; set; }
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string RoleTitle { get; set; }
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
        #region Relations
        public virtual ICollection<UserRole> UserRoles { get; set; }
        #endregion
    }
}
