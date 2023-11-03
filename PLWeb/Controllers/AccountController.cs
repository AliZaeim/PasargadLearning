using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PLCore.DTOs.Account;
using PLCore.Services.Interfaces;
using PLCore.Utility;
using PLDataLayer.Entities.User;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using PLCore.Generators;
using PLCore.Security;
using System.Globalization;
using PLDataLayer.Entities.Training;
using PLDataLayer.Entities.Sale;
using PLCore.DTOs.General;
using System.Net.Http;
using PLCore.DTOs.SkyRoom;
using Newtonsoft.Json;


namespace PLWeb.Controllers
{
    public class AccountController : Controller
    {
        public static readonly HttpClient HttpClient = new HttpClient();
        private readonly IUserService _userService;
        private readonly ISubEntityService _subEntityService;
        private readonly ITrainingService _tService;
        private readonly ISkyService _skyService;
        public AccountController(IUserService userService, ISubEntityService subEntityService, ITrainingService tService, ISkyService skyService)
        {
            _userService = userService;
            _subEntityService = subEntityService;
            _tService = tService;
            _skyService = skyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Login/{key}")]
        public IActionResult Login(string key, string returl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!User.IsInRole("Student"))
                {
                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return Redirect("/Login/S?returl=" + returl);
                }
            }
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                LoginKey = key,
                LoginReturnPath = returl
            };
            switch (key.Trim().ToLower())
            {
                case "o":
                    {
                        loginViewModel.RoleTitle = "اپراتور";
                        loginViewModel.LoginKey = "o";
                        loginViewModel.RoleId = 2;
                        break;
                    }
                case "t":
                    {
                        loginViewModel.RoleTitle = "مدرس";
                        loginViewModel.LoginKey = "t";
                        loginViewModel.RoleId = 3;
                        break;
                    }
                case "s":
                    {
                        loginViewModel.RoleTitle = "فراگیر";
                        loginViewModel.LoginKey = "s";
                        loginViewModel.RoleId = 4;
                        break;
                    }
                case "pl@admin":
                    {
                        loginViewModel.RoleTitle = "مدیر سایت";
                        loginViewModel.LoginKey = "pl@admin";
                        loginViewModel.RoleId = 1;
                        break;
                    }

                default:
                    loginViewModel.RoleTitle = "فراگیر";
                    loginViewModel.LoginKey = "s";
                    loginViewModel.RoleId = 4;
                    break;
            }
            return View(loginViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login/{key}")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            List<User> usersPass = await _userService.GetUserByPassword(loginViewModel.UserPassword).ConfigureAwait(false);
            if (usersPass == null)
            {
                ModelState.AddModelError("UserName", "نام کاربری یا رمز عبور اشتباه است !");
                return View(loginViewModel);
            }
            User userUN = await _userService.GetUserByUserName(loginViewModel.UserName);
            if (!usersPass.Any(a => a.UserName == loginViewModel.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری یا رمز عبور اشتباه است !");
                return View(loginViewModel);
            }
            if (userUN.UserIsActive == false)
            {
                ModelState.AddModelError("UserName", "کد کاربری شما فعال نمی باشد !");
                return View(loginViewModel);
            }
            UserRole userRole = await _userService.GetUserRoleBy_UserName_RoleId(loginViewModel.UserName, loginViewModel.RoleId);
            if (userRole == null)
            {
                ModelState.AddModelError("UserName", "نام کاربری یا رمز عبور اشتباه است !");
                return View(loginViewModel);
            }
            if (userRole.IsActive == false)
            {
                ModelState.AddModelError("UserName", "کد کاربری شما فعال نمی باشد !");
                return View(loginViewModel);
            }
            Role role = await _userService.GetRoleByIdAsync(loginViewModel.RoleId);
            var claims = new List<Claim>(){
                                        new Claim(ClaimTypes.NameIdentifier,userUN.UserId.ToString()),
                                        new Claim(ClaimTypes.Name,userUN.UserName),
                                        new Claim("MobilePhone",userUN.UserCellPhone),
                                        new Claim("RoleId",loginViewModel.RoleId.ToString()),
                                        new Claim("RoleTitle",role.RoleTitle)

                                    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = loginViewModel.RememberMe
            };
            await HttpContext.SignInAsync(principal, properties);

            if (string.IsNullOrEmpty(loginViewModel.LoginReturnPath))
            {

                return RedirectToAction(nameof(Index), "Home", new { area = "Admin" });
            }
            else
            {
                return Redirect(loginViewModel.LoginReturnPath);
            }

        }
        public async Task<IActionResult> GetCounties(int stateId)
        {
            if (stateId == 0)
            {
                return PartialView(await _subEntityService.GetCountiesofState(1));
            }
            return PartialView(await _subEntityService.GetCountiesofState(stateId));
        }
        public async Task<IActionResult> PrepareFee(int cId, int urId)
        {
            Course course = await _tService.GetCourseAsync(cId);
            if (course == null)
            {
                return NotFound("دوره یافت نشد !");
            }
            int Total = course.Course_Fee;
            int DisVal = 0;
            if (!string.IsNullOrEmpty(course.SteppedDiscountCode))
            {
                SteppedDiscount steppedDiscount = await _tService.GetSteppedDiscountByCodeAsync(course.SteppedDiscountCode).ConfigureAwait(false);
                int nubat = _tService.GetCourseUsersByRoleAsync(course.Course_Id, 4).Result.Count;
                nubat += 1;
                string type = string.Empty;
                if (steppedDiscount != null)
                {
                    if (steppedDiscount.SteppedDiscountType.Name.ToLower().Trim() == "person")
                    {
                        SteppedDiscountDetail steppedDiscountDetail = steppedDiscount.SteppedDiscountDetails.FirstOrDefault(f => nubat >= f.FromPerson && nubat <= f.ToPerson);
                        if (steppedDiscountDetail != null)
                        {
                            DisVal = (int)Math.Round(Total * steppedDiscountDetail.Percent / 100, 0);
                            Total -= DisVal;
                        }
                    }

                    if (steppedDiscount.SteppedDiscountType.Name.ToLower().Trim() == "time")
                    {
                        SteppedDiscountDetail steppedDiscountDetail = steppedDiscount.SteppedDiscountDetails.FirstOrDefault(f => DateTime.Now >= f.FromDate && DateTime.Now <= f.ToDate);
                        if (steppedDiscountDetail != null)
                        {
                            DisVal = (int)Math.Round(Total * steppedDiscountDetail.Percent / 100, 0);
                            Total -= DisVal;
                        }
                    }
                }
            }
            PrepareFeeViewModel prepareFeeViewModel = new PrepareFeeViewModel()
            {
                CourseId = cId,
                urId = urId,
                Fee = Total,
                TotalValue = Total,
                IsValid = true
            };
            return View(prepareFeeViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PrepareFee(PrepareFeeViewModel prepareFeeViewModel)
        {
            Course course = await _tService.GetCourseAsync(prepareFeeViewModel.CourseId);
            int Total = prepareFeeViewModel.TotalValue;
            int DisVal = 0;

            if (!string.IsNullOrEmpty(prepareFeeViewModel.DisCode))
            {
                ValidationDiscountCodeViewModel validationDiscountCodeViewModel = await _tService.ValidationDiscountCode(prepareFeeViewModel.DisCode, prepareFeeViewModel.urId, course.Course_Id).ConfigureAwait(false);
                StaticDiscount staticDiscount = await _tService.GetStaticDiscountByCodeAsync(prepareFeeViewModel.DisCode.Trim().ToLower()).ConfigureAwait(false);
                if (!validationDiscountCodeViewModel.IsValid)
                {
                    ModelState.AddModelError("DisCode", validationDiscountCodeViewModel.Message);
                    prepareFeeViewModel.IsValid = false;
                    return View(prepareFeeViewModel);
                }

                if (staticDiscount != null)
                {
                    if (Total != 0)
                    {
                        if (staticDiscount.SD_Percent != 0)
                        {
                            DisVal = (int)Math.Round(Total * staticDiscount.SD_Percent / 100, 0);
                            Total -= DisVal;
                        }
                    }
                }
            }
            prepareFeeViewModel.DiscountValue = DisVal;
            prepareFeeViewModel.TotalValue = Total;
            prepareFeeViewModel.IsValid = true;
            return View(prepareFeeViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayFee(PrepareFeeViewModel prepareFeeViewModel)
        {
            Course course = await _tService.GetCourseAsync(prepareFeeViewModel.CourseId).ConfigureAwait(false);

            if (course == null)
            {
                return NotFound("دوره یافت نشد !");
            }

            string MerchantID = "6f065498-9733-11ea-a5d8-000c295eb8fc";
            //http://localhost:56702/
            CourseUser courseUser = await _tService.GetCourseUserBy_URId_CourseId(prepareFeeViewModel.urId, prepareFeeViewModel.CourseId);
            if (courseUser == null)
            {
                return NotFound("امکان پرداخت وجود ندارد");
            }
            if (prepareFeeViewModel.TotalValue == 0)
            {
                return RedirectToAction(nameof(PayResult), new { cuId = courseUser.CU_Id, fee = prepareFeeViewModel.TotalValue, disValue = prepareFeeViewModel.DiscountValue, disCode = prepareFeeViewModel.DisCode });
            }
            //string CallbackURL = "http://localhost:56702/Account/PayResult?cuId=" + courseUser.CU_Id + "&fee=" + prepareFeeViewModel.TotalValue + "&disValue=" + prepareFeeViewModel.DiscountValue + "&disCode=" + prepareFeeViewModel.DisCode;
            string CallbackURL = "https://inslearn.ir/Account/PayResult?cuId=" + courseUser.CU_Id + "&fee=" + prepareFeeViewModel.TotalValue + "&disValue=" + prepareFeeViewModel.DiscountValue + "&disCode=" + prepareFeeViewModel.DisCode;
            UserRole userRole = await _userService.GetUserRoleByIdAsync(courseUser.URId);
            string Description = "پرداخت مبلغ دوره " + " " + course.Course_Title + "|" + userRole.User.UserFirstName + " " + userRole.User.UserFamily;
            

            var payment = new Zarinpal.Payment(MerchantID, prepareFeeViewModel.TotalValue);

            Zarinpal.Models.PaymentRequestResponse result = await payment.PaymentRequest(Description, CallbackURL);

            if (result.Status == 100)
            {
                return Redirect("https://zarinpal.com/pg/StartPay/" + result.Authority);
            }
            else
            {
                PayResultViewModel payResultViewModel = new PayResultViewModel()
                {
                    IsSuccess = false
                };

                return View(nameof(PayResult), payResultViewModel);
            }


        }

        public async Task<IActionResult> PayResult(int cuId, int fee, int disValue, string disCode)
        {
            CourseUser courseUser = await _tService.GetCourseUserByIdAsync(cuId);
            StaticDiscount staticDiscount = null;
            if (!string.IsNullOrEmpty(disCode))
            {
                staticDiscount = await _tService.GetStaticDiscountByCodeAsync(disCode);
            }
            if (courseUser == null)
            {
                return NotFound("امکان پرداخت وجود ندارد !");
            }
            string Status = HttpContext.Request.Query["Status"];
            string Authority = HttpContext.Request.Query["Authority"];
            PayResultViewModel payResultViewModel = new PayResultViewModel()
            {
                cuId = cuId,
                RegCourseUser = courseUser,
                RegUserRole = await _userService.GetUserRoleByIdAsync(courseUser.URId).ConfigureAwait(false),
                Course = await _tService.GetCourseAsync(courseUser.Course_Id)
            };

            if (fee == 0)
            {
                courseUser.IsActive = true;
                courseUser.PayDate = DateTime.Now;
                courseUser.PayValue = fee;
                courseUser.StaticDiscountCode = disCode;
                courseUser.DisValue = disValue;
                _tService.UpdateCourseUser(courseUser);
                if (staticDiscount != null)
                {
                    staticDiscount.SD_Used += 1;
                    _tService.EditStaticDiscount(staticDiscount);
                }

                await _tService.SaveAsync();
                payResultViewModel.TotalPayValue = fee;
                payResultViewModel.IsSuccess = true;
                payResultViewModel.RegUserRole = await _userService.GetUserRoleByIdAsync(courseUser.URId).ConfigureAwait(false);
                payResultViewModel.Teacher = await _tService.GetCourseTeacherAsync(payResultViewModel.Course.Course_Id).ConfigureAwait(false);
                int? roomid = payResultViewModel.Teacher.room_id;
                int? userid = null;
                if (payResultViewModel.RegUserRole.User.Sky_userId == null)
                {
                    List<SkyUserViewModel> skyUserViewModels = _skyService.GetSkyUsers();
                    if (!skyUserViewModels.Any(a => a.username == payResultViewModel.RegUserRole.User.UserCellPhone))
                    {
                        userid = await _skyService.CreateUserAsync(payResultViewModel.RegUserRole.URId, null).ConfigureAwait(false);
                    }
                    else
                    {
                        SkyUserViewModel skyUser = skyUserViewModels.FirstOrDefault(f => f.username == payResultViewModel.RegUserRole.User.UserCellPhone);
                        if (skyUser != null)
                        {
                            userid = skyUser.id;
                        }
                    }
                    User rgUser = await _userService.GetUserByIdAsync(payResultViewModel.RegUserRole.UserId);
                    rgUser.Sky_userId = userid;
                    _userService.UpdateUser(rgUser);
                    await _userService.SaveChangesAsync();

                }
                else
                {
                    userid = payResultViewModel.RegUserRole.User.Sky_userId;
                }
                List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel> RoomUsers = _skyService.GetRoomUsers((int)roomid);
                bool allowAdd = true;
                if (RoomUsers != null)
                {
                    if (RoomUsers.Count != 0)
                    {
                        if (RoomUsers.Any(a => a.user_id == userid))
                        {
                            allowAdd = false;
                        }
                    }
                }
                if (allowAdd == true)
                {
                    _skyService.AddStudentToRoom(roomid, userid);
                }

                return View(payResultViewModel);
            }
            else
            {
                if (Status.ToUpper() == "OK" && !string.IsNullOrEmpty(Authority))
                {
                    courseUser.IsActive = true;
                    courseUser.PayDate = DateTime.Now;
                    courseUser.PayValue = fee;
                    courseUser.StaticDiscountCode = disCode;
                    courseUser.DisValue = disValue;
                    _tService.UpdateCourseUser(courseUser);
                    if (staticDiscount != null)
                    {
                        staticDiscount.SD_Used += 1;
                        _tService.EditStaticDiscount(staticDiscount);
                    }

                    await _tService.SaveAsync();
                    payResultViewModel.IsSuccess = true;
                    payResultViewModel.RegUserRole = await _userService.GetUserRoleByIdAsync(courseUser.URId).ConfigureAwait(false);
                    payResultViewModel.Teacher = await _tService.GetCourseTeacherAsync(payResultViewModel.Course.Course_Id).ConfigureAwait(false);

                    int? roomid = payResultViewModel.Teacher.room_id;


                    int? userid = null;
                    if (payResultViewModel.RegUserRole.User.Sky_userId == null)
                    {
                        List<SkyUserViewModel> skyUserViewModels = _skyService.GetSkyUsers();
                        if (!skyUserViewModels.Any(a => a.username == payResultViewModel.RegUserRole.User.UserCellPhone))
                        {
                            userid = await _skyService.CreateUserAsync(payResultViewModel.RegUserRole.URId, null).ConfigureAwait(false);
                        }
                        else
                        {
                            SkyUserViewModel skyUser = skyUserViewModels.FirstOrDefault(f => f.username == payResultViewModel.RegUserRole.User.UserCellPhone);
                            if (skyUser != null)
                            {
                                userid = skyUser.id;
                            }
                        }
                        User rgUser = await _userService.GetUserByIdAsync(payResultViewModel.RegUserRole.UserId);
                        rgUser.Sky_userId = userid;
                        _userService.UpdateUser(rgUser);
                        await _userService.SaveChangesAsync();

                    }
                    else
                    {
                        userid = payResultViewModel.RegUserRole.User.Sky_userId;
                    }

                    List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel> roomUsers = _skyService.GetRoomUsers(payResultViewModel.Teacher.room_id);
                    bool allowAdd = true;
                    if (roomUsers != null)
                    {
                        if (roomUsers.Count != 0)
                        {
                            if (roomUsers.Any(a => a.user_id == userid))
                            {
                                allowAdd = false;
                            }
                        }
                    }
                    if (allowAdd == true)
                    {
                        bool res = _skyService.AddStudentToRoom(payResultViewModel.Teacher.room_id, payResultViewModel.RegUserRole.User.Sky_userId);
                    }
                    return View(payResultViewModel);
                }
            }

            payResultViewModel.IsSuccess = false;
            return View(payResultViewModel);
        }
        //register
        [Route("Register/{key}")]
        public IActionResult Register(string key, string cId)
        {
            ViewData["key"] = key;
            if (cId != null)
            {
                ViewData["cid"] = cId;
            }
            return View();
        }

        public IActionResult InitialRegister()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InitialRegister(InitialRegisterViewModel initialRegisterViewModel)
        {

            if (!ModelState.IsValid)
            {
                return PartialView(initialRegisterViewModel);
            }
            if (PLUtility.IsValidNC(initialRegisterViewModel.UserNC) == false)
            {
                ModelState.AddModelError("UserNC", "کد ملی نامعتبر است !");
                return PartialView(initialRegisterViewModel);
            }
            initialRegisterViewModel.UserNC = PLUtility.ChangeToEnglishNumber(initialRegisterViewModel.UserNC);
            initialRegisterViewModel.UserCellphone = PLUtility.ChangeToEnglishNumber(initialRegisterViewModel.UserCellphone);
            List<Role> roles = await _userService.Get_User_Roles_ByNC_Async(initialRegisterViewModel.UserNC);
            PersianCalendar pc = new PersianCalendar();
            if (initialRegisterViewModel.Key.ToLower() == "s")
            {
                if (roles != null)
                {

                    if (roles.Any(a => a.RoleId == 4))
                    {
                        initialRegisterViewModel.RegisteredAsStudent = true;
                        if (string.IsNullOrEmpty(initialRegisterViewModel.CId))
                        {
                            ModelState.AddModelError("UserNC", "این کد ملی قبلا به عنوان فراگیر ثبت شده است !");
                            return PartialView(initialRegisterViewModel);
                        }
                        else
                        {
                            User user = await _userService.GetUserByNC(initialRegisterViewModel.UserNC);

                            if (user != null)
                            {
                                UserRole userRole = await _userService.GetUserRoleBy_UserName_RoleId(user.UserName, 4);
                                Course course = await _tService.GetCourseAsync(int.Parse(initialRegisterViewModel.CId));

                                List<UserRole> students = await _tService.GetCourseUsersByRoleAsync(course.CourseUsers.ToList(), 4, course.Course_Id);
                                int stCount = 0;
                                if (students != null)
                                {
                                    if (students.Count != 0)
                                    {
                                        stCount = students.Count;
                                    }
                                }
                                if (userRole == null)
                                {
                                    ModelState.AddModelError("UserNC", "شما کاربری از نوع فراگیر ندارید !");
                                    return PartialView(initialRegisterViewModel);

                                }
                                if (initialRegisterViewModel.CId == null)
                                {
                                    ModelState.AddModelError("UserNC", "دوره ای یافت نشد !");
                                    return PartialView(initialRegisterViewModel);

                                }
                                if (course == null)
                                {
                                    ModelState.AddModelError("UserNC", "دوره ای یافت نشد !");
                                    return PartialView(initialRegisterViewModel);
                                }
                                if (DateTime.Now > course.Course_EndDateRegistration)
                                {
                                    ModelState.AddModelError("UserNC", "مهلت ثبت نام به پایان رسیده است !");
                                    return PartialView(initialRegisterViewModel);
                                }
                                if (course.Course_Capacity - stCount <= 0)
                                {
                                    ModelState.AddModelError("UserNC", "ظرفیت دوره تکمیل شده است !");
                                    return PartialView(initialRegisterViewModel);
                                }
                                if (course.CourseUsers.Any(a => a.URId == userRole.URId && a.IsActive == true))
                                {
                                    ModelState.AddModelError("UserNC", "شما قبلا در این دوره ثبت نام کرده اید !");
                                    initialRegisterViewModel.IsPaied = true;
                                    initialRegisterViewModel.Course = course;
                                    return PartialView(initialRegisterViewModel);

                                }
                                if (course.CourseUsers.Any(a => a.URId == userRole.URId && a.IsActive == false))
                                {

                                    initialRegisterViewModel.IsPaied = false;
                                    initialRegisterViewModel.UserRole = userRole;
                                    return PartialView(initialRegisterViewModel);

                                }
                                CourseUser courseUser = new CourseUser()
                                {
                                    URId = userRole.URId,
                                    Course_Id = course.Course_Id,
                                    CU_CreateDate = DateTime.Now,


                                };
                                int total = course.Course_Fee;
                                if (!string.IsNullOrEmpty(course.SteppedDiscountCode))
                                {
                                    SteppedDiscount steppedDiscount = await _tService.GetSteppedDiscountByCodeAsync(course.SteppedDiscountCode).ConfigureAwait(false);
                                    if (steppedDiscount != null)
                                    {
                                        if (steppedDiscount.IsActive)
                                        {
                                            string type = steppedDiscount.SteppedDiscountType.Name;
                                            int nubat = course.CourseUsers.Where(w => w.UserRole.RoleId == 4).Count();
                                            nubat += 1;
                                            if (steppedDiscount.SteppedDiscountType.Name == "person")
                                            {
                                                SteppedDiscountDetail steppedDiscountDetail = steppedDiscount.SteppedDiscountDetails.FirstOrDefault(f => nubat <= f.ToPerson);
                                                if (steppedDiscountDetail != null)
                                                {
                                                    total = course.Course_Fee - (int)Math.Round(course.Course_Fee * (steppedDiscountDetail.Percent / 100), 0);
                                                }
                                            }
                                            if (steppedDiscount.SteppedDiscountType.Name == "time")
                                            {
                                                SteppedDiscountDetail steppedDiscountDetail = steppedDiscount.SteppedDiscountDetails.FirstOrDefault(f => DateTime.Now <= f.ToDate);
                                                if (steppedDiscountDetail != null)
                                                {
                                                    total = course.Course_Fee - (int)Math.Round((course.Course_Fee * (steppedDiscountDetail.Percent / 100)), 0);
                                                }
                                            }

                                        }
                                    }
                                }

                                if (total == 0)
                                {
                                    courseUser.IsActive = true;
                                }
                                _tService.CreateCourseUser(courseUser);
                                await _tService.SaveAsync();
                                UserRole teaher = await _tService.GetCourseTeacherAsync(course.Course_Id).ConfigureAwait(false);
                                int? userid = null;
                                if (userRole.User.Sky_userId == null)
                                {
                                    List<SkyUserViewModel> skyUserViewModels = _skyService.GetSkyUsers();
                                    if (!skyUserViewModels.Any(a => a.username == userRole.User.UserCellPhone))
                                    {
                                        userid = await _skyService.CreateUserAsync(userRole.URId, null).ConfigureAwait(false);
                                    }
                                    else
                                    {
                                        SkyUserViewModel skyUser = skyUserViewModels.FirstOrDefault(f => f.username == userRole.User.UserCellPhone);
                                        if (skyUser != null)
                                        {
                                            userid = skyUser.id;
                                        }
                                    }
                                    user.Sky_userId = userid;
                                    _userService.UpdateUser(user);
                                    await _userService.SaveChangesAsync();

                                }
                                else
                                {
                                    userid = userRole.User.Sky_userId;
                                }
                                List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel> roomUsers = _skyService.GetRoomUsers(teaher.room_id);
                                bool allowAdd = true;
                                if (roomUsers != null)
                                {
                                    if (roomUsers.Count != 0)
                                    {
                                        if (roomUsers.Any(a => a.user_id == userid))
                                        {
                                            allowAdd = false;
                                        }
                                    }
                                }
                                if (allowAdd == true)
                                {
                                    if (courseUser.IsActive == true)
                                    {
                                        bool res = _skyService.AddStudentToRoom(teaher.room_id, userRole.User.Sky_userId);
                                    }
                                    
                                }

                                return RedirectToAction(nameof(RegisterResult), new { urId = userRole.URId, totalFee = total, cId = course.Course_Id, onlycourse = true });



                            }
                        }

                    }
                    if (roles.Any(a => a.RoleId == 3))
                    {
                        User user = await _userService.GetUserByNC(initialRegisterViewModel.UserNC);
                        if (initialRegisterViewModel.UserCellphone != user.UserCellPhone)
                        {
                            ModelState.AddModelError("UserCellphone", "این شماره تلفن متعلق به این کد ملی نیست !");
                            return PartialView(initialRegisterViewModel);
                        }

                        initialRegisterViewModel.RegisteredAsTeacher = true;
                        StudentRegisterViewModel studentRegisterViewModel = new StudentRegisterViewModel()
                        {
                            UserFirstName = user.UserFirstName,
                            UserFamily = user.UserFamily,
                            UserNC = user.UserNC,
                            UserCellphone = user.UserCellPhone,
                            UserNcImage = user.UserNCFile,
                            UserSex = user.UserSex,
                            UserEduLevel = user.LevelOfEducation,
                            StateId = user.County.StateId,
                            CountyId = (int)user.CountyId,
                            Counties = await _subEntityService.GetCountiesofState(user.County.StateId),
                            IsConfirmed = true,
                            States = await _subEntityService.GetStates(),
                            UserLevels = await _userService.UserLevels(),
                            RoleId = 4,
                            CId = initialRegisterViewModel.CId
                        };
                        if (!string.IsNullOrEmpty(user.UserBirthDate))
                        {
                            string[] bdt = user.UserBirthDate.Split("/");
                            studentRegisterViewModel.UserBirthDateYear = int.Parse(bdt[0]);
                            studentRegisterViewModel.UserBirthDateMouth = int.Parse(bdt[1]);
                            studentRegisterViewModel.UserBirthDateDay = int.Parse(bdt[2]);
                        }
                        return PartialView(nameof(RegisterStudent), studentRegisterViewModel);
                    }
                }

                if (await _userService.ExistUserCellphone(initialRegisterViewModel.UserCellphone))
                {
                    ModelState.AddModelError("UserCellphone", "این شماره تلفن متعلق به این کد ملی نیست !");
                    return PartialView(initialRegisterViewModel);
                }
                ConfirmCellphoneViewModel confirmCellphoneViewModel = new ConfirmCellphoneViewModel()
                {
                    Key = initialRegisterViewModel.Key,
                    CId = initialRegisterViewModel.CId,
                    UserNC = initialRegisterViewModel.UserNC,
                    UserCellphone = initialRegisterViewModel.UserCellphone
                };
                return RedirectToAction("ConfirmCellphone", confirmCellphoneViewModel);

            }
            if (initialRegisterViewModel.Key.ToLower() == "t")
            {
                if (roles != null)
                {

                    if (roles.Any(a => a.RoleId == 3))
                    {

                        initialRegisterViewModel.RegisteredAsTeacher = true;
                        ModelState.AddModelError("UserNC", "این کد ملی قبلا به عنوان مدرس ثبت شده است !");
                        return PartialView(initialRegisterViewModel);
                    }
                    if (roles.Any(a => a.RoleId == 4))
                    {
                        User user = await _userService.GetUserByNC(initialRegisterViewModel.UserNC).ConfigureAwait(false);
                        if (initialRegisterViewModel.UserCellphone != user.UserCellPhone)
                        {
                            ModelState.AddModelError("UserCellphone", "این شماره تلفن برای فرد دیگری ثبت شده است !");
                            return PartialView(initialRegisterViewModel);
                        }
                        initialRegisterViewModel.RegisteredAsStudent = true;

                        TeacherRegisterViewModel teacherRegisterViewModel = new TeacherRegisterViewModel()
                        {
                            Key = initialRegisterViewModel.Key,
                            UserNC = initialRegisterViewModel.UserNC,
                            UserCellphone = initialRegisterViewModel.UserCellphone,
                            States = await _subEntityService.GetStates(),
                            UserFirstName = user.UserFirstName,
                            UserFamily = user.UserFamily,
                            UserNcImage = user.UserNCFile,
                            UserSex = user.UserSex,
                            UserEduLevel = user.LevelOfEducation,
                            StateId = user.County.StateId,
                            CountyId = (int)user.CountyId,
                            Counties = await _subEntityService.GetCountiesofState(user.County.StateId),
                            IsConfirmed = true,
                            RoleId = 3

                        };
                        if (!string.IsNullOrEmpty(user.UserBirthDate))
                        {
                            string[] bdt = user.UserBirthDate.Split("/");
                            teacherRegisterViewModel.UserBirthDateYear = int.Parse(bdt[0]);
                            teacherRegisterViewModel.UserBirthDateMouth = int.Parse(bdt[1]);
                            teacherRegisterViewModel.UserBirthDateDay = int.Parse(bdt[2]);
                        }

                        return PartialView("RegisterTeacher", teacherRegisterViewModel);

                    }


                }

                if (await _userService.ExistUserCellphone(initialRegisterViewModel.UserCellphone))
                {
                    ModelState.AddModelError("UserCellphone", "این شماره تلفن برای فرد دیگری ثبت شده است !");
                    return PartialView(initialRegisterViewModel);
                }
                ConfirmCellphoneViewModel confirmCellphoneViewModel = new ConfirmCellphoneViewModel()
                {
                    Key = initialRegisterViewModel.Key,
                    UserNC = initialRegisterViewModel.UserNC,
                    UserCellphone = initialRegisterViewModel.UserCellphone
                };
                return RedirectToAction("ConfirmCellphone", confirmCellphoneViewModel);
            }

            return PartialView();
        }
        public IActionResult ConfirmCellphone(ConfirmCellphoneViewModel confirmCellphoneViewModel)
        {
            SendMessageViewModel sendMessageViewModel = new SendMessageViewModel();
            string code = GeneratorClass.GeneratePassword(4, "digit");

            CookieOptions option = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(60)
            };
            string cookiecode = PasswordHelper.EncodePasswordMd5(code);
            Response.Cookies.Append("pconfirmcode", cookiecode, option);

            bool res = _userService.SendVerificationCode(code, confirmCellphoneViewModel.UserCellphone);
            return PartialView(confirmCellphoneViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmCellphone(string key, ConfirmCellphoneViewModel confirmCellphoneViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(confirmCellphoneViewModel);
            }
            if (string.IsNullOrEmpty(confirmCellphoneViewModel.ConfirmCode))
            {
                ModelState.AddModelError("ConfirmCode", "لطفا کد اعتبار سنجی را وارد کنید");
                return PartialView(confirmCellphoneViewModel);
            }
            string cookie = Request.Cookies["pconfirmcode"];
            if (!string.IsNullOrEmpty(cookie))
            {
                string concode = PasswordHelper.EncodePasswordMd5(confirmCellphoneViewModel.ConfirmCode);
                if (concode != null)
                {
                    if (key.ToLower() == "s")
                    {
                        return RedirectToAction("RegisterStudent", new { key = confirmCellphoneViewModel.Key, nc = confirmCellphoneViewModel.UserNC, cellphone = confirmCellphoneViewModel.UserCellphone, cid = confirmCellphoneViewModel.CId });
                    }
                    if (key.ToLower() == "t")
                    {
                        return RedirectToAction("RegisterTeacher", new { key = confirmCellphoneViewModel.Key, nc = confirmCellphoneViewModel.UserNC, cellphone = confirmCellphoneViewModel.UserCellphone });
                    }
                    return RedirectToAction("RegisterStudent", new { key = confirmCellphoneViewModel.Key, nc = confirmCellphoneViewModel.UserNC, cellphone = confirmCellphoneViewModel.UserCellphone, cid = confirmCellphoneViewModel.CId });

                }
                else
                {
                    ModelState.AddModelError("ConfirmCode", "کد وارد شده نامعتبر است !");
                    return PartialView(confirmCellphoneViewModel);
                }

            }
            else
            {
                ModelState.AddModelError("ConfirmCode", "کد وارد شده نامعتبر است ثبت نام را دوباره شروع کنید !");
                return PartialView(confirmCellphoneViewModel);
            }

        }
        public async Task<IActionResult> RegisterStudent(string key, string nc, string cellphone, string cid)
        {
            StudentRegisterViewModel studentRegisterViewModel = new StudentRegisterViewModel()
            {
                States = await _subEntityService.GetStates(),
                UserLevels = await _userService.UserLevels(),
                UserNC = nc,
                UserCellphone = cellphone,
                Key = key,
                CId = cid

            };
            return PartialView(studentRegisterViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterStudent(StudentRegisterViewModel studentRegisterViewModel)
        {
            try
            {
                studentRegisterViewModel.States = await _subEntityService.GetStates();
                studentRegisterViewModel.Counties = await _subEntityService.GetCountiesofState((int)studentRegisterViewModel.StateId);
                studentRegisterViewModel.UserLevels = await _userService.UserLevels();
                if (!ModelState.IsValid)
                {
                    return PartialView(studentRegisterViewModel);
                }

                if (studentRegisterViewModel.IsConfirmed == false)
                {

                    User user = new User()
                    {
                        UserFirstName = studentRegisterViewModel.UserFirstName,
                        UserFamily = studentRegisterViewModel.UserFamily,
                        UserNC = studentRegisterViewModel.UserNC,
                        UserCellPhone = studentRegisterViewModel.UserCellphone,
                        UserCellPhoneConfirmed = true,
                        LevelOfEducation = studentRegisterViewModel.UserEduLevel,
                        UserLevel_Id = studentRegisterViewModel.UserLevelId,
                        UserName = studentRegisterViewModel.UserCellphone,
                        UserPassword = PasswordHelper.EncodePasswordMd5(studentRegisterViewModel.UserNC),
                        LastPassword = studentRegisterViewModel.UserNC,
                        UserIsActive = true,
                        UserRegisteredDate = DateTime.Now,
                        UserSex = studentRegisterViewModel.UserSex,
                        CountyId = studentRegisterViewModel.CountyId

                    };

                    int y = studentRegisterViewModel.UserBirthDateYear;
                    int m = studentRegisterViewModel.UserBirthDateMouth;
                    int d = studentRegisterViewModel.UserBirthDateDay;
                    string strbdate = y.ToString("0000") + "/" + m.ToString("00") + "/" + d.ToString("00");
                    user.UserBirthDate = strbdate;

                    // await _userService.SaveChangesAsync();
                    Role role = await _userService.GetRoleByIdAsync(4);
                    if (role != null)
                    {
                        UserRole userRole = new UserRole()
                        {
                            User = user,
                            RoleId = 4,
                            RegisterDate = DateTime.Now,
                            IsActive = true
                        };
                        await _userService.CreateUserRoleAsync(userRole);
                        _userService.SaveChange();
                        Course course = null;
                        if (!string.IsNullOrEmpty(studentRegisterViewModel.CId))
                        {
                            course = await _tService.GetCourseAsync(int.Parse(studentRegisterViewModel.CId)).ConfigureAwait(false);
                            if (course != null)
                            {
                                if (course.Course_EndDateRegistration >= DateTime.Now)
                                {
                                    List<UserRole> students = new List<UserRole>();
                                    students = await _tService.GetCourseUsersByRoleAsync(course.CourseUsers.ToList(), 4 , course.Course_Id);
                                    if (course.Course_Capacity - students.Count > 0)
                                    {
                                        CourseUser courseUser = new CourseUser()
                                        {
                                            Course = course,
                                            UserRole = userRole,
                                            CU_CreateDate = DateTime.Now
                                        };
                                        if (course.Course_Fee == 0)
                                        {
                                            courseUser.IsActive = true;
                                            _tService.CreateCourseUser(courseUser);
                                            await _tService.SaveAsync();
                                        }
                                        else
                                        {
                                            courseUser.IsActive = false;
                                            _tService.CreateCourseUser(courseUser);
                                            await _tService.SaveAsync();
                                            string url = "/Account/PrepareFee?cId=" + course.Course_Id + "&urId=" + userRole.URId;
                                            string content = "<h3 class='text-xs-center alert alert-info'>برای نهایی شدن ثبت نام بر روی دکمه پرداخت کلیک کنید</h3>";
                                            content += "<br /><a class='btn btn-success btn-lg col-xs-12' href =" + url + ">" + "پرداخت</a>";
                                            return Content(content);
                                        }

                                    }
                                    else
                                    {
                                        ModelState.AddModelError("UserFirstName", "ظرفیت دوره تکمیل شده است !");
                                        return PartialView(studentRegisterViewModel);
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("UserFirstName", "مهلت ثبت نام به پایان رسیده است ! !");
                                    return PartialView(studentRegisterViewModel);
                                }
                            }

                        }
                        await _userService.SaveChangesAsync();
                        RegisterResultViewModel registerResultViewModel = new RegisterResultViewModel
                        {
                            User = user,
                            Role = role,
                            UserRole = userRole,
                            Course = course
                        };
                        int? cid = null;
                        if (course != null)
                        {
                            cid = course.Course_Id;
                        }
                        return RedirectToAction(nameof(RegisterResult), new { urId = userRole.URId, cId = cid });
                    }
                }
                else
                {
                    User user = await _userService.GetUserByNC(studentRegisterViewModel.UserNC);
                    if (user != null)
                    {
                        user.UserLevel_Id = studentRegisterViewModel.UserLevelId;
                    };
                    _userService.UpdateUser(user);
                    Role role = await _userService.GetRoleByIdAsync(4);
                    if (role != null)
                    {
                        UserRole userRole = new UserRole()
                        {
                            User = user,
                            RoleId = 4,
                            RegisterDate = DateTime.Now,
                            IsActive = true
                        };
                        await _userService.CreateUserRoleAsync(userRole);

                        Course course = null;
                        if (!string.IsNullOrEmpty(studentRegisterViewModel.CId))
                        {
                            course = await _tService.GetCourseAsync(int.Parse(studentRegisterViewModel.CId)).ConfigureAwait(false);

                            if (course != null)
                            {
                                if (course.Course_EndDateRegistration >= DateTime.Now)
                                {
                                    List<UserRole> students = new List<UserRole>();
                                    students = await _tService.GetCourseUsersByRoleAsync(course.CourseUsers.ToList(), 4, course.Course_Id);

                                    if (course.Course_Capacity - students.Count > 0)
                                    {
                                        CourseUser courseUser = new CourseUser()
                                        {
                                            Course = course,
                                            UserRole = userRole,
                                            CU_CreateDate = DateTime.Now
                                        };
                                        _tService.CreateCourseUser(courseUser);
                                    }
                                    else
                                    {
                                        return Content("ظرفیت دوره تکمیل شده است !");
                                    }
                                }
                                else
                                {
                                    return Content("مهلت ثبت نام به پایان رسیده است !");
                                }
                            }
                        }

                        await _userService.SaveChangesAsync();
                        RegisterResultViewModel registerResultViewModel = new RegisterResultViewModel
                        {
                            User = user,
                            Role = role,
                            UserRole = userRole,
                            Course = course
                        };
                        int? cid = null;
                        if (course != null)
                        {
                            cid = course.Course_Id;
                        }
                        if (course.CourseStatus.CourseStatus_Title != "دانلودی")
                        {
                            return RedirectToAction(nameof(RegisterResult), new { urId = userRole.URId, cId = cid });
                        }
                        else
                        {
                            return RedirectToAction("CourseDetails", "Home", new { id = cid });
                        }

                    }

                }

            }
            catch (Exception ex)
            {

                string m = ex.Message;
            }
            return PartialView(studentRegisterViewModel);
        }
        public async Task<IActionResult> RegisterTeacher(string key, string nc, string cellphone, bool isReged)
        {
            if (isReged == false)
            {
                TeacherRegisterViewModel teacherRegisterViewModel = new TeacherRegisterViewModel()
                {
                    IsConfirmed = isReged,
                    Key = key,
                    UserNC = nc,
                    UserCellphone = cellphone,
                    States = await _subEntityService.GetStates().ConfigureAwait(false)
                };
                return PartialView(teacherRegisterViewModel);
            }
            else
            {
                User user = await _userService.GetUserByNC(nc).ConfigureAwait(false);
                TeacherRegisterViewModel teacherRegisterViewModel = new TeacherRegisterViewModel()
                {
                    IsConfirmed = isReged,
                    UserFirstName = user.UserFirstName,
                    UserFamily = user.UserFamily,
                    UserCellphone = user.UserCellPhone,

                    States = await _subEntityService.GetStates().ConfigureAwait(false)
                };
                return PartialView(teacherRegisterViewModel);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterTeacher(TeacherRegisterViewModel teacherRegisterViewModel)
        {
            try
            {
                teacherRegisterViewModel.States = await _subEntityService.GetStates();
                teacherRegisterViewModel.Counties = await _subEntityService.GetCountiesofState((int)teacherRegisterViewModel.StateId);

                if (!ModelState.IsValid)
                {
                    return PartialView(teacherRegisterViewModel);
                }
                if (teacherRegisterViewModel.IsConfirmed == true)
                {
                    User user = await _userService.GetUserByNC(teacherRegisterViewModel.UserNC).ConfigureAwait(false);
                    user.LevelOfEducation = teacherRegisterViewModel.UserEduLevel;
                    user.UserBiography = teacherRegisterViewModel.UserSkills;
                    user.UserYearofGraduataion = int.Parse(teacherRegisterViewModel.UserEduGraYear);
                    user.UserUniversity = teacherRegisterViewModel.UserUniversity;
                    user.EducationFile = teacherRegisterViewModel.UserEduImage;
                    user.UserIsActive = true;
                    _userService.UpdateUser(user);
                    UserRole userRole = new UserRole()
                    {
                        User = user,
                        RoleId = 3,
                        RegisterDate = DateTime.Now,
                        IsActive = false
                    };
                    await _userService.CreateUserRoleAsync(userRole).ConfigureAwait(false);
                    await _userService.SaveChangesAsync().ConfigureAwait(false);
                    int? roomid = await _skyService.CreateRoomAsync(userRole.URId).ConfigureAwait(false);
                    int? userid = await _skyService.CreateUserAsync(userRole.URId, null).ConfigureAwait(false);
                    string roomUrl = _skyService.GetRoomLink(roomid);
                    if (!string.IsNullOrEmpty(roomUrl))
                    {
                        userRole.RoomLink = roomUrl;
                        _userService.UpdateUserRole(userRole);
                        await _userService.SaveChangesAsync().ConfigureAwait(false);
                    }
                    _skyService.AddTeacherToRoom(roomid, userid);
                    return RedirectToAction(nameof(RegisterResult), new { urId = userRole.URId });
                }
                else
                {

                    User user = new User()
                    {
                        UserFirstName = teacherRegisterViewModel.UserFirstName,
                        UserFamily = teacherRegisterViewModel.UserFamily,
                        UserNC = teacherRegisterViewModel.UserNC,
                        UserCellPhone = teacherRegisterViewModel.UserCellphone,
                        UserName = teacherRegisterViewModel.UserCellphone,
                        UserPassword = PasswordHelper.EncodePasswordMd5(teacherRegisterViewModel.UserNC),
                        LastPassword = teacherRegisterViewModel.UserNC,
                        LevelOfEducation = teacherRegisterViewModel.UserEduLevel,
                        UserSex = teacherRegisterViewModel.UserSex,
                        UserYearofGraduataion = int.Parse(teacherRegisterViewModel.UserEduGraYear),
                        UserUniversity = teacherRegisterViewModel.UserUniversity,
                        UserBiography = teacherRegisterViewModel.UserSkills,
                        CountyId = teacherRegisterViewModel.CountyId,
                        UserCellPhoneConfirmed = true,
                        UserIsActive = true,
                        UserRegisteredDate = DateTime.Now
                    };
                    int y = teacherRegisterViewModel.UserBirthDateYear;
                    int m = teacherRegisterViewModel.UserBirthDateMouth;
                    int d = teacherRegisterViewModel.UserBirthDateDay;
                    string strbdate = y.ToString("0000") + "/" + m.ToString("00") + "/" + d.ToString("00");
                    user.UserBirthDate = strbdate;
                    _userService.CreateUser(user);
                    await _userService.SaveChangesAsync().ConfigureAwait(false);
                    UserRole userRole = new UserRole()
                    {
                        User = user,
                        RoleId = 3,
                        RegisterDate = DateTime.Now,
                        IsActive = true,
                    };
                    _userService.CreateUserRole(userRole);
                    await _userService.SaveChangesAsync().ConfigureAwait(false);
                    int? roomid = await _skyService.CreateRoomAsync(userRole.URId).ConfigureAwait(false);
                    int? userid = await _skyService.CreateUserAsync(userRole.URId, null).ConfigureAwait(false);
                    string roomUrl = _skyService.GetRoomLink(roomid);
                    if (!string.IsNullOrEmpty(roomUrl))
                    {
                        userRole.RoomLink = roomUrl;
                        _userService.UpdateUserRole(userRole);
                        await _userService.SaveChangesAsync().ConfigureAwait(false);
                    }
                    _skyService.AddTeacherToRoom(roomid, userid);
                    return RedirectToAction(nameof(RegisterResult), new { urId = userRole.URId });
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }

            return PartialView(teacherRegisterViewModel);
        }
        public async Task<IActionResult> RegisterResult(int urId, int totalFee, int? cId, bool onlycourse = false)
        {
            Course course = null;
            UserRole Teacher = null;
            if (cId != null)
            {
                course = await _tService.GetCourseAsync((int)cId).ConfigureAwait(false);
                Teacher = await _tService.GetCourseTeacherAsync((int)cId).ConfigureAwait(false);
            }
            UserRole userRole = await _userService.GetUserRoleByIdAsync(urId).ConfigureAwait(false);
            
            int? roomid = Teacher.room_id;
            int? userid = null;
            if (userRole.User.Sky_userId == null)
            {
                List<SkyUserViewModel> skyUserViewModels = _skyService.GetSkyUsers();
                if (!skyUserViewModels.Any(a => a.username == userRole.User.UserCellPhone))
                {
                    userid = await _skyService.CreateUserAsync(userRole.URId, null).ConfigureAwait(false);
                }
                else
                {
                    SkyUserViewModel skyUser = skyUserViewModels.FirstOrDefault(f => f.username == userRole.User.UserCellPhone);
                    if (skyUser != null)
                    {
                        userid = skyUser.id;
                    }
                }
                User rgUser = await _userService.GetUserByIdAsync(userRole.UserId);
                rgUser.Sky_userId = userid;
                _userService.UpdateUser(rgUser);
                await _userService.SaveChangesAsync();
            }
            else
            {
                userid = userRole.User.Sky_userId;
            }
            List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel> RoomUsers = _skyService.GetRoomUsers((int)roomid);
            bool allowAdd = true;
            if (RoomUsers != null)
            {
                if (RoomUsers.Count != 0)
                {
                    if (RoomUsers.Any(a => a.user_id == userid))
                    {                        
                        allowAdd = false;
                    }
                }
            }
            if (allowAdd == true)
            {
                CourseUser courseUser = await _tService.GetCourseUserBy_URId_CourseId(urId, (int)cId);
                if (courseUser != null)
                {
                    if(courseUser.IsActive == true)
                    {
                        _skyService.AddStudentToRoom(roomid, userid);
                    }
                }
                
            }
            RegisterResultViewModel registerResultViewModel = new RegisterResultViewModel()
            {
                User = userRole.User,
                Role = userRole.Role,
                Course = course,
                UserRole = userRole,
                RegInCourseOnly = onlycourse,
                Teacher = Teacher,
                TotalFee = totalFee
            };

            return PartialView(registerResultViewModel);
        }
        //register
        #region forgotPassword
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            User user = await _userService.GetUserByUserName(forgotPassword.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "کاربری با اطلاعات وارد شده در سیستم وجود ندارد !");
                return View(forgotPassword);
            }
            if (forgotPassword.UserNC != user.UserNC)
            {
                ModelState.AddModelError("UserName", "کاربری با اطلاعات وارد شده در سیستم وجود ندارد !");
                return View(forgotPassword);
            }
            if (forgotPassword.UserCellphone != user.UserCellPhone)
            {
                ModelState.AddModelError("UserName", "کاربری با اطلاعات وارد شده در سیستم وجود ندارد !");
                return View(forgotPassword);
            }
            string cookie = Request.Cookies["sendpass"];
            string cookieEx = Request.Cookies["spt"];
            if (string.IsNullOrEmpty(cookie))
            {
                SendMessageViewModel sendMessageViewModel = new SendMessageViewModel();
                _userService.SendPassword(user.LastPassword, forgotPassword.UserCellphone);
                CookieOptions option = new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(30)
                };
                Response.Cookies.Append("sendpass", "1", option);
                PersianCalendar pc = new PersianCalendar();
                int min = pc.GetMinute(DateTime.Now);
                int h = pc.GetHour(DateTime.Now);
                Response.Cookies.Append("spt", h.ToString() + ":" + min.ToString(), option);

                forgotPassword.SendPass = true;
                forgotPassword.SendMess = "";
            }
            else
            {
                string exmin = string.Empty;
                if (!string.IsNullOrEmpty(cookieEx))
                {
                    exmin = cookieEx;
                }
                int nmin = DateTime.Now.Minute;
                int rem = 0;
                PersianCalendar pc = new PersianCalendar();
                int nh = pc.GetHour(DateTime.Now);
                int nm = pc.GetMinute(DateTime.Now);
                if (!string.IsNullOrEmpty(exmin))
                {
                    string[] t = exmin.Split(":");
                    int h = int.Parse(t[0].ToString());
                    int m = int.Parse(t[1].ToString());
                    int pminutes = (h * 60) + m;
                    int nminutes = (nh * 60) + nm;

                    rem = nminutes - pminutes;
                }
                string time = "";
                string mess = "";
                if (rem <= 1)
                {

                    mess = "رمز عبور در لحظاتی پیش به تلفن همراه شما ارسال شده است و تا 30 دقیقه آینده امکان ارسال مجدد وجود ندارد";
                }
                else
                {
                    time = rem.ToString();
                    mess = "رمز عبور در حدود  " + " " + time + " " + "دقیقه قبل به تلفن همراه شما ارسال شده است و تا " + " " + (30 - rem).ToString() + " " + "دقیقه امکان ارسال رمز وجود ندارد";
                }
                forgotPassword.SendMess = mess;
                return View(forgotPassword);
            }


            return View(forgotPassword);
        }
        #endregion forgotPassword
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }




    }
}