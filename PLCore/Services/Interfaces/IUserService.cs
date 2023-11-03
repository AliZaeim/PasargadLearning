using PLCore.DTOs.Account;
using PLDataLayer.Entities.Permissions;
using PLDataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLCore.Services.Interfaces
{
    public interface IUserService
    {
        #region User
        public Task CreateUserAsync(User user);
        public void CreateUser(User user);
        
        public void UpdateUser(User user);
        public Task<User> GetUserByUserName(string userName);
        public Task<List<User>> GetUserByPassword(string password);
        public Task<List<User>> GetAllUsers();
        public Task<bool> ExistUserNC(string NC);
        public Task<bool> ExistUserCellphone(string cellphone);
        public Task<User> GetUserByNC(string NC);
        public Task<User> GetUserByIdAsync(int id);
        #endregion User
        #region Role
        public Task<Role> GetRoleByIdAsync(int id);
        public Task<Role> GetRoleByNameAsync(string roleName);
        public Task<List<Role>> GetAllRolesAsync();
        public Task AddPermissionsToRoleAsync(int roleId, List<int> permission);
        public Role EditRole(Role role);
        public Task DeleteRoleAsync(int roleId);
        public Task<Role> CreateRoleAsync(Role role, List<int> permissions);
        public void CreateRole(Role role);
        public Task UpdatePermissions_of_RoleAsync(int roleId, List<int> permissionsId);
        public Task<List<User>> GetUsers_of_RoleAsync(int roleId);
        public Task<List<UserRole>> GetUserRoles_of_RoleAsync(int roleId);
        public Task<List<Role>> GetAllRolesofUserWithNCAsync(string NC);
        #endregion Role
        #region UserRole
        public Task<UserRole> GetUserRoleBy_UserName_RoleId(string UserName, int RoleId);
        public Task<List<UserRole>> GetUserRoles();
        
        public Task CreateUserRoleAsync(UserRole userRole);
        public void CreateUserRole(UserRole userRole);
        public Task<List<UserRole>> GetTeachersAsync();
        public Task<List<UserRole>> GetSchoolsAsync();
        public Task<List<Role>> Get_User_Roles_ByNC_Async(string NC);
        public Task<List<Role>> Get_User_Roles_ByName_Async(string UserName);
        public Task<UserRole> GetUserRoleByIdAsync(int id);
        public void UpdateUserRole(UserRole userRole);
        #endregion UserRole
        #region UserLevel
        public Task<List<UserLevel>> UserLevels();
        public Task<bool> UserLevel_HasUploadAsync(int id);
        #endregion UserLevel
        #region Permission
        Task<List<Permission>> GetAllPermissions();
        Task<List<Permission>> GetPermissions_of_RoleByRoleId(int roleId);
        bool CheckPermission(int permissionId, string userName, int roleId);
        #endregion
        #region Generic
        public bool SendMessage(SendMessageViewModel sendMessageViewModel);
        public bool SendVerificationCode(string code, string phoneNumber);
        public bool SendPassword(string pass,string phoneNumber);
        public Task SaveChangesAsync();
        public void SaveChange();
        #endregion Generic
    }
}
