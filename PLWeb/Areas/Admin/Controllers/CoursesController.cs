using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PLCore.DTOs.Course;
using PLCore.Generators;
using PLCore.Services.Interfaces;
using PLDataLayer.Entities.Training;
using PLCore.Convertors;
using PLDataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using PLCore.Security;
using System.Drawing;
using PLCore.DTOs.SkyRoom;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CoursesController : Controller
    {

        private readonly ITrainingService _trainingService;
        private readonly IUserService _userService;
        private readonly ISkyService _skyService;

        public CoursesController(ITrainingService trainingService, IUserService userService, ISkyService skyService)
        {
            _trainingService = trainingService;
            _userService = userService;
            _skyService = skyService;
        }

        // GET: Admin/Courses
        [PermissionChecker(64)]
        public async Task<IActionResult> Index()
        {
            User user = await _userService.GetUserByUserName(User.Identity.Name).ConfigureAwait(false);
            int roleId = int.Parse(User.FindFirst("RoleId").Value.ToString());
            UserRole userRole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId).ConfigureAwait(false);
            List<Course> courses = await _trainingService.GetCoursesAsync().ConfigureAwait(false);
            if (roleId == 3)
            {
                courses = courses.Where(w => w.CourseUsers.Any(a => a.URId == userRole.URId)).ToList();
            }
            return View(courses);
        }

        // GET: Admin/Courses/Details/5
        [PermissionChecker(67)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _trainingService.GetCourseAsync((int)id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Admin/Courses/Create
        [PermissionChecker(65)]
        public async Task<IActionResult> Create()
        {
            int roleId = int.Parse(User.FindFirst("RoleId").Value.ToString());
            UserRole Tuserrole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId);
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                CourseGroups = await _trainingService.GetCourseGroupsAsync(),
                CourseStatuses = await _trainingService.GetCourseStatusesAsync(),
                CourseLevels = await _trainingService.GetCourseLevelsAsync(),
                CourseTeachers = await _userService.GetTeachersAsync(),
                TypeofMeurements = await _trainingService.GetCourseTypeofMeasurments(),
                SteppedDisCodes = await _trainingService.GetSteppedDiscountsAsync()
            };
            if (roleId == 3)
            {
                courseViewModel.CourseTeachers = courseViewModel.CourseTeachers.Where(w => w.URId == Tuserrole.URId).ToList();
            }

            return View(courseViewModel);
        }

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(65)]
        public async Task<IActionResult> Create(CourseViewModel courseViewModel, IFormFile Course_Image)
        {
            try
            {
                courseViewModel.CourseLevels = await _trainingService.GetCourseLevelsAsync();
                courseViewModel.CourseGroups = await _trainingService.GetCourseGroupsAsync();
                courseViewModel.CourseStatuses = await _trainingService.GetCourseStatusesAsync();
                courseViewModel.CourseTeachers = await _userService.GetTeachersAsync();
                courseViewModel.TypeofMeurements = await _trainingService.GetCourseTypeofMeasurments();
                courseViewModel.SteppedDisCodes = await _trainingService.GetSteppedDiscountsAsync();

                if (!ModelState.IsValid)
                {
                    return View(courseViewModel);
                }
                if (Course_Image != null)
                {
                    if (Course_Image.Length > .05 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Course_Image", "حجم تصویر بیشتر از 50 کیلو بایت است !");
                        return View(courseViewModel);
                    }
                    Image image = Image.FromStream(Course_Image.OpenReadStream(), true, true);
                    int w = image.Width;
                    int h = image.Height;
                    if (image.Width != 400 || image.Height != 250)
                    {
                        ModelState.AddModelError("Course_Image", " عرض تصویر 400 پیکسل و ارتفاع آن باید 250 پیکسل باشد !");
                        return View(courseViewModel);
                    }
                }
                Course course = new Course()
                {
                    CourseGroup_Id = courseViewModel.CourseGroup_Id,
                    CourseLevel_Id = courseViewModel.CourseLevel_Id,
                    CourseStatus_Id = courseViewModel.CourseStatus_Id,
                    CTM_Id = courseViewModel.CourseStatus_Id,


                };
                if (string.IsNullOrEmpty(courseViewModel.Course_Fee))
                {
                    courseViewModel.SteppedDiscountCode = string.Empty;
                }
                if (courseViewModel.Course_Fee == "0")
                {
                    courseViewModel.SteppedDiscountCode = string.Empty;
                }
                course.CTM_Id = courseViewModel.CTM_Id;
                course.Course_Title = courseViewModel.Course_Title;
                course.Course_Capacity = int.Parse(courseViewModel.Course_Capacity);
                course.Course_CreateDate = DateTime.Now;
                course.Course_Comment = courseViewModel.Course_Comment;
                course.Course_Duration = courseViewModel.Course_Duration;
                course.Course_Fee = int.Parse(courseViewModel.Course_Fee);
                course.Course_Tags = courseViewModel.Course_Tags;
                course.Course_abstract = courseViewModel.Course_abstract;
                course.Course_StartDate = DateConvertor.ToMiladi(courseViewModel.Course_StartDate);
                course.Course_EndDate = DateConvertor.ToMiladi(courseViewModel.Course_EndDate);
                course.Course_StartTime = courseViewModel.Course_StartTime;
                course.Course_EndTime = courseViewModel.Course_EndTime;
                course.Course_EndDateRegistration = DateConvertor.ToMiladi(courseViewModel.Course_EndDateRegistration);
                course.Course_IsActive = true;
                course.Course_UpdateDate = DateTime.Now;
                course.Course_about = courseViewModel.Course_about;
                course.OP_Create = User.Identity.Name + "|" + DateTime.Now.ToShamsiWithTime();
                course.CourseLink = courseViewModel.CourseLink;
                course.SteppedDiscountCode = courseViewModel.SteppedDiscountCode;
                _trainingService.CreateCourse(course);
                await _trainingService.SaveAsync();
                UserRole Teacher = await _userService.GetUserRoleByIdAsync(courseViewModel.TeacherId);
                CourseUser courseUser = new CourseUser()
                {
                    URId = Teacher.URId,
                    Course_Id = course.Course_Id,
                    CU_CreateDate = DateTime.Now,
                    OP_Create = User.Identity.Name,
                    IsActive = true
                };
                _trainingService.CreateCourseUser(courseUser);

                //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course", Course_Image.FileName);
                string fileName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(Course_Image.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course", fileName);
                course.Course_Image = fileName;
                _trainingService.Save();

                #region saveNewsFile
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Course_Image.CopyTo(stream);
                }
                #endregion saveNewsFile
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                string m = ex.Message;
                return View(courseViewModel);
            }
        }

        // GET: Admin/Courses/Edit/5
        [PermissionChecker(66)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _trainingService.GetCourseAsync((int)id);
            if (course == null)
            {
                return NotFound();
            }
            UserRole Teacher = await _trainingService.GetCourseTeacherAsync(course.Course_Id);
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                Course_Id = course.Course_Id,
                Course_Image = course.Course_Image,
                CourseGroup_Id = course.CourseGroup_Id,
                CourseLevel_Id = course.CourseLevel_Id,
                CourseStatus_Id = course.CourseStatus_Id,
                CTM_Id = course.CTM_Id,
                Course_about = course.Course_about,
                Course_abstract = course.Course_abstract,
                Course_Capacity = course.Course_Capacity.ToString(),
                Course_Comment = course.Course_Comment,
                Course_Duration = course.Course_Duration,
                Course_EndDate = course.Course_EndDate.ToShamsi(),
                Course_EndDateRegistration = course.Course_EndDateRegistration.ToShamsi(),
                Course_Fee = course.Course_Fee.ToString(),
                Course_StartDate = course.Course_StartDate.ToShamsi(),
                Course_Tags = course.Course_Tags,
                Course_Title = course.Course_Title,
                Course_StartTime = course.Course_StartTime,
                Course_EndTime = course.Course_EndTime,
                CourseLink = course.CourseLink,
                SteppedDiscountCode = course.SteppedDiscountCode,
                CourseGroups = await _trainingService.GetCourseGroupsAsync(),
                CourseStatuses = await _trainingService.GetCourseStatusesAsync(),
                CourseLevels = await _trainingService.GetCourseLevelsAsync(),
                CourseTeachers = await _userService.GetTeachersAsync(),
                TypeofMeurements = await _trainingService.GetCourseTypeofMeasurments(),
                SteppedDisCodes = await _trainingService.GetSteppedDiscountsAsync(),
                IsActive = course.Course_IsActive
            };
            if (Teacher != null)
            {
                courseViewModel.TeacherId = Teacher.URId;
            }
            return View(courseViewModel);
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(66)]

        public async Task<IActionResult> Edit(int id, CourseViewModel courseViewModel, IFormFile Course_Image)
        {
            if (id != courseViewModel.Course_Id)
            {
                return NotFound();
            }
            Course course = await _trainingService.GetCourseAsync(id);
            courseViewModel.CourseGroups = await _trainingService.GetCourseGroupsAsync().ConfigureAwait(false);
            courseViewModel.CourseStatuses = await _trainingService.GetCourseStatusesAsync().ConfigureAwait(false);
            courseViewModel.CourseLevels = await _trainingService.GetCourseLevelsAsync().ConfigureAwait(false);
            courseViewModel.CourseTeachers = await _userService.GetTeachersAsync().ConfigureAwait(false);
            courseViewModel.TypeofMeurements = await _trainingService.GetCourseTypeofMeasurments().ConfigureAwait(false);
            courseViewModel.SteppedDisCodes = await _trainingService.GetSteppedDiscountsAsync().ConfigureAwait(false);
            if (ModelState.IsValid)
            {
                try
                {
                    if (Course_Image != null)
                    {
                        if (Course_Image.Length > .05 * 1024 * 1024)
                        {
                            ModelState.AddModelError("Course_Image", "حجم تصویر بیشتر از 50 کیلو بایت است !");
                            return View(courseViewModel);
                        }
                        Image image = Image.FromStream(Course_Image.OpenReadStream(), true, true);
                        int w = image.Width;
                        int h = image.Height;
                        if (image.Width != 400 || image.Height != 250)
                        {
                            ModelState.AddModelError("Course_Image", " عرض تصویر 400 پیکسل و ارتفاع آن باید 250 پیکسل باشد !");
                            return View(courseViewModel);
                        }
                    }

                    if (string.IsNullOrEmpty(courseViewModel.Course_Fee))
                    {
                        courseViewModel.SteppedDiscountCode = string.Empty;
                    }
                    if (courseViewModel.Course_Fee == "0")
                    {
                        courseViewModel.SteppedDiscountCode = string.Empty;
                    }
                    course.CourseGroup_Id = courseViewModel.CourseGroup_Id;
                    course.CourseLevel_Id = courseViewModel.CourseLevel_Id;
                    course.CourseStatus_Id = courseViewModel.CourseStatus_Id;
                    course.CTM_Id = courseViewModel.CTM_Id;
                    course.Course_Title = courseViewModel.Course_Title;
                    course.Course_Capacity = int.Parse(courseViewModel.Course_Capacity);
                    course.Course_Comment = courseViewModel.Course_Comment;
                    course.Course_Duration = courseViewModel.Course_Duration;
                    course.Course_Fee = int.Parse(courseViewModel.Course_Fee);
                    course.Course_Tags = courseViewModel.Course_Tags;
                    course.Course_about = courseViewModel.Course_about;
                    course.Course_StartDate = DateConvertor.ToMiladi(courseViewModel.Course_StartDate);
                    course.Course_EndDate = DateConvertor.ToMiladi(courseViewModel.Course_EndDate);
                    course.Course_StartTime = courseViewModel.Course_StartTime;
                    course.Course_EndTime = courseViewModel.Course_EndTime;
                    course.Course_EndDateRegistration = DateConvertor.ToMiladi(courseViewModel.Course_EndDateRegistration);
                    course.SteppedDiscountCode = courseViewModel.SteppedDiscountCode;
                    course.CourseLink = courseViewModel.CourseLink;
                    course.Course_abstract = courseViewModel.Course_abstract;
                    course.OP_Update = User.Identity.Name + "|" + DateTime.Now.ToShamsiWithTime() + "-";
                    if (Course_Image != null)
                    {
                        string nowimgPath = "/images/course/" + course.Course_Image;
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course", Course_Image.FileName);
                        string fileName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(Course_Image.FileName);
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course", fileName);
                        course.Course_Image = fileName;
                        
                        if (System.IO.File.Exists(nowimgPath))
                        {
                            System.IO.File.Delete(nowimgPath);
                        }

                        #region saveFile
                        using var stream = new FileStream(filePath, FileMode.Create);
                        Course_Image.CopyTo(stream);
                        #endregion saveFile
                    }
                    _trainingService.UpdateCourse(course);
                    if (course.CourseUsers != null)
                    {
                        if (course.CourseUsers.Any(a => a.URId == courseViewModel.TeacherId && a.Course_Id == course.Course_Id))
                        {
                            CourseUser courseUser = await _trainingService.GetCourseUserBy_URId_CourseId(courseViewModel.TeacherId, course.Course_Id);
                            if (courseUser != null)
                            {
                                courseUser.URId = courseViewModel.TeacherId;                                
                                _trainingService.UpdateCourseUser(courseUser);
                            }
                        }
                        else
                        {
                            UserRole currentTeacher = await _trainingService.GetCourseTeacherAsync(course.Course_Id).ConfigureAwait(false);
                            //CourseUser NowTeacher = course.CourseUsers.FirstOrDefault(f => f.UserRole.RoleId == 3);
                            UserRole Teacher = await _userService.GetUserRoleByIdAsync(courseViewModel.TeacherId).ConfigureAwait(false);
                            if (currentTeacher != null)
                            {
                                if (currentTeacher != Teacher)
                                {
                                    CourseUser courseUser1 = await _trainingService.GetCourseUserBy_URId_CourseId(currentTeacher.URId, course.Course_Id).ConfigureAwait(false);
                                    if (courseUser1 != null)
                                    {
                                        course.CourseUsers.Remove(courseUser1);
                                    }

                                }
                            }

                            CourseUser courseUser = new CourseUser()
                            {
                                URId = Teacher.URId,
                                Course_Id = course.Course_Id,
                                IsActive = true
                            };
                            _trainingService.CreateCourseUser(courseUser);
                        }
                    }
                    else
                    {
                        UserRole Teacher = await _userService.GetUserRoleByIdAsync(courseViewModel.TeacherId);
                        CourseUser courseUser = new CourseUser()
                        {
                            URId = Teacher.URId,
                            Course_Id = course.Course_Id,
                            IsActive = true
                        };
                        _trainingService.CreateCourseUser(courseUser);
                    }

                    await _trainingService.SaveAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Course_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        //Active Deactive Course
        [PermissionChecker(66)]
        public async Task<bool> ManageCourseStatus(int id, bool status)
        {
            Course course = await _trainingService.GetCourseAsync(id);
            if (course == null)
            {
                return false;
            }
            course.Course_IsActive = status;
            DateTime? dt = new DateTime();
            dt = DateTime.Now;
            course.Course_UpdateDate = dt;
            course.OP_Update = User.Identity.Name + "|" + DateTime.Now.ToShamsiWithTime() + " " + status.ToString() + "-";
            _trainingService.UpdateCourse(course);
            await _trainingService.SaveAsync();
            return true;
        }
        // GET: Admin/Courses/Delete/5
        [PermissionChecker(68)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _trainingService.GetCourseAsync((int)id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Admin/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(68)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _trainingService.GetCourseAsync(id);
            course.IsDeleted = true;
            course.OP_Remove = User.Identity.Name + "|" + DateTime.Now.ToShamsiWithTime();
            _trainingService.UpdateCourse(course);
            await _trainingService.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionChecker(70)]
        public IActionResult InsertFiles(int courseId)
        {
            CourseFile courseFile = new CourseFile()
            {
                Course_Id = courseId,

            };
            return View(courseFile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(69)]
        public async Task<IActionResult> InsertFiles(CourseFile courseFile, IFormFile CF_File)
        {
            if (!ModelState.IsValid)
            {
                return View(courseFile);
            }
            if (CF_File == null)
            {
                ModelState.AddModelError("CF_File", "لطفا فایل را انتخاب کنید !");
                return View(courseFile);
            }
            string extension = Path.GetExtension(CF_File.FileName);
            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".mp4", ".m4v", ".mpeg", ".mp3", ".pdf" };
            if (!allowedExtensions.Any(a => a == extension))
            {
                ModelState.AddModelError("CF_File", "فایل با این فرمت قابل آپلود نیست !");

                return View(courseFile);
            }
            #region saveFile

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", CF_File.FileName);
            string newsfileName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(CF_File.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", newsfileName);
            courseFile.CF_Date = DateTime.Now;
            courseFile.CF_Type = CF_File.ContentType;
            courseFile.CF_File = newsfileName;
            #endregion saveFile
            courseFile.OP_Create = User.Identity.Name + "|" + DateTime.Now.ToShamsiWithTime();
            _trainingService.CreateCourseFile(courseFile);
            await _trainingService.SaveAsync();


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await CF_File.CopyToAsync(stream);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ShowFiles(string type, int courseId)
        {
            ViewData["type"] = type;
            ViewData["cid"] = courseId;
            return View(await _trainingService.GetCourseFilesByType_Cid(type, courseId));
        }
        [PermissionChecker(70)]
        public async Task<IActionResult> EditFile(int cfId)
        {

            CourseFile courseFile = await _trainingService.GetCourseFileByIdAsync(cfId);
            ViewBag.Course_Id = new SelectList(await _trainingService.GetCoursesAsync(), "Course_Id", "Course_Title", courseFile.Course_Id);
            return View(courseFile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(70)]
        public async Task<IActionResult> EditFile(CourseFile courseFile, IFormFile CF_File)
        {


            if (!ModelState.IsValid)
            {
                ViewBag.Course_Id = new SelectList(await _trainingService.GetCoursesAsync(), "Course_Id", "Course_Title", courseFile.Course_Id);
                return View(courseFile);
            }
            string filePath = string.Empty;
            if (CF_File != null)
            {
                #region saveFile

                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", CF_File.FileName);
                string coursefileName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(CF_File.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", coursefileName);
                courseFile.CF_Date = DateTime.Now;
                courseFile.CF_Type = CF_File.ContentType;
                courseFile.CF_File = coursefileName;
                #endregion saveFile
            }
            if (!courseFile.IsDeleted)
            {
                courseFile.OP_Update += "-" + User.Identity.Name + " " + DateTime.Now.ToShamsiWithTime();
            }
            else
            {
                courseFile.OP_Remove += "-" + User.Identity.Name + " " + DateTime.Now.ToShamsiWithTime();
            }

            _trainingService.UpdateCourseFile(courseFile);
            await _trainingService.SaveAsync();
            if (!string.IsNullOrEmpty(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await CF_File.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Details", new { id = courseFile.Course_Id });
        }
        [PermissionChecker(71)]
        public async Task<IActionResult> DeleteFile(int id)
        {
            CourseFile courseFile = await _trainingService.GetCourseFileByIdAsync(id);
            return View(courseFile);
        }
        [HttpPost, ActionName("DeleteFile")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(71)]
        public async Task<IActionResult> DeleteFileConfirmed(int id)
        {
            CourseFile courseFile = await _trainingService.GetCourseFileByIdAsync(id);
            courseFile.IsDeleted = true;
            courseFile.OP_Remove += "-" + User.Identity.Name + "|" + DateTime.Now.ToShamsiWithTime();
            _trainingService.UpdateCourseFile(courseFile);
            await _trainingService.SaveAsync();
            return RedirectToAction(nameof(Details), new { id = courseFile.Course_Id });
        }
        public async Task<IActionResult> CourseUsers(int cid)
        {
            var course = await _trainingService.GetCourseAsync(cid).ConfigureAwait(false);
            ViewData["course"] = course.Course_Title;
            UserRole Teacher = await _trainingService.GetCourseTeacherAsync(cid).ConfigureAwait(false);
            ViewData["teacher"] = Teacher;
            List<CourseUser> courseUsers = await _trainingService.GetCourseUsersByRoleAsync(cid, 4).ConfigureAwait(false);
            return View(courseUsers);
        }
        public async Task<IActionResult> EditCourseUser(int id)
        {
            CourseUser courseUser = await _trainingService.GetCourseUserByIdAsync(id);
            if (courseUser == null)
            {
                return NotFound("کاربر دوره یافت نشد !");
            }

            return PartialView(courseUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourseUser(CourseUser courseUser)
        {
            if (!ModelState.IsValid)
            {
                return View(courseUser);
            }
            _trainingService.UpdateCourseUser(courseUser);
            await _trainingService.SaveAsync();
            return RedirectToAction(nameof(CourseUsers), new { cid = courseUser.Course_Id });
        }
        [PermissionChecker(72)]
        public async Task<IActionResult> RemoveCourseUser(int id)
        {
            CourseUser courseUser = await _trainingService.GetCourseUserByIdAsync(id);
            return View(courseUser);
        }
        [HttpPost, ActionName("RemoveCourseUser")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(72)]
        public async Task<IActionResult> RemoveCourseUserConfirmed(int id)
        {
            CourseUser courseUser = await _trainingService.GetCourseUserByIdAsync(id);

            _trainingService.RemoveCourseUser(courseUser);
            await _trainingService.SaveAsync();
            return RedirectToAction(nameof(CourseUsers), new { cid = courseUser.Course_Id });
        }
        private bool CourseExists(int id)
        {
            return _trainingService.IsExistCourse(id);
        }
        /// <summary>
        /// note : userIds are SkyRoom User Ids
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RemoveUsersFromRoom(int cId, int? roomId, List<int> userIds)
        {
            if (userIds != null)
            {
                List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel> skyUserViewModels = _skyService.GetRoomUsers(roomId);
                List<int> RemoveList = userIds.Intersect(skyUserViewModels.Select(s => s.user_id)).ToList();
                if (RemoveList != null)
                {
                    if (RemoveList.Count != 0)
                    {
                        int id = _skyService.RemoveStudentsFromRoom(roomId, RemoveList);
                    }
                }
               
            }
            return RedirectToAction(nameof(CourseUsers), new { cid = cId });
        }
        public IActionResult AddUsersToRoom(int cId, int? roomId, List<int> userIds)
        {
            if (userIds != null)
            {
                List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel> skyUserViewModels = _skyService.GetRoomUsers(roomId);
                List<int> AddList = userIds.Except(skyUserViewModels.Select(s => s.user_id)).ToList();
                if (AddList != null)
                {
                    if (AddList.Count != 0)
                    {
                        _skyService.AddStudentsToRoom(roomId, AddList);
                        
                    }
                }
               
            }
            return RedirectToAction(nameof(CourseUsers), new { cid = cId });
        }
    }
}
