using Microsoft.EntityFrameworkCore;
using PLCore.DTOs.Account;
using PLCore.Security;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using PLDataLayer.Entities.Permissions;
using PLDataLayer.Entities.User;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCore.Services
{
    public class UserService : IUserService
    {
        private readonly PLContext _PLContext;
        public UserService(PLContext PLContext)
        {
            _PLContext = PLContext;
        }
        #region User
        public async Task CreateUserAsync(User user)
        {
            if (!_PLContext.Users.Any(a => a.UserNC == user.UserNC))
            {
                await _PLContext.Users.AddAsync(user);
            }
        }
        public void CreateUser(User user)
        {
            if (!_PLContext.Users.Any(a => a.UserNC == user.UserNC))
            {
                _PLContext.Users.Add(user);
            }
            
        }

        public void UpdateUser(User user)
        {
            _PLContext.Users.Update(user);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _PLContext.Users.Include(r => r.County).Include(r => r.County.State).ToListAsync();
        }

        public async Task<List<User>> GetUserByPassword(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                string pass = PasswordHelper.EncodePasswordMd5(password.Trim());
                return await _PLContext.Users.Where(w => w.UserPassword == pass).ToListAsync();
            }
            return null;

        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _PLContext.Users.Include(r => r.UserRoles).Include(r => r.County).Include(r => r.County.State).Include(r => r.UserLevel)
                .SingleOrDefaultAsync(s => s.UserName == userName.Trim());
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _PLContext.Users.Include(r => r.UserRoles).Include(r => r.County).Include(r => r.County.State).Include(r => r.UserLevel)

                .SingleOrDefaultAsync(s => s.UserId == id);
        }
        public async Task<bool> ExistUserNC(string NC)
        {
            if (PLCore.Utility.PLUtility.IsValidNC(NC) == false)
            {
                return false;
            }
            else
            {
                User userNC = await _PLContext.Users.FirstOrDefaultAsync(f => f.UserNC == NC);
                if (userNC == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public async Task<bool> ExistUserCellphone(string cellphone)
        {
            User UserCell = await _PLContext.Users.FirstOrDefaultAsync(f => f.UserCellPhone == cellphone);
            if (UserCell == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<User> GetUserByNC(string NC)
        {
            return await _PLContext.Users.Include(r => r.County).Include(r => r.County.State)
                .SingleOrDefaultAsync(s => s.UserNC == NC);
        }
        #endregion User
        #region Role
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _PLContext.Roles.FindAsync(id);
        }
        public async Task AddPermissionsToRoleAsync(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                await _PLContext.RolePermissions.AddAsync(new RolePermission()
                {
                    PermissionId = p,
                    RoleId = roleId
                });
            }
        }
        public async Task<Role> CreateRoleAsync(Role role, List<int> permissions)
        {
            await _PLContext.AddAsync(role);
            await AddPermissionsToRoleAsync(role.RoleId, permissions);
            return role;
        }
        public void CreateRole(Role role)
        {
            _PLContext.Roles.Add(role);
        }
        public Role EditRole(Role role)
        {
            _PLContext.Roles.Update(role);
            return role;
        }
        public async Task UpdatePermissions_of_RoleAsync(int roleId, List<int> permissionsId)
        {
            //_PLContext.RolePermissions.Where(p => p.RoleId == roleId).ToList().ForEach(p => _PLContext.RolePermissions.Remove(p));
            List<int> CurPer = await _PLContext.RolePermissions.Where(w => w.RoleId == roleId).Select(s => s.Permission.PermissionId).ToListAsync();
            List<int> RemovePer = CurPer.Except(permissionsId).ToList();
            List<int> AddPer = permissionsId.Except(CurPer).ToList();
            if (RemovePer != null && RemovePer.Count() != 0)
            {
                foreach (var p in RemovePer)
                {
                    RolePermission rolePermission = await _PLContext.RolePermissions.SingleOrDefaultAsync(s => s.RoleId == roleId && s.PermissionId == p);
                    _PLContext.RolePermissions.Remove(rolePermission);

                }
            }
            if (AddPer != null && AddPer.Count() != 0)
            {
                await AddPermissionsToRoleAsync(roleId, AddPer);
            }

        }
        public async Task<List<User>> GetUsers_of_RoleAsync(int roleId)
        {
            List<UserRole> userRoles = await _PLContext.UserRoles.Include(r => r.User).Include(r => r.Role)
                .Include(r => r.User.County).Include(r => r.User.County.State)
                .Where(w => w.RoleId == roleId).ToListAsync();
            return userRoles.Select(s => s.User).ToList();
        }
        public async Task DeleteRoleAsync(int roleId)
        {
            Role role = await _PLContext.Roles.SingleOrDefaultAsync(r => r.RoleId == roleId);
            _PLContext.Roles.Remove(role);
        }

        public Task<List<Role>> GetAllRolesofUserWithNCAsync(string NC)
        {
            if (!string.IsNullOrEmpty(NC))
            {
                return _PLContext.UserRoles.Include(r => r.User).Include(r => r.Role).Where(w => w.User.UserNC == NC).Select(s => s.Role).ToListAsync();
            }
            return null;
        }
        public async Task<List<UserRole>> GetUserRoles_of_RoleAsync(int roleId)
        {
            return await _PLContext.UserRoles.Include(r => r.User).Include(r => r.Role).Include(r => r.User.County).Include(r => r.User.County.State)
                .Where(w => w.RoleId == roleId).ToListAsync();
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _PLContext.Roles.SingleOrDefaultAsync(f => f.RoleName == roleName.Trim());
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _PLContext.Roles.Include(r => r.UserRoles).ToListAsync();
        }
        #endregion Role
        #region UserRole
        public async Task<List<Role>> Get_User_Roles_ByNC_Async(string NC)
        {
            User user = await _PLContext.Users.SingleOrDefaultAsync(s => s.UserNC == NC);
            if (user == null)
            {
                return null;
            }
            else
            {
                List<UserRole> userRoles = await _PLContext.UserRoles.Where(w => w.UserId == user.UserId).Include(r => r.User).Include(r => r.Role).ToListAsync();
                if (userRoles != null)
                {
                    List<Role> roles = userRoles.Select(s => s.Role).ToList();
                    return roles;
                }
                return null;

            }
        }
        public async Task<List<Role>> Get_User_Roles_ByName_Async(string Name)
        {
            User user = await _PLContext.Users.SingleOrDefaultAsync(s => s.UserName.Trim() == Name);
            if (user == null)
            {
                return null;
            }
            else
            {
                List<UserRole> userRoles = await _PLContext.UserRoles.Where(w => w.UserId == user.UserId).Include(r => r.User).Include(r => r.Role).ToListAsync();
                if (userRoles != null)
                {
                    List<Role> roles = userRoles.Select(s => s.Role).ToList();
                    return roles;
                }
                return null;

            }
        }
        public async Task<UserRole> GetUserRoleBy_UserName_RoleId(string UserName, int RoleId)
        {
            User user = await _PLContext.Users.SingleOrDefaultAsync(s => s.UserName == UserName);
            if (user == null)
            {
                return null;
            }
            Role role = await _PLContext.Roles.SingleOrDefaultAsync(s => s.RoleId == RoleId);
            if (role == null)
            {
                return null;
            }
            UserRole userRole = await _PLContext.UserRoles.FirstOrDefaultAsync(f => f.RoleId == RoleId && f.UserId == user.UserId);
            if (userRole == null)
            {
                return null;
            }
            return userRole;
        }

        public async Task<List<UserRole>> GetUserRoles()
        {
            return await _PLContext.UserRoles
                .Include(r => r.User).Include(r => r.Role)
                .ToListAsync();
        }


        public void CreateUserRole(UserRole userRole)
        {
            if (!_PLContext.UserRoles.Any(a => a.UserId == userRole.UserId && a.RoleId == userRole.RoleId))
            {
                _PLContext.UserRoles.Add(userRole);
            }
            
        }

        public async Task CreateUserRoleAsync(UserRole userRole)
        {
            if (!_PLContext.UserRoles.Any(a => a.UserId == userRole.UserId && a.RoleId == userRole.RoleId))
            {
                await _PLContext.UserRoles.AddAsync(userRole);
            }
            
        }

        public async Task<UserRole> GetUserRoleByIdAsync(int id)
        {
            UserRole userRole = await _PLContext.UserRoles.Include(r => r.User).Include(r => r.Role)
                .Include(r => r.CourseUsers).Include(r => r.CourseUsers)
                .SingleOrDefaultAsync(s => s.URId == id).ConfigureAwait(false);
            return userRole;
        }
        public void UpdateUserRole(UserRole userRole)
        {
            _PLContext.UserRoles.Update(userRole);
        }

        #endregion UserRole
        #region UserLevel
        public async Task<List<UserLevel>> UserLevels()
        {
            return await _PLContext.UserLevels.ToListAsync();
        }
        public async Task<bool> UserLevel_HasUploadAsync(int id)
        {
            UserLevel userLevel = await _PLContext.UserLevels.FindAsync(id);
            if (userLevel != null)
            {
                if (userLevel.UserLevel_HasUpload)
                {
                    return true;
                }
            }
            return false;

        }

        public async Task<List<UserRole>> GetTeachersAsync()
        {
            return await _PLContext.UserRoles.Include(r => r.User).Include(r => r.Role)
                .Where(w => w.RoleId == 3).ToListAsync();
        }

        public async Task<List<UserRole>> GetSchoolsAsync()
        {
            return await _PLContext.UserRoles.Include(r => r.User).Include(r => r.Role)
                 .Where(w => w.RoleId == 4).ToListAsync();
        }
        #endregion UserLevel
        #region Permissions
        public async Task<List<Permission>> GetAllPermissions()
        {
            return await _PLContext.Permissions.Include(r => r.Permissions).ToListAsync();
        }
        public async Task<List<Permission>> GetPermissions_of_RoleByRoleId(int roleId)
        {
            return await _PLContext.RolePermissions
               .Where(r => r.RoleId == roleId)
               .Select(s => s.Permission).ToListAsync();
        }
        public bool CheckPermission(int permissionId, string userName, int roleId)
        {
            int userId = _PLContext.Users.FirstOrDefault(u => u.UserName == userName).UserId;
            Role role = _PLContext.Roles.FirstOrDefault(s => s.RoleId == roleId);
            RolePermission rolePermission = _PLContext.RolePermissions
                .FirstOrDefault(p => p.PermissionId == permissionId && p.RoleId == roleId);
            if (rolePermission == null)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
        #endregion Permissions
        #region General

        public async Task SaveChangesAsync()
        {
            await _PLContext.SaveChangesAsync();
        }

        public void SaveChange()
        {
            _PLContext.SaveChanges();
        }



        public bool SendMessage(SendMessageViewModel sendMessageViewModel)
        {
            if (string.IsNullOrEmpty(sendMessageViewModel.Key) && string.IsNullOrEmpty(sendMessageViewModel.SecurityCode) && string.IsNullOrEmpty(sendMessageViewModel.SMSirLineNumber))
            {
                sendMessageViewModel.Key = "2027ea4381a2e4def2bf654";
                sendMessageViewModel.SecurityCode = "@#rth@123456#";
                sendMessageViewModel.SMSirLineNumber = "50002015775882";
            }
            var messageSendObject = new MessageSendObject()
            {

                Messages = sendMessageViewModel.Messages.ToArray(),
                MobileNumbers = sendMessageViewModel.MobileNumbers.ToArray(),
                LineNumber = sendMessageViewModel.SMSirLineNumber,
                SendDateTime = DateTime.Now,
                CanContinueInCaseOfError = false
            };
            SmsIrRestfulNetCore.Token token = new Token();
            string result = token.GetToken(sendMessageViewModel.Key, sendMessageViewModel.SecurityCode);
            if (!string.IsNullOrEmpty(result))
            {
                SmsIrRestfulNetCore.MessageSendResponseObject MessageSendResponseObject = new MessageSend().Send(result, messageSendObject);
                return MessageSendResponseObject.IsSuccessful;
            }
            else
            {
                return false;
            }

        }


        public bool SendVerificationCode(string code, string phoneNumber)
        {
            var token = new Token().GetToken("2027ea4381a2e4def2bf654", "@#rth@123456#");

            var restVerificationCode = new RestVerificationCode()
            {
                Code = code,
                MobileNumber = phoneNumber
            };

            var restVerificationCodeRespone = new VerificationCode().Send(token, restVerificationCode);
            if (restVerificationCode!=null)
            {
                if (restVerificationCodeRespone.IsSuccessful)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        public bool SendPassword(string pass, string phoneNumber)
        {
            var token = new Token().GetToken("2027ea4381a2e4def2bf654", "@#rth@123456#");

            var ultraFastSend = new UltraFastSend()
            {
                Mobile = long.Parse(phoneNumber),
                TemplateId = 22819,
                ParameterArray = new List<UltraFastParameters>()
    {
        new UltraFastParameters()
        {
            Parameter = "password" , ParameterValue = pass
        }
    }.ToArray()

            };

            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            if (ultraFastSendRespone.IsSuccessful)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        #endregion General
    }
}
