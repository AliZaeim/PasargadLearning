using PLDataLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PLDataLayer.Entities.User
{
    public class UserRole
    {
        public UserRole()
        {

        }
        [Key]
        public int URId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
        public int UserRoleCode { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }
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

        
        [Display(Name = "لینک اتاق")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string RoomLink { get; set; }
        [Display(Name = "شناسه اتاق اسکای")]
        public int? room_id { get; set; }
       

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public virtual ICollection<CourseUser> CourseUsers { get; set; }
        public virtual ICollection<UserMessage> UserMessages { get; set; }

        #endregion
    }
}
