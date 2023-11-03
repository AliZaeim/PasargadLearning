using PLCore.DTOs.Course;
using PLCore.DTOs.General;
using PLDataLayer.Entities.Sale;
using PLDataLayer.Entities.Training;
using PLDataLayer.Entities.User;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PLCore.Services.Interfaces
{
    public interface ITrainingService
    {
        #region CourseGroup
        public Task<CourseGroup> CreateCourseGroupAsync(CourseGroup courseGroup);
        public void UpdateCourseGroup(CourseGroup CourseGroup);
        public Task<List<CourseGroup>> GetCourseGroupsAsync();
        public Task<CourseGroup> GetCourseGroupByIdAsync(int id);
        public bool ExistCourseGroup(int id);
        #endregion CourseGroup
        #region Course
        public Task CreateCourseAsync(Course course);
        public void CreateCourse(Course course);
        public void UpdateCourse(Course course);
        public Task<bool> RemoveCourseAsync(int id);
        public Task<Course> GetCourseAsync(int id);
        public Task<List<Course>> GetCoursesAsync();
        public bool IsExistCourse(int id);
        public Task<List<Course>> GetCoursesByGroupId(int gid);
        public Task<List<Course>> GetCoursesByMounth(int year, int mounth);
        public Task<List<Course>> Search_in_Courses(string srch);
        public Task<(bool CanReg, string Message)> ValidateRegisterinCourse(int cid, int urId);
        public Task<int> UpdateCourseVisits(int id);
        public Task<List<Course>> GetCoursesByRole(int urId);
        public Task<List<CourseUser>> GetCourseUsersByRoleAsync(int cId, int roleId);
        public Task<CourseStatusResult> GetCourseStatusResult(int cId);
        public Task RemoveUserFromCourse(int cId, int urId);
        #endregion Course
        #region CourseLevel
        public Task<List<CourseLevel>> GetCourseLevelsAsync();
        #endregion CourseLevel
        #region CourseStatus
        public Task<List<CourseStatus>> GetCourseStatusesAsync();
        #endregion CourseStatus
        #region CourseUser
        public void CreateCourseUser(CourseUser courseUser);
        public Task<CourseUser> GetCourseUserByIdAsync(int id);
        public Task<List<UserRole>> GetCourseUsersByRoleAsync(List<CourseUser> courseUsers, int roleId, int? cId);
        public void UpdateCourseUser(CourseUser courseUser);
        public void RemoveCourseUser(CourseUser courseUser);
        public Task<CourseUser> GetCourseUserBy_URId_CourseId(int UrId, int courseId);
        public Task<UserRole> GetCourseTeacherAsync(int courseId);
        public Task<List<CourseUser>> GetAllCourse_UsersAsync();
        public Task<List<CourseUser>> GetAllCourse_TeachersAsync();
        public Task<List<CourseFile>> GetCourseFiles();
        public Task<int> GetCourseFilesCount();
        #endregion CourseUser
        #region CourseFile
        public void CreateCourseFile(CourseFile courseFile);
        public void UpdateCourseFile(CourseFile courseFile);
        public Task<CourseFile> GetCourseFileByIdAsync(int id);
        public void RemoveCourseFile(CourseFile courseFile);
        public Task<List<CourseFile>> GetCourseFilesAsync();
        public Task<List<CourseFile>> GetCourseFilesByType_Cid(string type, int courseId);
        #endregion
        #region CourseTypeofMeasurment
        public Task<List<CourseTypeofMeasurment>> GetCourseTypeofMeasurments();
        public Task<CourseTypeofMeasurment> GetCourseTypeofMeasurmentAsync(int tmId);
        #endregion
        #region SteppedDiscountType
        public void CreateSteppedDiscountType(SteppedDiscountType steppedDiscountType);
        public void EditSteppedDiscountType(SteppedDiscountType steppedDiscountType);
        public Task<List<SteppedDiscountType>> GetSteppedDiscountTypesAsync();
        public Task<SteppedDiscountType> GetSteppedDiscountTypeByIdAsync(int id);
        public void RemoveSteppedDiscountType(int id);
        public bool ExistSteppedDiscountType(int id);
        #endregion
        #region SteppedDiscount
        public void CreateSteppedDiscount(SteppedDiscount steppedDiscount);        
        public void EditSteppedDiscount(SteppedDiscount steppedDiscount);
        public Task<List<SteppedDiscount>> GetSteppedDiscountsAsync();
        public List<SteppedDiscount> GetSteppedDiscounts();
        public Task<SteppedDiscount> GetSteppedDiscountByIdAsync(int id);
        public void RemoveSteppedDiscount(int id);
        public bool ExistSteppedDiscount(int id);
        public Task<bool> ExistSteppedDiscount(string code);
        public Task<SteppedDiscount> GetSteppedDiscountByCodeAsync(string code);
        public Task<List<SteppedDiscount>> GetValidSteppedDiscounts();
        public Task<SteppedDiscountDetail> GetValidDetailofSteppedDiscountAsync(string code, int nubat);
        public Task<List<Course>> GetSteppedDiscountCoursesByCode(string Discode);
        public Task<List<SteppedDiscountStatusViewModel>> SteppedDiscountStatusForCourseAsync(string DisCode, int courseId);
        #endregion
        #region SteppedDiscountDetail
        public void CreateSteppedDiscountDetail(SteppedDiscountDetail steppedDiscountDetail);
        public void EditSteppedDiscountDetail(SteppedDiscountDetail steppedDiscountDetail);
        public Task<List<SteppedDiscountDetail>> GetSteppedDiscountDetailsAsync();
        public List<SteppedDiscountDetail> GetSteppedDiscountDetails();
        public Task<List<SteppedDiscountDetail>> GetSteppedDiscountDetailsAsync(string code);
        public Task<SteppedDiscountDetail> GetSteppedDiscountDetailByIdAsync(int id);
        public void RemoveSteppedDiscountDetail(int id);
        public bool ExistSteppedDiscountDetail(int id);
        #endregion
        #region StaticDiscount
        public Task AddCoursesToStaticDiscount(int SDId, List<int> Course_Ids);
        public Task AddUserRolesToStaticDiscount(int SDId, List<int> UserRole_Ids);
        public void CreateStaticDiscount(StaticDiscount staticDiscount);
        public Task<bool> CreateStaticDiscount(StaticDiscount staticDiscount, List<int> SelectedUserRoles, List<int> SelectedCourses);
        public void EditStaticDiscount(StaticDiscount staticDiscount);
        public Task UpdateUserRolesofStaticDiscount(int SDId, List<int> UserRole_Ids);
        public Task UpdateCoursesofStaticDiscount(int SDId, List<int> Course_Ids);
        public bool RemoveStaticDiscount(int id);
        public Task<StaticDiscount> GetStaticDiscountByIdAsync(int id);
        public Task<StaticDiscount> GetStaticDiscountByCodeAsync(string code);
        public Task<List<StaticDiscount>> GetStaticDiscountsAsync(int urId);
        
        public List<StaticDiscount> GetStaticDiscounts();
        public Task<bool> ExistStaticDiscountAsync(string code);
        public bool ExistStaticDiscount(int id);
        public List<string> GetStaticDiscountCodes();
        public Task<List<string>> GetStaticDiscountCodesAsync();
        public Task<ValidationDiscountCodeViewModel> ValidationDiscountCode(string Code, int urId, int? CourseId);
        public Task<int> UserDiscountCodeUsage(string DisCode,int urId);
        #endregion StaticDiscount

        #region General
        public void Save();
        public Task SaveAsync();
        #endregion
    }
}
