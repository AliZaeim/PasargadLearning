using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLCore.Convertors;
using PLCore.DTOs.Course;
using PLCore.Security;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using PLDataLayer.Entities.Sale;
using PLDataLayer.Entities.User;


namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StaticDiscountsController : Controller
    {
        private readonly ITrainingService _trainingService;
        private readonly IUserService _userService;
        public StaticDiscountsController(ITrainingService trainingService, IUserService userService)
        {
            _trainingService = trainingService;
            _userService = userService;            
        }

        // GET: Admin/StaticDiscounts
        [PermissionChecker(153)]
        public async Task<IActionResult> Index()
        {
            int roleId = int.Parse(User.FindFirst("RoleId").Value.ToString());
            UserRole userRole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId).ConfigureAwait(false);
            List<StaticDiscount> staticDiscounts = await _trainingService.GetStaticDiscountsAsync(userRole.URId).ConfigureAwait(false);
            if (roleId == 3)
            {
                staticDiscounts = staticDiscounts.Where(w => w.Op_Creator_urID == userRole.URId).ToList();
            }
            return View(staticDiscounts);
        }

        // GET: Admin/StaticDiscounts/Details/5
        [PermissionChecker(156)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticDiscount = await _trainingService.GetStaticDiscountByIdAsync((int)id).ConfigureAwait(false);
            if (staticDiscount == null)
            {
                return NotFound();
            }

            return View(staticDiscount);
        }
        [HttpPost]
        [PermissionChecker(157)]
        public async Task<bool> ActDeact(int id)
        {
            StaticDiscount staticdiscount = await _trainingService.GetStaticDiscountByIdAsync(id);
            bool st = false;
            if (staticdiscount.SD_IsActive == false)
            {
                st = true;
            }
            staticdiscount.SD_IsActive = st;
            _trainingService.EditStaticDiscount(staticdiscount);
            await _trainingService.SaveAsync();
            return st;
        }
        // GET: Admin/StaticDiscounts/Create
        [PermissionChecker(154)]
        public async Task<IActionResult> Create()
        {
            User user = await _userService.GetUserByUserName(User.Identity.Name).ConfigureAwait(false);
            if (user == null)
            {
                return NotFound("کاربر لاگین نکرده است !");
            }
            int roleId = int.Parse(User.FindFirst("RoleId").Value);
            Role role = await _userService.GetRoleByIdAsync(roleId).ConfigureAwait(false);
            UserRole LoginUserRole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId).ConfigureAwait(false);
            StaticDiscountViewModel staticDiscountViewModel = new StaticDiscountViewModel
            {
                Courses = await _trainingService.GetCoursesAsync().ConfigureAwait(false),
                CourseGroups = await _trainingService.GetCourseGroupsAsync().ConfigureAwait(false),
                UserRoles = await _userService.GetUserRoles().ConfigureAwait(false),
                Code = PLCore.Generators.GeneratorClass.GenerateKey(await _trainingService.GetStaticDiscountCodesAsync().ConfigureAwait(false), 6),
                IsGeneral = true,
                LoginUser = user,
                LoginRole = role,
                LoginUserRole = LoginUserRole
            };

            staticDiscountViewModel.Courses = staticDiscountViewModel.Courses.Where(w => w.Course_IsActive = true).ToList();
            if (roleId == 3)
            {
                staticDiscountViewModel.Courses = await _trainingService.GetCoursesByRole(LoginUserRole.URId).ConfigureAwait(false);
            }

            //staticDiscountViewModel.SelectedCourses = staticDiscountViewModel.Courses.Select(s => s.Course_Id).ToList();
            staticDiscountViewModel.UserRoles = staticDiscountViewModel.UserRoles.Where(w => w.IsActive == true && w.User.UserIsActive == true && w.RoleId == 4).ToList();
            // staticDiscountViewModel.SelectedUserRoles = staticDiscountViewModel.UserRoles.Select(s => s.URId).ToList();
            return View(staticDiscountViewModel);
        }

        // POST: Admin/StaticDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(154)]
        public async Task<IActionResult> Create(StaticDiscountViewModel staticDiscountViewModel)
        {
            User user = await _userService.GetUserByUserName(User.Identity.Name).ConfigureAwait(false);
            int roleId = int.Parse(User.FindFirst("RoleId").Value);
            Role role = await _userService.GetRoleByIdAsync(roleId).ConfigureAwait(false);
            UserRole LoginUserRole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId).ConfigureAwait(false);
            staticDiscountViewModel.Courses = await _trainingService.GetCoursesAsync().ConfigureAwait(false);

            if (roleId == 3)
            {
                staticDiscountViewModel.Courses = await _trainingService.GetCoursesByRole(LoginUserRole.URId).ConfigureAwait(false);

            }

            staticDiscountViewModel.CourseGroups = await _trainingService.GetCourseGroupsAsync().ConfigureAwait(false);
            staticDiscountViewModel.UserRoles = await _userService.GetUserRoles().ConfigureAwait(false);
            staticDiscountViewModel.Courses = staticDiscountViewModel.Courses.Where(w => w.Course_IsActive = true).ToList();
            staticDiscountViewModel.UserRoles = staticDiscountViewModel.UserRoles.Where(w => w.IsActive == true && w.User.UserIsActive == true && w.RoleId == 4).ToList();
            staticDiscountViewModel.LoginUser = user;
            staticDiscountViewModel.LoginRole = role;
            staticDiscountViewModel.LoginUserRole = LoginUserRole;
            if (!ModelState.IsValid)
            {
                return View(staticDiscountViewModel);
            }

            StaticDiscount staticDiscount = new StaticDiscount()
            {
                SD_Code = staticDiscountViewModel.Code,
                SD_Comment = staticDiscountViewModel.SD_Comment,
                SD_IsActive = staticDiscountViewModel.SD_IsActive,
                SD_IsGeneral = staticDiscountViewModel.IsGeneral,
                SD_MaxCourseValue = staticDiscountViewModel.SD_MaxCourseValue,
                SD_MinCourseValue = staticDiscountViewModel.SD_MinCourseValue,
                SD_Percent = staticDiscountViewModel.SD_Percent,
                SD_UsableCount = staticDiscountViewModel.UsableCount,
                RegisterDate = DateTime.Now,
                Op_Creator_urID = LoginUserRole.URId
               
            };
            if (roleId == 3)
            {
                staticDiscount.SD_IsGeneral = true;
            }
            if (!string.IsNullOrEmpty(staticDiscountViewModel.StartDate))
            {
                if (!string.IsNullOrEmpty(staticDiscountViewModel.StartTime))
                {
                    staticDiscount.SD_StartDate = staticDiscountViewModel.StartDate.ChangeToMiladi(staticDiscountViewModel.StartTime);
                }
                else
                {
                    ModelState.AddModelError("StartTime", "زمان شروع را مشخص کنید !");
                    return View(staticDiscountViewModel);
                }
            }
            if (!string.IsNullOrEmpty(staticDiscountViewModel.EndDate))
            {
                if (!string.IsNullOrEmpty(staticDiscountViewModel.EndTime))
                {
                    staticDiscount.SD_EndDate = staticDiscountViewModel.EndDate.ChangeToMiladi(staticDiscountViewModel.EndTime);
                }
                else
                {
                    ModelState.AddModelError("EndDate", "زمان پایان رو مشخص کنید");
                    return View(staticDiscountViewModel);
                }
            }
            await _trainingService.CreateStaticDiscount(staticDiscount, staticDiscountViewModel.SelectedUserRoles.ToList(), staticDiscountViewModel.SelectedCourses.ToList());
            await _trainingService.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public string GetNewDiscountCode()
        {
            string code = PLCore.Generators.GeneratorClass.GenerateKey(_trainingService.GetStaticDiscountCodes(), 6);
            return code;
        }
        // GET: Admin/StaticDiscounts/Edit/5
        [PermissionChecker(155)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticDiscount = await _trainingService.GetStaticDiscountByIdAsync((int)id);
            if (staticDiscount == null)
            {
                return NotFound();
            }
            User user = await _userService.GetUserByUserName(User.Identity.Name).ConfigureAwait(false);
            int roleId = int.Parse(User.FindFirst("RoleId").Value);
            Role role = await _userService.GetRoleByIdAsync(roleId).ConfigureAwait(false);
            UserRole LoginUserRole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId).ConfigureAwait(false);
            StaticDiscountViewModel staticDiscountViewModel = new StaticDiscountViewModel()
            {
                Id = staticDiscount.SD_Id,
                Courses = await _trainingService.GetCoursesAsync().ConfigureAwait(false),
                CourseGroups = await _trainingService.GetCourseGroupsAsync().ConfigureAwait(false),
                UserRoles = await _userService.GetUserRoles().ConfigureAwait(false),
                Code = staticDiscount.SD_Code,
                IsGeneral = staticDiscount.SD_IsGeneral,
                SD_MinCourseValue = staticDiscount.SD_MinCourseValue,
                SD_MaxCourseValue = staticDiscount.SD_MaxCourseValue,
                UsableCount = staticDiscount.SD_UsableCount,
                SD_Percent = staticDiscount.SD_Percent,
                SD_Comment = staticDiscount.SD_Comment,
                StartDate = staticDiscount.SD_StartDate.ToShamsiN(),
                EndDate = staticDiscount.SD_EndDate.ToShamsiN(),
                SD_IsActive = staticDiscount.SD_IsActive,
                SelectedCourses = staticDiscount.CourseStaticDiscounts.Select(s => s.Course_Id).ToList(),
                SelectedUserRoles = staticDiscount.UserRoleStaticDiscounts.Select(s => s.URId).ToList(),
                LoginUser = user,
                LoginRole = role,
                LoginUserRole = LoginUserRole
            };
            staticDiscountViewModel.Courses = staticDiscountViewModel.Courses.Where(w => w.Course_IsActive = true).ToList();
            staticDiscountViewModel.UserRoles = staticDiscountViewModel.UserRoles.Where(w => w.IsActive == true && w.User.UserIsActive == true && w.RoleId == 4).ToList();
            if (staticDiscount.SD_StartDate.HasValue)
            {
                int sh = staticDiscount.SD_StartDate.Value.Hour;
                int sm = staticDiscount.SD_StartDate.Value.Minute;
                staticDiscountViewModel.StartTime = sh.ToString("00") + ":" + sm.ToString("00");
            }
            if (staticDiscount.SD_EndDate.HasValue)
            {
                int sh = staticDiscount.SD_EndDate.Value.Hour;
                int sm = staticDiscount.SD_EndDate.Value.Minute;
                staticDiscountViewModel.EndTime = sh.ToString("00") + ":" + sm.ToString("00");
            }

            return View(staticDiscountViewModel);
        }

        // POST: Admin/StaticDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(155)]
        public async Task<IActionResult> Edit(StaticDiscountViewModel staticDiscountViewModel)
        {
            User user = await _userService.GetUserByUserName(User.Identity.Name).ConfigureAwait(false);
            int roleId = int.Parse(User.FindFirst("RoleId").Value);
            Role role = await _userService.GetRoleByIdAsync(roleId).ConfigureAwait(false);
            UserRole LoginUserRole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId).ConfigureAwait(false);
            staticDiscountViewModel.Courses = await _trainingService.GetCoursesAsync().ConfigureAwait(false);
            staticDiscountViewModel.CourseGroups = await _trainingService.GetCourseGroupsAsync().ConfigureAwait(false);
            staticDiscountViewModel.UserRoles = await _userService.GetUserRoles().ConfigureAwait(false);
            staticDiscountViewModel.Courses = staticDiscountViewModel.Courses.Where(w => w.Course_IsActive = true).ToList();
            staticDiscountViewModel.UserRoles = staticDiscountViewModel.UserRoles.Where(w => w.IsActive == true && w.User.UserIsActive == true && w.RoleId == 4).ToList();
            staticDiscountViewModel.LoginUser = user;
            staticDiscountViewModel.LoginRole = role;
            staticDiscountViewModel.LoginUserRole = LoginUserRole;
            if (!ModelState.IsValid)
            {
                return View(staticDiscountViewModel);
            }
            if (!staticDiscountViewModel.IsGeneral)
            {
                if (staticDiscountViewModel.SelectedCourses.Count == 0)
                {
                    ModelState.AddModelError("Code", "دوره ای انتخاب نشده است !");
                    return View(staticDiscountViewModel);
                }
                if (staticDiscountViewModel.SelectedUserRoles.Count == 0)
                {
                    ModelState.AddModelError("Code", "کاربری انتخاب نشده است !");
                    return View(staticDiscountViewModel);
                }

            }
            StaticDiscount staticDiscount = await _trainingService.GetStaticDiscountByIdAsync((int)staticDiscountViewModel.Id);
            staticDiscount.SD_Code = staticDiscountViewModel.Code;
            staticDiscount.SD_Comment = staticDiscountViewModel.SD_Comment;
            staticDiscount.SD_IsActive = staticDiscountViewModel.SD_IsActive;
            staticDiscount.SD_IsGeneral = staticDiscountViewModel.IsGeneral;
            staticDiscount.SD_MaxCourseValue = staticDiscountViewModel.SD_MaxCourseValue;
            staticDiscount.SD_MinCourseValue = staticDiscountViewModel.SD_MinCourseValue;
            staticDiscount.SD_Percent = staticDiscountViewModel.SD_Percent;



            int reqUC = 0;

            if (staticDiscountViewModel.UsableCount != null)
            {
                reqUC = staticDiscountViewModel.UsableCount.GetValueOrDefault(0);
                if (reqUC < staticDiscount.SD_Used)
                {
                    ModelState.AddModelError("UsableCount", "تعداد کوپن با تعداد استفاده شده متناسب نیست !");
                    return View(staticDiscountViewModel);
                }
                staticDiscount.SD_UsableCount = reqUC;
            }
            else
            {
                staticDiscount.SD_UsableCount = staticDiscountViewModel.UsableCount;
            }

            if (!string.IsNullOrEmpty(staticDiscountViewModel.StartDate))
            {
                if (!string.IsNullOrEmpty(staticDiscountViewModel.StartTime))
                {
                    staticDiscount.SD_StartDate = staticDiscountViewModel.StartDate.ChangeToMiladi(staticDiscountViewModel.StartTime);
                }
                else
                {
                    ModelState.AddModelError("StartTime", "زمان شروع را مشخص کنید !");
                    return View(staticDiscountViewModel);
                }
            }
            else
            {
                staticDiscount.SD_StartDate = null;
                
            }

            if (!string.IsNullOrEmpty(staticDiscountViewModel.EndDate))
            {
                if (!string.IsNullOrEmpty(staticDiscountViewModel.EndTime))
                {
                    staticDiscount.SD_EndDate = staticDiscountViewModel.EndDate.ChangeToMiladi(staticDiscountViewModel.EndTime);
                }
                else
                {
                    ModelState.AddModelError("EndDate", "زمان پایان رو مشخص کنید");
                    return View(staticDiscountViewModel);
                }

            }
            else
            {
                staticDiscount.SD_EndDate = null;

            }
            if (staticDiscountViewModel.UsableCount != null)
            {
                staticDiscount.SD_UsableCount = (int)staticDiscount.SD_UsableCount;
            }
            _trainingService.EditStaticDiscount(staticDiscount);
            await _trainingService.UpdateCoursesofStaticDiscount(staticDiscountViewModel.Id, staticDiscountViewModel.SelectedCourses.ToList());
            await _trainingService.UpdateUserRolesofStaticDiscount(staticDiscountViewModel.Id, staticDiscountViewModel.SelectedUserRoles.ToList());

            _trainingService.Save();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
