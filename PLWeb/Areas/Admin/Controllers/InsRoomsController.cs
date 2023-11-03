using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLCore.DTOs.SkyRoom;
using PLCore.Services.Interfaces;
using PLDataLayer.Entities.User;

using Newtonsoft.Json;
using PLCore.Utility;
using PLCore.Generators;


namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class InsRoomsController : Controller
    {
        public static readonly HttpClient HttpClient = new HttpClient();
        private readonly IUserService _userService;
        private readonly ITrainingService _tService;
        private readonly ISkyService _skyService;
        public InsRoomsController(IUserService userService, ITrainingService tService , ISkyService skyService)
        {
            _userService = userService;
            _tService = tService;
            _skyService = skyService;
        }
        public IActionResult GetRooms()
        {
            List<SkyRoomViewModel> skyRoomViewModels =  _skyService.GetRooms();
            return View(skyRoomViewModels);
        }
        public IActionResult GetUsers()
        {
            List<SkyUserViewModel> skyUserViewModels = _skyService.GetSkyUsers();
            return View(skyUserViewModels);
        }
        public async Task<IActionResult> CreateRoom()
        {
            int roleId = int.Parse(User.FindFirst("RoleId").Value.ToString());
            User user = await _userService.GetUserByUserName(User.Identity.Name).ConfigureAwait(false);
            UserRole userRole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId).ConfigureAwait(false);
            RoomViewModel roomViewModel = new RoomViewModel()
            {

                title = "اتاق آموزش آنلاین" + " " + userRole.User.UserFirstName + " " + userRole.User.UserFamily,
                description = "مرکز آموزشهای صنعت بیمه",
                status = 1,
                op_login_first = true,                
                max_users = 51,
                time_limit = 360000
            };

            List<UserRole> teachers = new List<UserRole>();
            if (roleId == 3)
            {
                UserRole teacher = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, roleId).ConfigureAwait(false);
                roomViewModel.TeacherurId = teacher.URId;
                teachers.Add(teacher);
            }
            if (roleId == 1)
            {
                var insteachers = await _userService.GetTeachersAsync().ConfigureAwait(false);
                teachers.AddRange(insteachers.Where(w => w.IsActive && w.User.UserIsActive == true));

            }
            roomViewModel.Teachers.AddRange(teachers);
            return View(roomViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateRoom(RoomViewModel roomViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(roomViewModel);
            }
            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "createRoom",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };
            int roleId = int.Parse(User.FindFirst("RoleId").Value.ToString());
            if (roleId == 3 || roleId == 1)
            {
                User user = await _userService.GetUserByUserName(User.Identity.Name).ConfigureAwait(false);

                UserRole userRole = await _userService.GetUserRoleByIdAsync((int)roomViewModel.TeacherurId).ConfigureAwait(false);
                var paramz = new Dictionary<string, object>();
                paramz.Add("name", "class_" + userRole.URId.ToString() + "_" + GeneratorClass.GeneratePassword(4, "digit"));
                string title = "اتاق آموزش آنلاین" + " " + userRole.User.UserFirstName + " " + userRole.User.UserFamily;
                if (!string.IsNullOrEmpty(roomViewModel.title))
                {
                    title = roomViewModel.title;

                }
                paramz.Add("title", title);

                if (!string.IsNullOrEmpty(roomViewModel.description))
                {
                    paramz.Add("description", roomViewModel.description);
                }
                else
                {
                    paramz.Add("description", "مرکز آموزشهای صنعت بیمه");
                }

                paramz.Add("status", roomViewModel.status);
                paramz.Add("op_login_first", roomViewModel.op_login_first);
                paramz.Add("guest_login", roomViewModel.guest_login);
                paramz.Add("guest_limit", roomViewModel.guest_limit);
                paramz.Add("time_limit", roomViewModel.time_limit.GetValueOrDefault(100));
                paramz.Add("max_users", roomViewModel.max_users.GetValueOrDefault(51));
                try
                {
                    response = HttpClient.PostAsJsonAsync<Dictionary<string, object>>(
                        data.APIUrl,
                        new Dictionary<String, object>
                        {
                        { "action", data.Action },
                        { "params", paramz }
                        }
                    ).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string res = response.Content.ReadAsStringAsync().Result;
                        var resultDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(res);                      
                        string roomId = resultDictionary["result"].ToString();
                        ResultViewModel resultViewModel = new ResultViewModel()
                        {
                            room_id = int.Parse(resultDictionary["result"].ToString()),
                            InsUser = user,
                            Title = "ایجاد اتاق",
                            Description = title + " " + "با شناسه " + roomId + " " + " با موفقیت ایجاد شد"
                        };
                        userRole.room_id = int.Parse(roomId);
                        userRole.RoomLink = _skyService.GetRoomLink(int.Parse(roomId));
                        _userService.UpdateUserRole(userRole);
                        await _userService.SaveChangesAsync();
                        return View(nameof(Result), resultViewModel);
                    }
                    else
                    {
                        return Content("Internal Server Error");
                    }
                }
                catch (AggregateException)
                {
                    return Content("Network Error");
                }
                catch (JsonReaderException)
                {
                    return Content("Invalid Response");
                }
            }
            else
            {
                return NotFound("مجاز به ایجاد کلاس نمی باشید !");
            }


        }

        public IActionResult Result()
        {
            return View();
        }
        public IActionResult ShowRoom(int? roomId)
        {
            if (roomId == null)
            {
                return NotFound("شناسه اتاق مشخص نشده است !");
            }
            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "getRoom",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };
            var paramz = new Dictionary<string, object>();
            paramz.Add("room_id", (int)roomId);
            response = HttpClient.PostAsJsonAsync<Dictionary<string, object>>(
                        data.APIUrl,
                        new Dictionary<String, object>
                        {
                            { "action", data.Action },
                            { "params", paramz }
                        }
                    ).Result;
           
            if (response.IsSuccessStatusCode)
            {

                // Read result from stream
                string res = response.Content.ReadAsStringAsync().Result;

                // Deserialize result
                var resultDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(res);

                SkyRoomViewModel roomViewModel = new SkyRoomViewModel();
                roomViewModel = JsonConvert.DeserializeObject<SkyRoomViewModel>(resultDictionary["result"].ToString());
                return View(roomViewModel);
                //return new JsonResult(resultDictionary["result"].ToString());
            }
            return NotFound("خطا رخ داده است !");
        }

        public IActionResult GetUserRooms(int? userId)
        {
            List<RoomReportViewModel> skyRoomViewModels = _skyService.GetUserRooms(userId);
            return View(skyRoomViewModels);
        }
        
    }
}
