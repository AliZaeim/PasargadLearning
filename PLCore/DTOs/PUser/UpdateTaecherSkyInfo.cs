using PLDataLayer.Entities.User;
using System.ComponentModel.DataAnnotations;


namespace PLCore.DTOs.PUser
{
    public class UpdateTaecherSkyInfo
    {
        [Display(Name = "شناسه اتاق اسکای")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Sky_roomId { get; set; }
        [Display(Name = "لینک اتاق")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Sky_roomLink { get; set; }

        public int urId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
