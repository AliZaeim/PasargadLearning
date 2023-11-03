using PLCore.Security;
using PLDataLayer.Entities.User;
using System.ComponentModel.DataAnnotations;
namespace PLCore.DTOs.Account
{
    public class InitialRegisterViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]        
        [Display(Name = "کد ملی")]
        [MaxLength(10, ErrorMessage = "{0} باید {1} رقم باشد!")]
        [MinLength(10, ErrorMessage = "{0} باید {1} رقم باشد!")]
        public string UserNC { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^(\+98|0098|98|0)?9\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        public string UserCellphone { get; set; }
        public string Key { get; set; }
        public int RoleId { get; set; }
        public bool RegisteredAsStudent { get; set; }
        public bool RegisteredAsTeacher { get; set; }

        public bool IsPaied { get; set; }
        public UserRole UserRole { get; set; }
        public string CId { get; set; }
        public PLDataLayer.Entities.Training.Course Course { get; set; }
    }
}
