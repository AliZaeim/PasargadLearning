using PLDataLayer.Entities.SubEntities;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PLCore.DTOs.Account
{
    public class TeacherRegisterViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام")]
        public string UserFirstName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام خانوادگی")]
        public string UserFamily { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کد ملی")]
        public string UserNC { get; set; }

        [Display(Name = "تصویر کارت ملی")]
        public string UserNcImage { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سال تولد")]
        public int UserBirthDateYear { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ماه تولد")]
        public int UserBirthDateMouth { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "روز تولد")]
        public int UserBirthDateDay { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "جنسیت")]
        public string UserSex { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^(\+98|0098|98|0)?9\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        public string UserCellphone { get; set; }
        [Display(Name = "مدرک تحصیلی")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public string UserEduLevel { get; set; }

        [Display(Name = "تصویر مدرک تحصیلی")]
        public string UserEduImage { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "دانشگاه محل اخذ")]
        public string UserUniversity { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سال فارغ التحصیلی")]
        public string UserEduGraYear { get; set; }
        [Display(Name = "سوابق و مهارتها")]
        public string UserSkills { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "استان")]
        public int? StateId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "شهرستان")]
        public int? CountyId { get; set; }


        public List<State> States { get; set; }
        public List<County> Counties { get; set; }
        public string Key { get; set; }
        public int RoleId { get; set; }
        public bool IsConfirmed { get; set; }

    }
}
