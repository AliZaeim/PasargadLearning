using PLDataLayer.Entities.User;

namespace PLCore.DTOs.Account
{
    public class RegisterResultViewModel
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public UserRole UserRole { get; set; }
        public PLDataLayer.Entities.Training.Course Course { get; set; }
        public bool RegInCourseOnly { get; set; }
        public UserRole Teacher { get; set; }

        public int TotalFee { get; set; }

        public int? roomId { get; set; }
    }
}
