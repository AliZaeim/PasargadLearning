using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLCore.Security;
using PLCore.Services.Interfaces;
using PLDataLayer.Entities.Training;
using PLDataLayer.Entities.User;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ITrainingService _trainingService;
        private readonly IUserService _userService;
        public ReportsController(ITrainingService trainingService, IUserService userService)
        {
            _trainingService = trainingService;
            _userService = userService;

        }
        [PermissionChecker(2)]
        public async Task<IActionResult> yCourses()
        {

            int roleId = int.Parse(User.FindFirst("RoleId").Value);
            var userrole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId);
            List<Course> courses = await _trainingService.GetCoursesByRole(userrole.URId);
            return View(courses);
        }
        [PermissionChecker(2)]
        public async Task<IActionResult> CourseUsers(int cid)
        {
            var course = await _trainingService.GetCourseAsync(cid);

            return View(course);
        }
        [PermissionChecker(3)]
        public async Task<IActionResult> yStudents()
        {
            int roleId = int.Parse(User.FindFirst("RoleId").Value);
            var userrole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId);
            List<Course> courses = await _trainingService.GetCoursesByRole(userrole.URId);

            List<UserRole> AlluserRoles = new List<UserRole>();
            foreach (var item in courses)
            {
                List<UserRole> students = await _trainingService.GetCourseUsersByRoleAsync(item.CourseUsers.ToList(), 4, item.Course_Id);
                if (AlluserRoles != null)
                {
                    if (AlluserRoles.Count != 0)
                    {
                        AlluserRoles.AddRange(AlluserRoles.Intersect(students).ToList());
                    }
                    else
                    {
                        AlluserRoles.AddRange(students);
                    }
                }
                
            }
            return View(AlluserRoles.Distinct());
        }
    }
}