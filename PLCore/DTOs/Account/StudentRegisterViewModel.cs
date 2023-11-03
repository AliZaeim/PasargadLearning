using PLDataLayer.Entities.SubEntities;
using PLDataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PLCore.DTOs.Account
{
    public class StudentRegisterViewModel
    {
        [Display(Name = "مدرس مرکز هستم")]
        public bool UserIsTeacher { get; set; }

        public bool UserIsStudent { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام")]
        public string UserFirstName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام خانوادگی")]
        public string UserFamily { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کد ملی")]
        public string UserNC { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "سال")]

        public int UserBirthDateYear { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "ماه")]

        public int UserBirthDateMouth { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "روز")]
        public int UserBirthDateDay { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "جنسیت")]
        public string UserSex { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        public string UserCellphone { get; set; }
        [Display(Name = "تصویر کارت ملی")]
        public string UserNcImage { get; set; }
        [Display(Name = "تصویر قرارداد سازمانی")]

        public string UserContractImage { get; set; }
        public bool ContractUploade { get; set; }
        //[Display(Name = "کد سازمانی")]
        //public string UserOrgCode { get; set; }
        [Display(Name = "میزان تحصیلات")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public string UserEduLevel { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "استان")]
        public int? StateId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "شهرستان")]
        public int? CountyId { get; set; }

        public int RoleId { get; set; }
        public string Key { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "سمت متقاضی")]
        public int UserLevelId { get; set; }

        public List<State> States { get; set; }
        public List<County> Counties { get; set; }
        public List<UserLevel> UserLevels { get; set; }

        public bool IsConfirmed { get; set; }

        public string CId { get; set; }

    }
}
