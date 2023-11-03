using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using PLCore.Convertors;
using PLCore.DTOs.Account;
using PLCore.DTOs.General;
using PLCore.DTOs.PUser;
using PLCore.Generators;
using PLCore.Security;

using PLCore.Services.Interfaces;
using PLCore.Utility;
using PLDataLayer.Entities.User;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISubEntityService _subEntityService;
        public UsersController(IUserService userService, ISubEntityService subEntityService)
        {
            _userService = userService;
            _subEntityService = subEntityService;
        }
        #region Admin
        [Authorize]
        [PermissionChecker(51)]
        public async Task<IActionResult> GetAllUsers()
        {
            return View(await _userService.GetUserRoles());
        }
        [Authorize]
        [PermissionChecker(52)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("کاربری یافت نشد !");
            }

            var user = await _userService.GetUserByIdAsync((int)id);
            if (user == null)
            {
                return NotFound("کاربری یافت نشد !");
            }

            return View(user);
        }
        [Authorize]
        [PermissionChecker(52)]
        public async Task<IActionResult> Edit(int id)
        {
            User user = await _userService.GetUserByIdAsync(id).ConfigureAwait(false);

            UpdateUserViewModel updateUserViewModel = new UpdateUserViewModel()
            {
                UserId = user.UserId
            };
            DateTime bdt = DateTime.Now;
            string birthdt = string.Empty;
            
            //if (user.UserBirthDate != null)           
            //{                
            //    bdt = Convert.ToDateTime(user.UserBirthDate.Value.Date);               
            //    birthdt = bdt.ToShamsi();
            //}
           
            updateUserViewModel.UserNC = user.UserNC;
            updateUserViewModel.UserCellPhone = user.UserCellPhone;
            updateUserViewModel.UserName = user.UserName;
            updateUserViewModel.UserFirstName = user.UserFirstName;
            updateUserViewModel.UserFamily = user.UserFamily;
            updateUserViewModel.UserEmail = user.UserEmail;
            updateUserViewModel.UserSex = user.UserSex;
            updateUserViewModel.UserRestAddress = user.UserRestAddress;
            updateUserViewModel.UserAvatar = user.UserAvatar;
            updateUserViewModel.UserBiography = user.UserBiography;
            updateUserViewModel.UserBirthDate = user.UserBirthDate;
            updateUserViewModel.UserFatherName = user.UserFatherName;
            updateUserViewModel.UserIsActive = user.UserIsActive;
            updateUserViewModel.UserLevel_Id = user.UserLevel_Id;
            updateUserViewModel.UserOrgCode = user.UserOrgCode;
            updateUserViewModel.UserUniversity = user.UserUniversity;
            updateUserViewModel.UserYearofGraduataion = user.UserYearofGraduataion;
            updateUserViewModel.LevelOfEducation = user.LevelOfEducation;
            updateUserViewModel.EducationFile = user.EducationFile;
            updateUserViewModel.UserContractImage = user.UserContractFile;
            updateUserViewModel.UserAvatar = user.UserAvatar;
            updateUserViewModel.UserNCImage = user.UserNCFile;
            updateUserViewModel.UserLabel = user.UserLabel;
            updateUserViewModel.UserDescription = user.UserDescription;
            if (user.County != null)
            {
                updateUserViewModel.CountyId = (int)user.CountyId;
                updateUserViewModel.Counties = await _subEntityService.GetCountiesofState(user.County.StateId);
                updateUserViewModel.StateId = user.County.StateId;
            }
            else
            {
                updateUserViewModel.Counties = await _subEntityService.GetCounties();

            }
            if (user.UserLevel != null)
            {
                if (user.UserLevel.UserLevel_HasUpload)
                {
                    updateUserViewModel.ContractUploade = true;
                }
            }

            updateUserViewModel.States = await _subEntityService.GetStates();
            updateUserViewModel.UserLevels = await _userService.UserLevels();

            return View(updateUserViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [PermissionChecker(52)]
        public async Task<IActionResult> Edit(int id, UpdateUserViewModel updateUserViewModel, IFormFile UserAvatar, IFormFile EducationFile, IFormFile UserNCImage, IFormFile UserContractImage)
        {
            if (id != updateUserViewModel.UserId)
            {
                return NotFound();
            }
            if (updateUserViewModel.StateId != null)
            {
                updateUserViewModel.Counties = await _subEntityService.GetCountiesofState((int)updateUserViewModel.StateId);
                updateUserViewModel.States = await _subEntityService.GetStates();
            }
            else
            {
                updateUserViewModel.Counties = await _subEntityService.GetCountiesofState(1);
                updateUserViewModel.States = await _subEntityService.GetStates();
            }
            
            updateUserViewModel.UserLevels = await _userService.UserLevels();
            if (!ModelState.IsValid)
            {
                return View(updateUserViewModel);
            }

            if (UserAvatar != null)
            {
                if (UserAvatar.Length > .05 * 1024 * 1024)
                {
                    ModelState.AddModelError("UserAvatar", "تصویر پرسنلی بزرگتر از 50 کیلوبایت است !");
                    return View(updateUserViewModel);
                }
            }
            if (EducationFile != null)
            {
                if (EducationFile.Length > 0.5 * 1024 * 1024)
                {
                    ModelState.AddModelError("EducationFile", "عکس مدرک تحصیلی بیشتر از 50 کیلو بایت است !");
                    return View(updateUserViewModel);
                }
            }
            if (UserNCImage != null)
            {
                if (UserNCImage.Length > 0.5 * 1024 * 1024)
                {
                    ModelState.AddModelError("UserNCImage", "عکس کارت ملی بیشتر از 50 کیلو بایت است !");
                    return View(updateUserViewModel);
                }
            }
            if (UserContractImage != null)
            {
                if (UserContractImage.Length > 0.5 * 1024 * 1024)
                {
                    ModelState.AddModelError("UserNCImage", "عکس قرارداد سازمانی بیشتر از 50 کیلو بایت است !");
                    return View(updateUserViewModel);
                }
            }
            User user = await _userService.GetUserByIdAsync(updateUserViewModel.UserId);
            user.UserName = updateUserViewModel.UserName;
            user.UserFirstName = updateUserViewModel.UserFirstName;
            user.UserFamily = updateUserViewModel.UserFamily;
            user.UserFatherName = updateUserViewModel.UserFatherName;
            user.UserEmail = updateUserViewModel.UserEmail;
            user.UserCellPhone = updateUserViewModel.UserCellPhone;
            user.UserLevel_Id = updateUserViewModel.UserLevel_Id;
            user.UserNC = updateUserViewModel.UserNC;
            user.UserSex = updateUserViewModel.UserSex;
            user.UserOrgCode = updateUserViewModel.UserOrgCode;
            user.UserRestAddress = updateUserViewModel.UserRestAddress;
            user.UserUniversity = updateUserViewModel.UserUniversity;
            user.UserYearofGraduataion = updateUserViewModel.UserYearofGraduataion;
            user.UserIsActive = updateUserViewModel.UserIsActive;
            user.UserBirthDate = updateUserViewModel.UserBirthDate;
            user.UserBiography = updateUserViewModel.UserBiography;
            user.UserLabel = updateUserViewModel.UserLabel;
            user.UserDescription = updateUserViewModel.UserDescription;
            if (updateUserViewModel.Skyuser_id != null)
            {
                user.Sky_userId = (int)updateUserViewModel.Skyuser_id;
            }
            
            user.OP_Update += "-" + User.Identity.Name + "|" + DateTime.Now.ToShamsiWithTime();
            if (UserNCImage != null)
            {
                if (!string.IsNullOrEmpty(user.UserNCFile))
                {
                    string nowimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersnc", user.UserNCFile);
                    if (System.IO.File.Exists(nowimagePath))
                    {
                        System.IO.File.Delete(nowimagePath);
                    }
                }
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersnc", UserNCImage.FileName);
                string ncimegeName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(UserNCImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersnc", ncimegeName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    UserNCImage.CopyTo(stream);
                }
                user.UserNCFile = ncimegeName;
            }
            if (UserAvatar != null)
            {
                if (!string.IsNullOrEmpty(user.UserAvatar))
                {
                    string nowimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersavatar", user.UserAvatar);
                    if (System.IO.File.Exists(nowimagePath))
                    {
                        System.IO.File.Delete(nowimagePath);
                    }
                }
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersavatar", UserAvatar.FileName);
                string avimegeName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersavatar", avimegeName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    UserAvatar.CopyTo(stream);
                }
                user.UserAvatar = avimegeName;
            }
            if (EducationFile != null)
            {
                if (!string.IsNullOrEmpty(user.EducationFile))
                {
                    string nowimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersorgdoc", user.EducationFile);
                    if (System.IO.File.Exists(nowimagePath))
                    {
                        System.IO.File.Delete(nowimagePath);
                    }
                }
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersorgdoc", EducationFile.FileName);
                string EdimegeName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(EducationFile.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersorgdoc", EdimegeName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    EducationFile.CopyTo(stream);
                }
                user.EducationFile = EdimegeName;
            }
            if (UserContractImage != null)
            {
                if (!string.IsNullOrEmpty(user.UserContractFile))
                {
                    string nowimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersorgdoc", user.UserContractFile);
                    if (System.IO.File.Exists(nowimagePath))
                    {
                        System.IO.File.Delete(nowimagePath);
                    }
                }
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersorgdoc", UserContractImage.FileName);
                string ConimegeName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(UserContractImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersorgdoc", ConimegeName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    UserContractImage.CopyTo(stream);
                }
                user.UserContractFile = ConimegeName;
            }
            _userService.UpdateUser(user);
            
            await _userService.SaveChangesAsync();
            return RedirectToAction("GetAllUsers");
        }

        public async Task<IActionResult> UpdateTeacherInfo(int? urId)
        {
            if (urId == null)
            {
                return NotFound("کاربر مشخص نشده است !");
            }
            UserRole userRole = await _userService.GetUserRoleByIdAsync((int)urId);
            UpdateTaecherSkyInfo updateTeacherSkyInfo = new UpdateTaecherSkyInfo()
            {
                Sky_roomId = userRole.room_id,
                Sky_roomLink = userRole.RoomLink,
                UserRole = userRole
            };
            return View(updateTeacherSkyInfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTeacherInfo(UpdateTaecherSkyInfo updateTaecherSkyInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(updateTaecherSkyInfo);
            }
            UserRole userRole = await _userService.GetUserRoleByIdAsync(updateTaecherSkyInfo.urId);
            userRole.room_id = updateTaecherSkyInfo.Sky_roomId;
            userRole.RoomLink = updateTaecherSkyInfo.Sky_roomLink;
            _userService.UpdateUserRole(userRole);
            await _userService.SaveChangesAsync();
            return View(nameof(GetAllUsers), await _userService.GetUserRoles());
        }
        public IActionResult CreateOp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateOp(OperatorUserViewModel operatorUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(operatorUserViewModel);
            }
            if (PLUtility.IsValidNC(operatorUserViewModel.UserNC) == false)
            {
                ModelState.AddModelError("UserNC", "کد ملی نامعتبر است !");
                return View(operatorUserViewModel);
            }

            User Exuser = await _userService.GetUserByNC(operatorUserViewModel.UserNC);
            if (Exuser == null)
            {
                if (await _userService.ExistUserCellphone(operatorUserViewModel.UserCellphone))
                {
                    ModelState.AddModelError("UserCellphone", "این شماره تلفن برای فرد دیگری ثبت شده است !");
                    return PartialView(operatorUserViewModel);
                }
                User user = new User()
                {
                    UserFirstName = operatorUserViewModel.UserFirstName,
                    UserFamily = operatorUserViewModel.UserFamily,
                    UserName = operatorUserViewModel.UserCellphone,
                    UserCellPhone = operatorUserViewModel.UserCellphone,
                    UserNC = operatorUserViewModel.UserNC,
                    UserPassword = PasswordHelper.EncodePasswordMd5(operatorUserViewModel.UserNC),
                    LastPassword = operatorUserViewModel.UserNC,
                    UserBirthDate = operatorUserViewModel.UserBirthDate,
                    UserRegisteredDate = DateTime.Now
                };
                _userService.CreateUser(user);
                await _userService.SaveChangesAsync();
                UserRole userRole = new UserRole()
                {
                    User = user,
                    RoleId = 2,
                    RegisterDate = DateTime.Now,
                    OP_Create = "-" + User.Identity.Name + DateTime.Now
                };
                TempData["message"] = "کاربر با نقش اپراتور ثبت شد";
                _userService.CreateUserRole(userRole);
                _userService.SaveChange();
            }
            else
            {
                List<Role> roles = await _userService.GetAllRolesofUserWithNCAsync(operatorUserViewModel.UserNC);
                if (Exuser.UserFirstName.Trim() != operatorUserViewModel.UserFirstName.Trim() || Exuser.UserFamily.Trim() != operatorUserViewModel.UserFamily.Trim())
                {
                    ModelState.AddModelError("UserNC", "این کد ملی متعلق به کاربر دیگری می باشد !");
                    return View(operatorUserViewModel);
                }
                if (roles.Any(a => a.RoleId == 2))
                {
                    ModelState.AddModelError("UserNC", "این کاربر دارای نقش اپراتور می باشد !");
                    return View(operatorUserViewModel);
                }
                else
                {
                    UserRole userRole = new UserRole()
                    {
                        User = Exuser,
                        RoleId = 2,
                        RegisterDate = DateTime.Now,
                        OP_Create = "-" + User.Identity.Name + DateTime.Now
                    };
                    _userService.CreateUserRole(userRole);
                    _userService.SaveChange();
                    TempData["message"] = "نقش اپراتور به کاربر اضافه شد";
                }

            }

            return View(nameof(GetAllUsers), await _userService.GetUserRoles());
        }
        public async Task<IActionResult> AcDeUserRole(string userName, int roleId, bool act)
        {
            UserRole userRole = await _userService.GetUserRoleBy_UserName_RoleId(userName, roleId);
            userRole.IsActive = act;
            _userService.UpdateUserRole(userRole);
            await _userService.SaveChangesAsync();
            Role role = await _userService.GetRoleByIdAsync(roleId);
            return PartialView((role,act));
        }
        public async Task<bool> AcDeUser(string userName, bool act)
        {
            User user = await _userService.GetUserByUserName(userName);
            if(user==null)
            {
                return false;
            }
            user.UserIsActive = act;
            _userService.UpdateUser(user);
            await _userService.SaveChangesAsync();
            
            return true;
        }
        #endregion Admin
        #region Users
        [Authorize]
        public async Task<IActionResult> ShowProfile()
        {

            User user = await _userService.GetUserByUserName(User.Identity.Name).ConfigureAwait(false);
            return View(user);
        }
        [Authorize]
        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel()
            {
                IsSuccess = false
            };
            return View(changePasswordViewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordViewModel);
            }

            User user = await _userService.GetUserByUserName(User.Identity.Name);
            if (user.UserPassword != PasswordHelper.EncodePasswordMd5(changePasswordViewModel.Password))
            {
                ModelState.AddModelError("Password", "رمز عبور فعلی نادرست است !");
            }
            user.UserPassword = PasswordHelper.EncodePasswordMd5(changePasswordViewModel.NewPassowrd);
            user.LastPassword = changePasswordViewModel.NewPassowrd;
            user.OP_Update += "-" + User.Identity.Name + "|" + DateTime.Now;
            _userService.UpdateUser(user);
            await _userService.SaveChangesAsync();
            changePasswordViewModel.IsSuccess = true;
            return View(changePasswordViewModel);
        }
        [Route("UpdateInfo")]
        public async Task<IActionResult> UpdateUserInfo()
        {
            User user = await _userService.GetUserByUserName(User.Identity.Name);
            UpdateUserInfoViewModel updateUserInfoViewModel = new UpdateUserInfoViewModel()
            {
                OldAvatar = user.UserAvatar
            };
            return View(updateUserInfoViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("UpdateInfo")]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoViewModel updateUserInfoViewModel,IFormFile Avatar)
        {
            if (!ModelState.IsValid)
            {
               return View(updateUserInfoViewModel);
            }
            if (Avatar == null)
            {
                ModelState.AddModelError("Avatar", "لطفا عکس پرسنلی خود را انتخاب کنید !");
                return View(updateUserInfoViewModel);
            }
            User user = await _userService.GetUserByUserName(User.Identity.Name);
            string oldavatar = user.UserAvatar;        
            string fileName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(Avatar.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usersavatar", fileName);
            PLUtility.GetNewDImage(400, 450, Avatar.OpenReadStream(), filePath);
            user.UserAvatar = fileName;
            user.UserBiography = updateUserInfoViewModel.Biography;
            _userService.UpdateUser(user);
            await _userService.SaveChangesAsync();
            if (!string.IsNullOrEmpty(oldavatar))
            {
                if(System.IO.File.Exists("wwwroot/images/usersavatar/" + oldavatar))
                {
                    System.IO.File.Delete("wwwroot/images/usersavatar/" + oldavatar);
                }
            }
            updateUserInfoViewModel.IsSuccess = true;
            updateUserInfoViewModel.OldAvatar = fileName;
            return View(updateUserInfoViewModel);

        }
            #endregion Users

        }
}