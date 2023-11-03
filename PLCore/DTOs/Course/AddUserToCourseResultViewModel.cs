using PLDataLayer.Entities.User;

using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.Course
{
    public class AddUserToCourseResultViewModel
    {
        public bool IsSuccess { get; set; }
        public PLDataLayer.Entities.Training.Course Course { get; set; }
        public UserRole UserRole { get; set; }
    }
}
