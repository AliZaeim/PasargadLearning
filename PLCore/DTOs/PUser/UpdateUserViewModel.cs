using PLDataLayer.Entities.SubEntities;
using PLDataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.PUser
{
    public class UpdateUserViewModel
    {
        public int UserId { get; set; }
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نام")]
        public string UserFirstName { get; set; }
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نام خانوادگی")]
        public string UserFamily { get; set; }
        [MaxLength(10, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [MinLength(10, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [Display(Name = "کد ملی")]
        public string UserNC { get; set; }
        [Display(Name = "کد سازمانی")]
        public string UserOrgCode { get; set; }
        [Display(Name = "تاریخ تولد")]
        [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserBirthDate { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نام پدر")]
        public string UserFatherName { get; set; }
        [StringLength(200, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "آدرس")]
        public string UserRestAddress { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "عکس")]
        public string UserAvatar { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "تحصیلات")]
        public string LevelOfEducation { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "تصویر مدرک تحصیلی")]
        public string EducationFile { get; set; }
        [Display(Name = "دانشگاه")]
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        public string UserUniversity { get; set; }
        [Display(Name = "سال فارغ التحصیلی")]
        public int UserYearofGraduataion { get; set; }
        [StringLength(2000, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "سوابق و مهارتها")]
        public string UserBiography { get; set; }
        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserSex { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تلفن همراه")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        public string UserCellPhone { get; set; }

        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "آدرس ایمیل نامعتبر است !")]
        [Display(Name = "ایمیل")]
        public string UserEmail { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(8, ErrorMessage = "{0} باید حداقل {1} رقم باشد!")]
        
        public string UserName { get; set; }
        [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
         [Display(Name = "عنوان مدرس")]
        public string UserLabel { get; set; }
        
         [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد!")]
          [Display(Name = "سِمت مدرس")]
        public string UserDescription { get; set; }
        [Display(Name = "فعال/غیرفعال")]

        public bool UserIsActive { get; set; }
       
        [Display(Name = "استان")]
        public int? StateId { get; set; }
        
        [Display(Name = "شهرستان")]
        public int? CountyId { get; set; }
        [Display(Name = "سمت متقاضی")]
        public int? UserLevel_Id { get; set; }
        public bool ContractUploade { get; set; }
        [Display(Name = "تصویر قرارداد سازمانی")]
        public string UserContractImage { get; set; }
         [Display(Name = "تصویر کارت ملی")]
        public string UserNCImage { get; set; }
        [Display(Name = "شناسه کاربری اسکای")]
        public int? Skyuser_id { get; set; }
        public List<State> States { get; set; }
        public List<County> Counties { get; set; } = new List<County>();
        public List<UserLevel> UserLevels { get; set; }
    }
}
