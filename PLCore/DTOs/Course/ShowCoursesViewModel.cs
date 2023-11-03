using PLDataLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.Course
{
    public class ShowCoursesViewModel
    {
        public IList<PLDataLayer.Entities.Training.Course> Courses { get; set; }
        public CourseGroup CourseGroup { get; set; }
        public CourseGroup ParentCourseGroup { get; set; }
        public CourseGroup ParentofParentCourseGroup { get; set; }
        public bool SearchMode { get; set; }
        public string SerachText { get; set; }
    }
}
