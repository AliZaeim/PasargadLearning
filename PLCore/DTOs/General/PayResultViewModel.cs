using PLDataLayer.Entities.Training;
using PLDataLayer.Entities.User;

namespace PLCore.DTOs.General
{
    public class PayResultViewModel
    {
        public bool IsSuccess { get; set; }
        public int cuId { get; set; }
        public CourseUser RegCourseUser { get; set; }

        public PLDataLayer.Entities.Training.Course Course { get; set; }
        public UserRole RegUserRole { get; set; }
        public UserRole Teacher { get; set; }

        public int? TotalPayValue { get; set; }
    }
}
