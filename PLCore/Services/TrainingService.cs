using Microsoft.EntityFrameworkCore;
using PLCore.DTOs.Course;
using PLCore.DTOs.General;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using PLDataLayer.Entities.Sale;
using PLDataLayer.Entities.Training;
using PLDataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PLCore.Convertors;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PLCore.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly PLContext _PLContext;
        public TrainingService(PLContext PLContext)
        {
            _PLContext = PLContext;
        }
        #region General
        public void Save()
        {
            _PLContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _PLContext.SaveChangesAsync();
        }
        #endregion General
        #region CourseGroup
        public async Task<CourseGroup> CreateCourseGroupAsync(CourseGroup CourseGroup)
        {
            await _PLContext.CourseGroups.AddAsync(CourseGroup);
            return CourseGroup;
        }

        public async Task<List<CourseGroup>> GetCourseGroupsAsync()
        {
            return await _PLContext.CourseGroups.Include(r => r.CourseGroups).Include(r => r.Courses).ToListAsync();
        }

        public void UpdateCourseGroup(CourseGroup CourseGroup)
        {
            _PLContext.Update(CourseGroup);
        }
        public async Task<CourseGroup> GetCourseGroupByIdAsync(int id)
        {
            return await _PLContext.CourseGroups
                .Include(r => r.CourseGroups).Include(r => r.Courses)
                .SingleOrDefaultAsync(s => s.CourseGroup_Id == id);
        }
        public bool ExistCourseGroup(int id)
        {
            return _PLContext.CourseGroups.Any(a => a.CourseGroup_Id == id);
        }
        #endregion CourseGroup
        #region Course
        public async Task CreateCourseAsync(Course course)
        {
            await _PLContext.Courses.AddAsync(course);

        }
        public void CreateCourse(Course course)
        {
            _PLContext.Courses.Add(course);

        }
        public void UpdateCourse(Course course)
        {
            _PLContext.Courses.Update(course);
        }
        public async Task<bool> RemoveCourseAsync(int id)
        {
            Course course = await _PLContext.Courses.FindAsync(id);
            if (course == null)
            {
                return false;
            }
            else
            {
                _PLContext.Courses.Remove(course);
                return true;
            }

        }

        public async Task<Course> GetCourseAsync(int id)
        {
            return await _PLContext.Courses
                .Include(c => c.CourseGroup).Include(c => c.CourseLevel).Include(c => c.CourseStatus).Include(r => r.CourseEpisodes).Include(r => r.CourseFiles)
                .Include(r => r.CourseUsers).Include(r => r.CourseTypeofMeasurment)
                .SingleOrDefaultAsync(s => s.Course_Id == id);
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            return await _PLContext.Courses
                .Include(c => c.CourseGroup).Include(c => c.CourseLevel).Include(c => c.CourseStatus)
                .Include(r => r.CourseEpisodes).Include(r => r.CourseFiles).Include(r => r.CourseUsers)

                .ToListAsync();
        }
        public async Task<List<CourseLevel>> GetCourseLevelsAsync()
        {
            return await _PLContext.CourseLevels.Include(r => r.Courses).ToListAsync();
        }

        public async Task<List<CourseStatus>> GetCourseStatusesAsync()
        {
            return await _PLContext.CourseStatuses.Include(r => r.Courses).ToListAsync();
        }
        public bool IsExistCourse(int id)
        {
            return _PLContext.Courses.Any(e => e.Course_Id == id);
        }
        public async Task<List<Course>> GetCoursesByGroupId(int gid)
        {
            return await _PLContext.Courses.Include(r => r.CourseGroup).Include(r => r.CourseEpisodes).Include(r => r.CourseFiles)
                .Include(r => r.CourseLevel).Include(r => r.CourseStatus).Include(r => r.CourseUsers).Include(r => r.CourseTypeofMeasurment)
                .Where(w => w.CourseGroup_Id == gid).ToListAsync();
        }
        public async Task<List<Course>> GetCoursesByMounth(int year, int mounth)
        {
            PersianCalendar pc = new PersianCalendar();
            List<Course> courses = await _PLContext.Courses.Include(r => r.CourseGroup).Include(r => r.CourseEpisodes).Include(r => r.CourseFiles)
                .Include(r => r.CourseLevel).Include(r => r.CourseStatus).Include(r => r.CourseUsers).ToListAsync();
            courses = courses.Where(w => w.Course_IsActive && pc.GetYear(w.Course_StartDate) == year && pc.GetMonth(w.Course_StartDate) == mounth).ToList();
            return courses;
        }
        public async Task<List<Course>> Search_in_Courses(string srch)
        {
            List<UserRole> userRoles = await _PLContext.UserRoles.Include(r => r.User).Include(r => r.Role)
                                        .Where(w => w.RoleId == 3 && (w.User.UserFirstName.Contains(srch) || w.User.UserFamily.Contains(srch) || (w.User.UserFirstName + w.User.UserFamily).Replace(" ", "").Contains(srch.Replace(" ", "")))).ToListAsync();
            List<CourseFile> courseFiles = await _PLContext.CourseFiles
                .Include(r => r.Course).Include(r => r.Course.CourseGroup).Include(r => r.Course.CourseLevel)
                .Include(r => r.Course.CourseStatus).Include(r => r.Course.CourseUsers)
                .Where(w => w.CF_Title.Contains(srch) || w.CF_Comment.Contains(srch)).ToListAsync();
            List<Course> coursesU = new List<Course>();
            foreach (var item in userRoles)
            {
                List<Course> courses1 = await _PLContext.Courses.Include(r => r.CourseGroup).Include(r => r.CourseEpisodes).Include(r => r.CourseFiles)
                .Include(r => r.CourseLevel).Include(r => r.CourseStatus).Include(r => r.CourseUsers)
                                    .Where(w => w.CourseUsers.Any(a => a.URId == item.URId)
                                    ).Distinct().ToListAsync();
                coursesU.AddRange(courses1);
            }
            if (courseFiles != null)
            {
                foreach (var cf in courseFiles)
                {
                    coursesU.Add(cf.Course);
                }

            }

            List<Course> courses = null;
            if (srch == "رایگان")
            {
                courses = await _PLContext.Courses.Include(r => r.CourseGroup).Include(r => r.CourseEpisodes).Include(r => r.CourseFiles)
              .Include(r => r.CourseLevel).Include(r => r.CourseStatus).Include(r => r.CourseUsers)
              .Where(w => w.Course_IsActive == true && (w.Course_Title.Contains(srch) || w.Course_Tags.Contains(srch)
              || w.Course_Comment.Contains(srch) || w.Course_abstract.Contains(srch) || w.Course_about.Contains(srch)
             || w.CourseGroup.CourseGroup_Title.Contains(srch) || w.CourseLevel.CourseLevel_Title.Contains(srch)
             || w.CourseStatus.CourseStatus_Title.Contains(srch) || w.CourseTypeofMeasurment.CTM_Title.Contains(srch)
             || w.Course_Fee == 0)).Distinct().ToListAsync();
            }
            else
            {
                courses = await _PLContext.Courses.Include(r => r.CourseGroup).Include(r => r.CourseEpisodes).Include(r => r.CourseFiles)
               .Include(r => r.CourseLevel).Include(r => r.CourseStatus).Include(r => r.CourseUsers)
               .Where(w => w.Course_IsActive == true && (w.Course_Title.Contains(srch) || w.Course_Tags.Contains(srch)
               || w.Course_Comment.Contains(srch) || w.Course_abstract.Contains(srch) || w.Course_about.Contains(srch)
              || w.CourseGroup.CourseGroup_Title.Contains(srch) || w.CourseLevel.CourseLevel_Title.Contains(srch)
              || w.CourseStatus.CourseStatus_Title.Contains(srch) || w.CourseTypeofMeasurment.CTM_Title.Contains(srch)
              )).Distinct().ToListAsync();
            }
            List<Course> result = coursesU.Union(courses).ToList();
            return result;
        }
        public async Task<(bool CanReg, string Message)> ValidateRegisterinCourse(int cid, int urId)
        {
            Course course = await _PLContext.Courses.Include(r => r.CourseUsers).SingleOrDefaultAsync(s => s.Course_Id == cid);
            UserRole userRole = await _PLContext.UserRoles.Include(r => r.CourseUsers).Include(r => r.User).Include(r => r.Role).SingleOrDefaultAsync(s => s.URId == urId);
            if (course.Course_IsActive == false)
            {
                return (false, "دسترسی به دوره امکان پذیر نیست !");
            }
            if (course.CourseUsers.Any(r => r.URId == urId && r.IsActive == true))
            {
                string sex = string.Empty;
                if (userRole.User.UserSex.Trim() == "مرد")
                {
                    sex = "آقای";
                }
                if (userRole.User.UserSex.Trim() == "زن")
                {
                    sex = "خانم";
                }

                return (false, userRole.Role.RoleTitle + " " + "عزیز" + " " + sex + " " + userRole.User.UserFirstName + " " + userRole.User.UserFamily + " " + "شما قبلا در دوره ثبت نام کرده اید");
            }

            if (DateTime.Now > course.Course_EndDateRegistration)
            {
                return (false, "مهلت ثبت نام دوره به پایان رسیده است !");
            }
            if (userRole.RoleId != 4)
            {
                return (false, "فقط فراگیران اجازه ثبت نام در دوره را دارند");
            }
            int Ucount = 0;

            if (course.CourseUsers != null)
            {
                if (course.CourseUsers.Count() != 0)
                {
                    List<UserRole> strudents = await GetCourseUsersByRoleAsync(course.CourseUsers.ToList(), 4, course.Course_Id);
                    if (strudents != null)
                    {
                        Ucount = strudents.Count();
                    }
                }
            }
            if (course.Course_Capacity - Ucount <= 0)
            {
                return (false, "ظرفیت ثبت نام دوره تکمیل شده است !");
            }
            return (true, "ثبت نام امکان پذیر است");
        }
        public async Task<int> UpdateCourseVisits(int id)
        {
            Course course = await _PLContext.Courses.FindAsync(id);
            int visits = course.Course_Visits;
            visits += 1;
            course.Course_Visits = visits;
            _PLContext.Courses.Update(course);
            return visits;
        }

        public async Task<List<Course>> GetCoursesByRole(int urId)
        {
            UserRole userRole = await _PLContext.UserRoles.Include(r => r.CourseUsers).SingleOrDefaultAsync(s => s.URId == urId);
            List<Course> courses = await _PLContext.Courses.Include(r => r.CourseUsers).Include(r => r.CourseGroup).Include(r => r.CourseLevel).Include(r => r.CourseStatus)
                .ToListAsync();
            courses = courses.Where(w => w.CourseUsers.Any(a => a.URId == urId)).ToList();
            return courses.ToList();

        }

        public async Task<List<CourseUser>> GetCourseUsersByRoleAsync(int cId, int roleId)
        {

            return await _PLContext.CourseUsers.Include(r => r.Course)
                .Include(r => r.UserRole).Where(w => w.Course_Id == cId && w.UserRole.RoleId == roleId)
                .ToListAsync();
        }
        public async Task<CourseStatusResult> GetCourseStatusResult(int cId)
        {
            Course course = await _PLContext.Courses.Include(r => r.CourseUsers).SingleOrDefaultAsync(s => s.Course_Id == cId);
            CourseStatusResult courseStatusResult = new CourseStatusResult()
            {
                IsActive = false,
                HasCapicity = false,
                HasRegTime = false,
                IsFree = false,
                HasSteppedDiscount = false
            };
            List<UserRole> students = await GetCourseUsersByRoleAsync(course.CourseUsers.ToList(), 4, cId);
            
           
            
            int stCount = 0;
            if (students != null)
            {
                if (students.Count() != 0)
                {
                    stCount = students.Count();
                }
            }
            if (course.Course_IsActive == true)
            {
                courseStatusResult.IsActive = true;
            }
            if (course.Course_Capacity > stCount)
            {
                courseStatusResult.HasCapicity = true;
            }
            if (course.Course_EndDateRegistration >= DateTime.Now)
            {
                courseStatusResult.HasRegTime = true;
            }
            if (course.Course_Fee == 0)
            {
                courseStatusResult.IsFree = true;
            }
            if (!string.IsNullOrEmpty(course.SteppedDiscountCode))
            {
                SteppedDiscount steppedDiscount = await GetSteppedDiscountByCodeAsync(course.SteppedDiscountCode);
                if (steppedDiscount != null)
                {
                    if (steppedDiscount.IsActive == true)
                    {
                        courseStatusResult.HasSteppedDiscount = true;
                    }
                }

            }
            return courseStatusResult;
        }
        #endregion Course
        #region CourseUser
        public void CreateCourseUser(CourseUser courseUser)
        {
            CourseUser ExistCourseUser = _PLContext.CourseUsers.FirstOrDefault(w => w.Course_Id == courseUser.Course_Id && w.URId == courseUser.URId);
            if (ExistCourseUser == null)
            {
                _PLContext.CourseUsers.Add(courseUser);
            }
            
        }

        public async Task<CourseUser> GetCourseUserByIdAsync(int id)
        {
            return await _PLContext.CourseUsers.Include(r => r.UserRole).Include(r => r.UserRole.User)
                .Include(r => r.UserRole.Role).Include(r => r.Course).SingleOrDefaultAsync(s => s.CU_Id == id);
        }

        public async Task<List<UserRole>> GetCourseUsersByRoleAsync(List<CourseUser> courseUsers, int roleId, int? cId)
        {
            if (cId == null)
            {
                return null;
            }
            if (courseUsers == null)
            {
                return null;
            }
            if (courseUsers.Count() == 0)
            {
                return null;
            }
            Course course = await _PLContext.Courses.Include(r => r.CourseUsers).SingleOrDefaultAsync(s => s.Course_Id == (int)cId);
            List<UserRole> userRoles = new List<UserRole>();
            DateTime CrDatetime = new DateTime(2020, 5, 20);
            if (course.Course_CreateDate > CrDatetime)
            {
                courseUsers = courseUsers.Where(w => w.IsActive == true).ToList();
            }
            foreach (var item in courseUsers.ToList())
            {
                UserRole userRole = await _PLContext.UserRoles.Include(r => r.User).Include(r => r.Role).SingleOrDefaultAsync(s => s.URId == item.URId);
                if (userRole != null)
                {
                    if (userRole.RoleId == roleId)
                    {
                        userRoles.Add(userRole);
                    }

                }
            }
            return userRoles;
        }

        public void UpdateCourseUser(CourseUser courseUser)
        {
            _PLContext.CourseUsers.Update(courseUser);
        }

        public void RemoveCourseUser(CourseUser courseUser)
        {
            _PLContext.CourseUsers.Remove(courseUser);
        }

        public async Task<CourseUser> GetCourseUserBy_URId_CourseId(int UrId, int courseId)
        {
            return await _PLContext.CourseUsers.Include(r => r.Course).Include(r => r.UserRole)
                .FirstOrDefaultAsync(f => f.Course_Id == courseId && f.URId == UrId);
        }

        public async Task<UserRole> GetCourseTeacherAsync(int courseId)
        {
            CourseUser courseUser = await _PLContext.CourseUsers.Include(r => r.UserRole).Include(r => r.UserRole.User).Include(r => r.UserRole.Role).Include(r => r.Course)
                .FirstOrDefaultAsync(f => f.Course_Id == courseId && f.UserRole.RoleId == 3);
            if (courseUser != null)
            {
                return courseUser.UserRole;
            }
            return null;
        }

        public async Task RemoveUserFromCourse(int cId, int urId)
        {
            CourseUser courseUser = await _PLContext.CourseUsers.FirstOrDefaultAsync(f => f.URId == urId && f.Course_Id == cId);
            _PLContext.CourseUsers.Remove(courseUser);
        }
        public async Task<List<CourseUser>> GetAllCourse_UsersAsync()
        {
            return await _PLContext.CourseUsers
                .Include(r => r.UserRole).Include(r => r.UserRole.User).Include(r => r.UserRole.Role).Include(r => r.Course)
                .Where(w => w.UserRole.RoleId == 4).ToListAsync();
        }

        public async Task<List<CourseUser>> GetAllCourse_TeachersAsync()
        {
            return await _PLContext.CourseUsers
                .Include(r => r.UserRole).Include(r => r.UserRole.User).Include(r => r.UserRole.Role).Include(r => r.Course)
                .Where(w => w.UserRole.RoleId == 3).ToListAsync();
        }
        public async Task<List<CourseFile>> GetCourseFiles()
        {
            return await _PLContext.CourseFiles.ToListAsync();

        }
        public async Task<int> GetCourseFilesCount()
        {
            return await _PLContext.CourseFiles.CountAsync();

        }
        #endregion CourseUser
        #region CourseFile
        public void CreateCourseFile(CourseFile courseFile)
        {
            _PLContext.CourseFiles.Add(courseFile);
        }

        public async Task<List<CourseFile>> GetCourseFilesAsync()
        {
            return await _PLContext.CourseFiles.Include(r => r.Course).ToListAsync();
        }

        public void UpdateCourseFile(CourseFile courseFile)
        {
            _PLContext.CourseFiles.Update(courseFile);
        }

        public async Task<CourseFile> GetCourseFileByIdAsync(int id)
        {
            return await _PLContext.CourseFiles.Include(r => r.Course).SingleOrDefaultAsync(f => f.CF_Id == id);
        }

        public void RemoveCourseFile(CourseFile courseFile)
        {
            _PLContext.CourseFiles.Update(courseFile);

        }

        public async Task<List<CourseFile>> GetCourseFilesByType_Cid(string type, int courseId)
        {
            return await _PLContext.CourseFiles.Include(r => r.Course)
                .Where(w => w.Course_Id == courseId && w.CF_Type.Contains(type)).ToListAsync();
        }



        #endregion CourseFile
        #region CourseTypeofMeasurment
        public async Task<List<CourseTypeofMeasurment>> GetCourseTypeofMeasurments()
        {
            return await _PLContext.CourseTypeofMeasurments.ToListAsync();
        }
        public async Task<CourseTypeofMeasurment> GetCourseTypeofMeasurmentAsync(int tmId)
        {
            CourseTypeofMeasurment courseTypeofMeasurment = await _PLContext.CourseTypeofMeasurments.FirstOrDefaultAsync(f => f.CTM_Id == tmId);
            return courseTypeofMeasurment;
        }
        #endregion
        #region SteppedDiscountType
        public void CreateSteppedDiscountType(SteppedDiscountType steppedDiscountType)
        {
            _PLContext.SteppedDiscountTypes.Add(steppedDiscountType);
        }
        public void EditSteppedDiscountType(SteppedDiscountType steppedDiscountType)
        {
            _PLContext.SteppedDiscountTypes.Update(steppedDiscountType);
        }
        public async Task<List<SteppedDiscountType>> GetSteppedDiscountTypesAsync()
        {
            return await _PLContext.SteppedDiscountTypes.Include(r => r.SteppedDiscounts).ToListAsync();
        }
        public async Task<SteppedDiscountType> GetSteppedDiscountTypeByIdAsync(int id)
        {
            return await _PLContext.SteppedDiscountTypes.Include(r => r.SteppedDiscounts).SingleOrDefaultAsync(s => s.Id == id);
        }
        public void RemoveSteppedDiscountType(int id)
        {
            SteppedDiscountType steppedDiscountType = _PLContext.SteppedDiscountTypes.Find(id);
            if (steppedDiscountType != null)
            {
                _PLContext.SteppedDiscountTypes.Remove(steppedDiscountType);
            }
        }
        public bool ExistSteppedDiscountType(int id)
        {
            return _PLContext.SteppedDiscountTypes.Any(a => a.Id == id);
        }
        #endregion  SteppedDiscountType
        #region SteppedDiscount
        public void CreateSteppedDiscount(SteppedDiscount steppedDiscount)
        {
            _PLContext.SteppedDiscounts.Add(steppedDiscount);
        }

        public void EditSteppedDiscount(SteppedDiscount steppedDiscount)
        {
            _PLContext.SteppedDiscounts.Update(steppedDiscount);
        }

        public async Task<List<SteppedDiscount>> GetSteppedDiscountsAsync()
        {
            return await _PLContext.SteppedDiscounts.Include(r => r.SteppedDiscountType).Include(r => r.SteppedDiscountDetails).ToListAsync();
        }

        public async Task<SteppedDiscount> GetSteppedDiscountByIdAsync(int id)
        {
            return await _PLContext.SteppedDiscounts.Include(r => r.SteppedDiscountType).Include(r => r.SteppedDiscountDetails)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
        public List<SteppedDiscount> GetSteppedDiscounts()
        {
            return _PLContext.SteppedDiscounts.ToList();
        }
        public void RemoveSteppedDiscount(int id)
        {
            SteppedDiscount steppedDiscount = _PLContext.SteppedDiscounts.Find(id);
            if (steppedDiscount != null)
            {
                _PLContext.SteppedDiscounts.Remove(steppedDiscount);
            }
        }

        public bool ExistSteppedDiscount(int id)
        {
            return _PLContext.SteppedDiscounts.Any(a => a.Id == id);
        }
        public async Task<bool> ExistSteppedDiscount(string code)
        {
            return await _PLContext.SteppedDiscounts.AnyAsync(a => a.Code.ToLower().Trim() == code.ToLower().Trim());
        }
        public async Task<SteppedDiscount> GetSteppedDiscountByCodeAsync(string code)
        {
            return await _PLContext.SteppedDiscounts.Include(r => r.SteppedDiscountDetails).Include(r => r.SteppedDiscountType)
                .SingleOrDefaultAsync(s => s.Code.Trim().ToLower() == code.Trim().ToLower());
        }
        public async Task<List<SteppedDiscount>> GetValidSteppedDiscounts()
        {
            return await _PLContext.SteppedDiscounts.ToListAsync();
        }
        public async Task<SteppedDiscountDetail> GetValidDetailofSteppedDiscountAsync(string code, int nubat)
        {
            SteppedDiscount steppedDiscount = await _PLContext.SteppedDiscounts.Include(r => r.SteppedDiscountType).Include(r => r.SteppedDiscountDetails).SingleOrDefaultAsync(s => s.Code.ToLower() == code.ToLower());
            if (steppedDiscount == null)
            {
                return null;
            }
            if (steppedDiscount.SteppedDiscountType.Name == "person")
            {
                SteppedDiscountDetail steppedDiscountDetail = steppedDiscount.SteppedDiscountDetails.FirstOrDefault(f => nubat >= f.FromPerson && nubat <= f.ToPerson);
                return steppedDiscountDetail;
            }
            if (steppedDiscount.SteppedDiscountType.Name == "time")
            {
                SteppedDiscountDetail steppedDiscountDetail = steppedDiscount.SteppedDiscountDetails.FirstOrDefault(f => DateTime.Now >= f.FromDate && DateTime.Now <= f.ToDate);
                return steppedDiscountDetail;
            }
            return null;
        }

        public async Task<List<Course>> GetSteppedDiscountCoursesByCode(string Discode)
        {
            return await _PLContext.Courses.Where(w => w.SteppedDiscountCode.ToLower() == Discode.ToLower()).ToListAsync().ConfigureAwait(false);
        }
        public async Task<List<SteppedDiscountStatusViewModel>> SteppedDiscountStatusForCourseAsync(string DisCode, int courseId)
        {
            SteppedDiscount steppedDiscount = await _PLContext.SteppedDiscounts.Include(r => r.SteppedDiscountType).SingleOrDefaultAsync(s => s.Code.ToLower() == DisCode.ToLower());
            Course course = await _PLContext.Courses.Include(r => r.CourseUsers).SingleOrDefaultAsync(s => s.Course_Id == courseId);
            List<SteppedDiscountStatusViewModel> result = new List<SteppedDiscountStatusViewModel>();
            if (steppedDiscount != null && course != null)
            {
                if (steppedDiscount.IsActive == true)
                {
                    string type = steppedDiscount.SteppedDiscountType.Name;
                    int nubat = GetCourseUsersByRoleAsync(course.CourseUsers.ToList(), 4, course.Course_Id).Result.Count;
                    foreach (var item in steppedDiscount.SteppedDiscountDetails.OrderBy(r => r.FromPerson).OrderBy(r => r.FromDate).ToList())
                    {
                        SteppedDiscountStatusViewModel steppedDiscountStatusViewModel = new SteppedDiscountStatusViewModel();
                        if (type.ToLower() == "time")
                        {
                            steppedDiscountStatusViewModel.StatusMessage = "از تاریخ" + " " + item.FromDate.ToShamsiN_WithTime() + " " + "تا تاریخ" + " " + item.ToDate.ToShamsiN_WithTime();
                            if (item.ToDate >= DateTime.Now)
                            {
                                steppedDiscountStatusViewModel.StatusAbstract = "فعال";
                                steppedDiscountStatusViewModel.IsActive = true;
                            }
                            else
                            {
                                steppedDiscountStatusViewModel.StatusAbstract = "اتمام";
                                steppedDiscountStatusViewModel.IsActive = false;
                            }
                        }
                        if (type.ToLower() == "person")
                        {
                            steppedDiscountStatusViewModel.StatusMessage = "از نفر" + " " + item.FromPerson + " " + "تا نفر" + " " + item.ToPerson;
                            if (item.ToPerson > nubat)
                            {
                                steppedDiscountStatusViewModel.StatusAbstract = "فعال";
                                steppedDiscountStatusViewModel.IsActive = true;
                            }
                            else
                            {
                                steppedDiscountStatusViewModel.StatusAbstract = "اتمام";
                                steppedDiscountStatusViewModel.IsActive = false;
                            }
                        }
                        steppedDiscountStatusViewModel.DisPercent = item.Percent;
                        steppedDiscountStatusViewModel.StatusFee = course.Course_Fee - (int)Math.Round((course.Course_Fee * item.Percent / 100), 0);
                        
                        result.Add(steppedDiscountStatusViewModel);
                    }
                }
            }
            return result;
        }
        #endregion
        #region SteppedDiscountDetail
        public void CreateSteppedDiscountDetail(SteppedDiscountDetail steppedDiscountDetail)
        {
            _PLContext.SteppedDiscountDetails.Add(steppedDiscountDetail);
        }

        public void EditSteppedDiscountDetail(SteppedDiscountDetail steppedDiscountDetail)
        {
            _PLContext.SteppedDiscountDetails.Update(steppedDiscountDetail);
        }

        public async Task<List<SteppedDiscountDetail>> GetSteppedDiscountDetailsAsync()
        {
            return await _PLContext.SteppedDiscountDetails.Include(r => r.SteppedDiscount).Include(r => r.SteppedDiscount.SteppedDiscountType)
                .ToListAsync();
        }

        public List<SteppedDiscountDetail> GetSteppedDiscountDetails()
        {
            return _PLContext.SteppedDiscountDetails.Include(r => r.SteppedDiscount).Include(r => r.SteppedDiscount.SteppedDiscountType)
                .ToList();
        }

        public async Task<List<SteppedDiscountDetail>> GetSteppedDiscountDetailsAsync(string code)
        {
            return await _PLContext.SteppedDiscountDetails.Include(r => r.SteppedDiscount).Include(r => r.SteppedDiscount.SteppedDiscountType)
              .Where(r => r.SteppedDiscount.Code.Trim().ToLower() == code.Trim().ToLower()).ToListAsync();
        }
        public async Task<SteppedDiscountDetail> GetSteppedDiscountDetailByIdAsync(int Id)
        {
            return await _PLContext.SteppedDiscountDetails.Include(r => r.SteppedDiscount).Include(r => r.SteppedDiscount.SteppedDiscountType)
                .SingleOrDefaultAsync(s => s.Id == Id);
        }
        public void RemoveSteppedDiscountDetail(int id)
        {
            SteppedDiscountDetail steppedDiscountDetail = _PLContext.SteppedDiscountDetails.Find(id);
            _PLContext.SteppedDiscountDetails.Remove(steppedDiscountDetail);
        }

        public bool ExistSteppedDiscountDetail(int id)
        {
            return _PLContext.SteppedDiscountDetails.Any(a => a.Id == id);
        }
        #endregion
        #region StaticDiscount
        public void CreateStaticDiscount(StaticDiscount staticDiscount)
        {
            _PLContext.StaticDiscounts.Add(staticDiscount);
        }
        public async Task<bool> CreateStaticDiscount(StaticDiscount staticDiscount, List<int> SelectedUserRoles, List<int> SelectedCourses)
        {
            if (staticDiscount == null)
            {
                return false;
            }
            _PLContext.StaticDiscounts.Add(staticDiscount);
            await _PLContext.SaveChangesAsync();
            await AddCoursesToStaticDiscount(staticDiscount.SD_Id, SelectedCourses);
            await AddUserRolesToStaticDiscount(staticDiscount.SD_Id, SelectedUserRoles);
            return true;
        }
        public void EditStaticDiscount(StaticDiscount staticDiscount)
        {
            _PLContext.StaticDiscounts.Update(staticDiscount);
        }

        public bool RemoveStaticDiscount(int id)
        {
            StaticDiscount staticDiscount = _PLContext.StaticDiscounts.SingleOrDefault(s => s.SD_Id == id);
            if (staticDiscount != null)
            {
                _PLContext.StaticDiscounts.Remove(staticDiscount);
                return true;
            }
            return false;
        }

        public async Task<StaticDiscount> GetStaticDiscountByIdAsync(int id)
        {
            return await _PLContext.StaticDiscounts.Include(r => r.CourseStaticDiscounts).Include(r => r.UserRoleStaticDiscounts)
                .SingleOrDefaultAsync(s => s.SD_Id == id);
            ;
        }

        public async Task<StaticDiscount> GetStaticDiscountByCodeAsync(string code)
        {
            return await _PLContext.StaticDiscounts.Include(r => r.CourseStaticDiscounts).Include(r => r.UserRoleStaticDiscounts)
                .SingleOrDefaultAsync(s => s.SD_Code.Trim().ToLower() == code.Trim().ToLower());
        }

        public async Task<List<StaticDiscount>> GetStaticDiscountsAsync(int urId)
        {
            UserRole userRole = await _PLContext.UserRoles.Include(r => r.User).Include(r => r.Role).SingleOrDefaultAsync(s => s.URId == urId).ConfigureAwait(false);
            if (userRole == null)
            {
                return null;
            }
            if (userRole.RoleId == 1)
            {
                return await _PLContext.StaticDiscounts.Include(r => r.CourseStaticDiscounts).Include(r => r.UserRoleStaticDiscounts).ToListAsync();
            }
            else
            {
                if (userRole.RoleId == 3)
                {

                    return await _PLContext.StaticDiscounts.Include(r => r.CourseStaticDiscounts).Include(r => r.UserRoleStaticDiscounts)
                    .Where(w => w.Op_Creator_urID == urId).ToListAsync();

                }
                else
                {
                    return null;
                }

            }


        }

        public List<StaticDiscount> GetStaticDiscounts()
        {
            return _PLContext.StaticDiscounts.Include(r => r.CourseStaticDiscounts).Include(r => r.UserRoleStaticDiscounts).ToList();
        }

        public async Task<bool> ExistStaticDiscountAsync(string code)
        {
            if (string.IsNullOrEmpty(code)) return false;
            return await _PLContext.StaticDiscounts.AnyAsync(a => a.SD_Code.Trim().ToLower() == code.Trim().ToLower());
        }

        public bool ExistStaticDiscount(int id)
        {
            return _PLContext.StaticDiscounts.Any(a => a.SD_Id == id);
        }

        public List<string> GetStaticDiscountCodes()
        {
            return _PLContext.StaticDiscounts.Select(s => s.SD_Code).ToList();
        }

        public async Task<List<string>> GetStaticDiscountCodesAsync()
        {
            return await _PLContext.StaticDiscounts.Select(s => s.SD_Code).ToListAsync();
        }

        public async Task AddCoursesToStaticDiscount(int SDId, List<int> Course_Ids)
        {
            foreach (var cId in Course_Ids)
            {
                await _PLContext.CourseStaticDiscounts.AddAsync(new CourseStaticDiscount()
                {
                    SD_Id = SDId,
                    Course_Id = cId
                });
            }
        }

        public async Task AddUserRolesToStaticDiscount(int SDId, List<int> UserRole_Ids)
        {
            foreach (var urId in UserRole_Ids)
            {
                await _PLContext.UserRoleStaticDiscounts.AddAsync(new UserRoleStaticDiscount()
                {
                    SD_Id = SDId,
                    URId = urId
                });
            }
        }

        public async Task UpdateUserRolesofStaticDiscount(int SDId, List<int> UserRole_Ids)
        {
            StaticDiscount staticDiscount = await _PLContext.StaticDiscounts.Include(r => r.UserRoleStaticDiscounts).SingleOrDefaultAsync(s => s.SD_Id == SDId);
            List<int> exp = staticDiscount.UserRoleStaticDiscounts.Select(s => s.URId).Except(UserRole_Ids).ToList();
            if (exp != null)
            {
                if (exp.Count() == 0)
                {
                    exp = UserRole_Ids.Except(staticDiscount.UserRoleStaticDiscounts.Select(s => s.URId)).ToList();
                }
                if (exp.Count() != 0)
                {
                    foreach (var item in exp)
                    {
                        if (staticDiscount.UserRoleStaticDiscounts.Any(a => a.URId == item) && !UserRole_Ids.Any(a => a == item))
                        {
                            UserRoleStaticDiscount userRoleStaticDiscount = _PLContext.UserRoleStaticDiscounts
                                .FirstOrDefault(f => f.URId == item && f.SD_Id == SDId);
                            _PLContext.UserRoleStaticDiscounts.Remove(userRoleStaticDiscount);
                        }
                        if (!staticDiscount.UserRoleStaticDiscounts.Any(a => a.URId == item) && UserRole_Ids.Any(a => a == item))
                        {
                            UserRoleStaticDiscount userRoleStaticDiscount = new UserRoleStaticDiscount()
                            {
                                URId = item,
                                SD_Id = SDId
                            };
                            await _PLContext.UserRoleStaticDiscounts.AddAsync(userRoleStaticDiscount);
                        }

                    }
                }
            }
        }

        public async Task UpdateCoursesofStaticDiscount(int SDId, List<int> Course_Ids)
        {
            StaticDiscount staticDiscount = await _PLContext.StaticDiscounts.Include(r => r.CourseStaticDiscounts).SingleOrDefaultAsync(s => s.SD_Id == SDId);
            List<int> exp = staticDiscount.CourseStaticDiscounts.Select(s => s.Course_Id).Except(Course_Ids).ToList();
            if (exp != null)
            {
                if (exp.Count() == 0)
                {
                    exp = Course_Ids.Except(staticDiscount.CourseStaticDiscounts.Select(s => s.Course_Id)).ToList();
                }
                if (exp.Count() != 0)
                {
                    foreach (var item in exp)
                    {
                        if (staticDiscount.CourseStaticDiscounts.Any(a => a.Course_Id == item) && !Course_Ids.Any(a => a == item))
                        {
                            CourseStaticDiscount courseStaticDiscount = _PLContext.CourseStaticDiscounts
                                .FirstOrDefault(f => f.Course_Id == item && f.SD_Id == SDId);
                            _PLContext.CourseStaticDiscounts.Remove(courseStaticDiscount);
                        }
                        if (!staticDiscount.CourseStaticDiscounts.Any(a => a.Course_Id == item) && Course_Ids.Any(a => a == item))
                        {
                            CourseStaticDiscount courseStaticDiscount = new CourseStaticDiscount()
                            {
                                Course_Id = item,
                                SD_Id = SDId
                            };
                            await _PLContext.CourseStaticDiscounts.AddAsync(courseStaticDiscount);
                        }

                    }
                }
            }
        }

        public async Task<ValidationDiscountCodeViewModel> ValidationDiscountCode(string Code, int urId, int? CourseId)
        {
            UserRole LoginUserRole = await _PLContext.UserRoles.Include(r => r.User).Include(r => r.Role).FirstOrDefaultAsync(f => f.URId == urId);
            ValidationDiscountCodeViewModel validationDiscountCodeViewModel = new ValidationDiscountCodeViewModel()
            {
                IsValid = false
            };
            if (CourseId == null)
            {
                validationDiscountCodeViewModel.Message = "دوره مشخص نشده است !";
                validationDiscountCodeViewModel.PrivateMessage = "دوره مشخص نشده است !";
                return validationDiscountCodeViewModel;
            }
            Course course = await _PLContext.Courses.Include(r => r.CourseUsers).FirstOrDefaultAsync(f => f.Course_Id == CourseId);
            if (course == null)
            {
                validationDiscountCodeViewModel.Message = "دوره وجود ندارد !";
                validationDiscountCodeViewModel.PrivateMessage = "دوره وجود ندارد !";
                return validationDiscountCodeViewModel;
            }
            if (string.IsNullOrEmpty(Code))
            {
                validationDiscountCodeViewModel.Message = "کد تخفیف مشخص نشده است !";
                validationDiscountCodeViewModel.PrivateMessage = "کد تخفیف مشخص نشده است !";
                return validationDiscountCodeViewModel;
            }
            StaticDiscount staticDiscount = await GetStaticDiscountByCodeAsync(Code).ConfigureAwait(false);
            if (staticDiscount == null)
            {
                validationDiscountCodeViewModel.Message = "کد تخفیف نامعتبر است !";
                validationDiscountCodeViewModel.PrivateMessage = "کد تخفیف نامعتبر است !";
                return validationDiscountCodeViewModel;
            }
            if (!staticDiscount.SD_IsGeneral)
            {
                if (!staticDiscount.UserRoleStaticDiscounts.Any(a => a.UserRole == LoginUserRole))
                {
                    validationDiscountCodeViewModel.Message = "کد تخفیف نامعتبر است !";
                    validationDiscountCodeViewModel.PrivateMessage = "کد تخفیف متعلق به کاربر نیست !";
                    return validationDiscountCodeViewModel;
                }
            }

            if (staticDiscount.SD_IsActive == false)
            {
                validationDiscountCodeViewModel.Message = "کد تخفیف نامعتبر است !";
                validationDiscountCodeViewModel.PrivateMessage = "کد تخفیف هنوز فعال نشده است !";
                return validationDiscountCodeViewModel;
            }
            if (staticDiscount.SD_StartDate != null)
            {
                if (staticDiscount.SD_StartDate > DateTime.Now)
                {
                    validationDiscountCodeViewModel.Message = "زمان شروع استفاده از کد " + " " + staticDiscount.SD_StartDate.ToShamsiN_WithTime() + " " + "است !";
                    validationDiscountCodeViewModel.PrivateMessage = "زمان شروع استفاده از کد " + " " + staticDiscount.SD_StartDate.ToShamsiN_WithTime() + " " + "است !";
                    return validationDiscountCodeViewModel;
                }
            }
            if (staticDiscount.SD_EndDate != null)
            {
                if (staticDiscount.SD_EndDate < DateTime.Now)
                {
                    validationDiscountCodeViewModel.Message = "زمان استفاده از کد تخفیف به پایان رسیده است !";
                    validationDiscountCodeViewModel.PrivateMessage = "زمان پایان استفاده از کد " + " " + staticDiscount.SD_StartDate.ToShamsiN_WithTime() + " " + "است !";
                    return validationDiscountCodeViewModel;
                }
            }
            if (staticDiscount.SD_UsableCount != null)
            {
                if (staticDiscount.SD_UsableCount == staticDiscount.SD_Used)
                {
                    validationDiscountCodeViewModel.Message = "اعتبار کد تخفیف به پایان رسیده است !";
                    validationDiscountCodeViewModel.PrivateMessage = "تعداد قابل استفاده از کد به پایان رسیده است !";
                    return validationDiscountCodeViewModel;
                }
            }
            if (staticDiscount.SD_MinCourseValue != null)
            {
                if (course.Course_Fee < staticDiscount.SD_MinCourseValue)
                {
                    validationDiscountCodeViewModel.Message = "کد تخفیف معتبر نیست !";
                    validationDiscountCodeViewModel.PrivateMessage = "مبلغ دوره از حداقل مبلغ مجاز تخفیف کمتر است !";
                    return validationDiscountCodeViewModel;
                }
            }
            if (staticDiscount.SD_MaxCourseValue != null)
            {
                if (course.Course_Fee > staticDiscount.SD_MaxCourseValue)
                {
                    validationDiscountCodeViewModel.Message = "کد تخفیف معتبر نیست !";
                    validationDiscountCodeViewModel.PrivateMessage = "مبلغ دوره از حداکثر مبلغ مجاز تخفیف بیشتر است !";
                    return validationDiscountCodeViewModel;
                }
            }




            validationDiscountCodeViewModel.IsValid = true;
            validationDiscountCodeViewModel.Message = "کد تخفیف معتبر است";
            return validationDiscountCodeViewModel;
        }

        public async Task<int> UserDiscountCodeUsage(string disCode, int urId)
        {
            List<UserRoleStaticDiscount> userRoleStaticDiscounts = await _PLContext.UserRoleStaticDiscounts.Include(r => r.UserRole).Include(r => r.StaticDiscount)
                 .Where(w => w.URId == urId && w.StaticDiscount.SD_Code.Trim().ToLower() == disCode.Trim().ToLower()).ToListAsync();
            return userRoleStaticDiscounts.Count();
        }
        #endregion StaticDiscount

    }
}
