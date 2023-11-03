using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLCore.DTOs.General;
using PLCore.Services.Interfaces;
using PLDataLayer.Entities.Training;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITrainingService _TService;
        private readonly IUserService _userService;
        public HomeController(ITrainingService trainingService,IUserService userService)
        {
            _TService = trainingService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            // List<CourseUser> courseUsers=await _TService.getcour

            AdminIndexViewModel adminIndexViewModel = new AdminIndexViewModel()
            {
                Courses = await _TService.GetCoursesAsync(),
                AllCourseUsers = await _TService.GetAllCourse_UsersAsync(),
                AllTeachers =  await _userService.GetTeachersAsync(),
                CourseFilesCount = await _TService.GetCourseFilesCount(),
                AllSchools=await _userService.GetSchoolsAsync()
            };
            return View(adminIndexViewModel);
        }
    }
}