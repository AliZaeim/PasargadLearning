using PLDataLayer.Entities.Training;
using PLDataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.General
{
    public class AdminIndexViewModel
    {
        public List<PLDataLayer.Entities.Training.Course> Courses { get; set; }
        public List<CourseUser> AllCourseUsers { get; set; }
        public List<UserRole> AllTeachers { get; set; }
        public List<UserRole> AllSchools { get; set; }
        public int SumCourseFeeGet { get; set; }
        public int CourseFilesCount { get; set; }
    }
}
