using PLDataLayer.Entities.SubEntities;
using PLDataLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLDataLayer.Entities.User
{
    public class User
    {
        #region User
        public User()
        {

        }
        [Key]
        public int UserId { get; set; }
        [StringLength(200)]
        [Display(Name = "نام")]
        public string UserFirstName { get; set; }
        [StringLength(200)]
        [Display(Name = "نام خانوادگی")]
        public string UserFamily { get; set; }
        [MaxLength(10)]
        [MinLength(10)]
        [Display(Name = "کد ملی")]
        public string UserNC { get; set; }
        [Display(Name = "کد سازمانی")]
        public string UserOrgCode { get; set; }
        [Display(Name = "تاریخ تولد")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string UserBirthDate { get; set; }
        [StringLength(50)]
        [Display(Name = "نام پدر")]
        public string UserFatherName { get; set; }
        [StringLength(200)]
        [Display(Name = "آدرس")]
        public string UserRestAddress { get; set; }
        [StringLength(200)]
        [Display(Name = "تصویر")]
        public string UserAvatar { get; set; }
        [MaxLength(50)]
        [Display(Name = "تحصیلات")]
        public string LevelOfEducation { get; set; }
        [StringLength(50)]
        [Display(Name = "مدرک تحصیلی")]
        public string EducationFile { get; set; }
        [MaxLength(50)]
        [Display(Name = "دانشگاه")]
        public string UserUniversity { get; set; }
        [Display(Name = "سال فارغ التحصیلی")]
        public int UserYearofGraduataion { get; set; }
        [MaxLength(2000)]
        [Display(Name = "درباره من")]
        public string UserBiography { get; set; }
        /// <summary>
        /// عنوان مدرس
        /// </summary>
         [StringLength(50)]
         [Display(Name = "عنوان")]
        public string UserLabel { get; set; }
        /// <summary>
        /// توضیحات مدرس
        /// </summary>
        [StringLength(200)]
        [Display(Name = "توضیحات")]
        public string UserDescription { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "جنسیت")]
        public string UserSex { get; set; }
        [StringLength(20)]
        [Display(Name = "تلفن همراه")]
        public string UserCellPhone { get; set; }
        [StringLength(100)]
        public string CellphoneConfirmCode { get; set; }
        public bool UserCellPhoneConfirmed { get; set; }
        [MaxLength(50)]
        [Display(Name = "ایمیل")]
        public string UserEmail { get; set; }
        [MaxLength(50)]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [MaxLength(100)]
        
        public string UserActiveCode { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool UserIsActive { get; set; }
        [MaxLength(50)]
        [Display(Name = "رمز عبور")]
        public string UserPassword { get; set; }
        [StringLength(50)]
        [Display(Name = "آخرین رمز")]
        public string LastPassword { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public DateTime UserRegisteredDate { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name ="تغییر رمز")]
        public bool ChangedPass { get; set; }
        [StringLength(50)]
        [Display(Name = "تصویر کارت ملی")]
        public string UserNCFile { get; set; }
        [StringLength(50)]
        [Display(Name = "تصویر قرارداد سازمانی")]
        public string UserContractFile { get; set; }
        [Display(Name = "کاربری اسکای")]
        public int? Sky_userId { get; set; }
        public int? CountyId { get; set; }

        public int? UserLevel_Id { get; set; }

        
        [Display(Name = "کاربر ایجاد کننده")]
        public string OP_Create { get; set; }
       
        [Display(Name = "کاربر ویرایش کننده")]
        public string OP_Update { get; set; }
        
        [Display(Name = "کاربر حذف کننده")]
        public string OP_Remove { get; set; }
        #endregion User
        #region Relations
        [ForeignKey("CountyId")]
        [Display(Name = "شهرستان")]
        public County County { get; set; }
        [ForeignKey("UserLevel_Id")]
        [Display(Name = "سمت سازمانی")]
        public UserLevel UserLevel { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        #endregion
    }
}
