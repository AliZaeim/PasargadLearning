using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PLCore.DTOs.SkyRoom;
using PLCore.Generators;
using PLCore.Services.Interfaces;
using PLCore.Utility;
using PLDataLayer.Context;
using PLDataLayer.Entities.SkyRooms;
using PLDataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PLCore.Services
{
    public class SkyService : ISkyService
    {
        public static readonly HttpClient HttpClient = new HttpClient();
        private readonly PLContext _pLContext;
        private readonly IUserService _userService;
        public SkyService(PLContext pLContext, IUserService userService)
        {
            _pLContext = pLContext;
            _userService = userService;
        }

        public bool AddStudentToRoom(int? roomId, int? userId)
        {
            if (roomId == null)
            {
                return false;
            }
            if (userId == null)
            {
                return false;
            }
            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "addUserRooms",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };

            var paramz = new Dictionary<string, object>();

            paramz.Add("user_id", userId);
            paramz.Add("rooms", new List<Dictionary<string, int>>
                    {
                        new Dictionary<string, int>
                        {
                            { "room_id", (int)roomId },
                            { "access", SkyRoomClient.USER_ACCESS_NORMAL}
                        }
                    });


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
                    string re = resultDictionary["ok"].ToString();
                    if (!string.IsNullOrEmpty(re))
                    {
                        if (re.ToLower() == "true")
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
                else
                {
                    return false;
                }
            }
            catch (AggregateException)
            {
                return false;
            }
            catch (JsonReaderException)
            {
                // Should never occur
                return false;
            }
        }
        public int RemoveStudentsFromRoom(int? roomId, List<int> users)
        {
            if (roomId == null)
            {
                return 0;
            }

            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "removeRoomUsers",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };

            var paramz = new Dictionary<string, object>();

            paramz.Add("room_id", roomId);
            paramz.Add("users", users);

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
                    string re = resultDictionary["ok"].ToString();
                    if (!string.IsNullOrEmpty(re))
                    {
                        if (re.ToLower() == "true")
                        {
                            return int.Parse(resultDictionary["result"].ToString());
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    return 0;
                }
            }
            catch (AggregateException)
            {
                return 0;
            }
            catch (JsonReaderException)
            {
                // Should never occur
                return 0;
            }
        }

        public bool AddTeacherToRoom(int? roomId, int? userId)
        {
            if (roomId == null)
            {
                return false;
            }
            if (userId == null)
            {
                return false;
            }
            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "addUserRooms",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };
            var paramz = new Dictionary<string, object>();

            paramz.Add("user_id", userId);
            paramz.Add("rooms", new List<Dictionary<string, int>>
                    {
                        new Dictionary<string, int>
                        {
                            { "room_id", (int)roomId },
                            { "access", PLDataLayer.Entities.SkyRooms.SkyRoomClient.USER_ACCESS_OPERATOR}
                        }
                    });
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (AggregateException)
            {
                return false;
            }
            catch (JsonReaderException)
            {
                // Should never occur
                return false;
            }
        }

        public async Task<int?> CreateRoomAsync(int? urId)
        {
            if (urId == null)
            {
                return null;
            }
            HttpResponseMessage response;
            PostData data = new PostData()
            {
                Action = "createRoom",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };
            UserRole userRole = await _userService.GetUserRoleByIdAsync((int)urId).ConfigureAwait(false);
            if (userRole.RoleId == 3)
            {
                var paramz = new Dictionary<string, object>();
                paramz.Add("name", "class_" + userRole.URId.ToString() + "_" + GeneratorClass.GeneratePassword(4, "digit"));
                string title = "اتاق آموزش آنلاین" + " " + userRole.User.UserFirstName + " " + userRole.User.UserFamily;
                paramz.Add("title", title);
                paramz.Add("description", "مرکز آموزشهای صنعت بیمه");
                paramz.Add("status", 1);
                paramz.Add("op_login_first", true);
                paramz.Add("guest_login", false);
                paramz.Add("max_users", 51);
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
                        // Read result from stream
                        string res = response.Content.ReadAsStringAsync().Result;
                        // Deserialize result
                        var resultDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(res);
                        string roomId = resultDictionary["result"].ToString();
                        ResultViewModel resultViewModel = new ResultViewModel()
                        {
                            room_id = int.Parse(resultDictionary["result"].ToString()),
                            InsUser = userRole.User,
                            Title = "ایجاد اتاق",
                            Description = title + " " + "با شناسه " + roomId + " " + " با موفقیت ایجاد شد"
                        };
                        int roomid = int.Parse(resultDictionary["result"].ToString());
                        userRole.room_id = int.Parse(roomId);
                        userRole.RoomLink = GetRoomLink(roomid);
                        _userService.UpdateUserRole(userRole);
                        await _userService.SaveChangesAsync();
                        return roomid;
                    }
                    else
                    {
                        // Return error indicator
                        return null;
                    }
                }
                catch (AggregateException)
                {
                    return null;
                }
                catch (JsonReaderException)
                {
                    // Should never occur
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<int?> CreateUserAsync(int? urId, string userNC)
        {
            if (urId == null && userNC == null)
            {
                return null;
            }
            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "createUser",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };
            UserRole userRole = null;
            PLDataLayer.Entities.User.User user = null;
            if (urId != null)
            {
                userRole = await _pLContext.UserRoles.Include(r => r.User).SingleOrDefaultAsync(s => s.URId == (int)urId);
            }
            var paramz = new Dictionary<string, object>();
            if (userRole != null)
            {
                paramz.Add("username", userRole.User.UserCellPhone);
                paramz.Add("nickname", userRole.User.UserFirstName + " " + userRole.User.UserFamily);
                paramz.Add("password", userRole.User.UserNC);
                paramz.Add("status", 1);
                paramz.Add("fname", userRole.User.UserFirstName);
                paramz.Add("lname", userRole.User.UserFamily);
                paramz.Add("is_public", false);
            }
            else
            {
                user = await _userService.GetUserByNC(userNC);
                if (user != null)
                {
                    paramz.Add("username", user.UserCellPhone);
                    paramz.Add("nickname", user.UserFirstName + " " + user.UserFamily);
                    paramz.Add("password", user.UserNC);
                    paramz.Add("status", 1);
                    paramz.Add("fname", user.UserFirstName);
                    paramz.Add("lname", user.UserFamily);
                    paramz.Add("is_public", false);
                }

            }

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
                    if (urId != null)
                    {
                        userRole.User.Sky_userId = int.Parse(resultDictionary["result"].ToString());
                        _userService.UpdateUserRole(userRole);
                    }
                    if (user != null)
                    {
                        user.Sky_userId = int.Parse(resultDictionary["result"].ToString());
                        _userService.UpdateUser(user);
                    }

                    await _userService.SaveChangesAsync();
                    return int.Parse(resultDictionary["result"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (AggregateException)
            {
                return null;
            }
            catch (JsonReaderException)
            {
                // Should never occur
                return null;
            }


        }

        public string GetRoomLink(int? roomId)
        {
            if (roomId == null)
            {
                return null;
            }
            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "getRoomUrl",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };

            var paramz = new Dictionary<string, object>();

            paramz.Add("room_id", (int)roomId);
            paramz.Add("language", "fa");


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
                    return resultDictionary["result"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (AggregateException)
            {
                return string.Empty;
            }
            catch (JsonReaderException)
            {
                // Should never occur
                return string.Empty;
            }
        }

        public List<SkyRoomViewModel> GetRooms()
        {
            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "getRooms",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };
            try
            {
                response = HttpClient.PostAsJsonAsync<Dictionary<string, object>>(
                    data.APIUrl,
                    new Dictionary<String, object>
                    {
                        { "action", data.Action }

                    }
                ).Result;
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    var resultDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(res, new KeyValuePairConverter());
                    string zres = resultDictionary["result"].ToString();
                    List<SkyRoomViewModel> skyUserViewModels = JsonConvert.DeserializeObject<List<DTOs.SkyRoom.SkyRoomViewModel>>(zres, new KeyValuePairConverter());
                    return skyUserViewModels;
                }
                else
                {
                    return null;
                }
            }
            catch (AggregateException)
            {
                return null;
            }
            catch (JsonReaderException)
            {
                // Should never occur
                return null;
            }
        }

        public List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel> GetRoomUsers(int? roomId)
        {
            if (roomId == null)
            {
                return null;
            }

            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "getRoomUsers",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };
            var paramz = new Dictionary<string, object>();

            paramz.Add("room_id", (int)roomId);

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
                    var resultDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(res, new KeyValuePairConverter());
                    string zres = resultDictionary["result"].ToString();
                    List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel> Skyusers = new List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel>();
                    Skyusers = JsonConvert.DeserializeObject<List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel>>(resultDictionary["result"].ToString());
                    
                    return Skyusers.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (AggregateException)
            {
                return null;
            }
            catch (JsonReaderException)
            {
                // Should never occur
                return null;
            }
        }

        public List<DTOs.SkyRoom.SkyUserViewModel> GetSkyUsers()
        {
            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "getUsers",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };
            try
            {
                response = HttpClient.PostAsJsonAsync<Dictionary<string, object>>(
                    data.APIUrl,
                    new Dictionary<String, object>
                    {
                        { "action", data.Action }

                    }
                ).Result;
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    var resultDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(res, new KeyValuePairConverter());
                    string zres = resultDictionary["result"].ToString();
                    List<DTOs.SkyRoom.SkyUserViewModel> skyUserViewModels = JsonConvert.DeserializeObject<List<DTOs.SkyRoom.SkyUserViewModel>>(zres, new KeyValuePairConverter());
                    return skyUserViewModels;
                }
                else
                {
                    return null;
                }
            }
            catch (AggregateException)
            {
                return null;
            }
            catch (JsonReaderException)
            {
                // Should never occur
                return null;
            }
        }

        public List<RoomReportViewModel> GetUserRooms(int? userId)
        {
            if (userId == null)
            {
                return null;
            }
            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "getUserRooms",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };
            var paramz = new Dictionary<string, object>();

            paramz.Add("user_id", (int)userId);
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
                    var resultDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(res, new KeyValuePairConverter());
                    string zres = resultDictionary["result"].ToString();
                    List<RoomReportViewModel> roomViewModels = JsonConvert.DeserializeObject<List<RoomReportViewModel>>(zres, new KeyValuePairConverter());
                    return roomViewModels;
                }
                else
                {
                    return null;
                }
            }
            catch (AggregateException)
            {
                return null;
            }
            catch (JsonReaderException)
            {
                // Should never occur
                return null;
            }
        }
        /// <summary>
        /// Ids is sky User Ids
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="Ids"></param>
        /// <returns></returns>

        public bool AddStudentsToRoom(int? roomId, List<int> Ids)
        {

            if (roomId == null)
            {
                return false;
            }
            if (Ids == null)
            {
                return false;
            }
            HttpResponseMessage response;

            PostData data = new PostData()
            {
                Action = "addRoomUsers",
                APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
            };

            var paramz = new Dictionary<string, object>();

            paramz.Add("room_id", roomId);
            List<Dictionary<string, int>> keyValues = new List<Dictionary<string, int>>();
            foreach (var item in Ids)
            {
                keyValues.Add(new Dictionary<string, int>
                        {
                            {"user_id", item },
                            {"access",  SkyRoomClient.USER_ACCESS_NORMAL }
                        });
            }
            
            paramz.Add("users", keyValues);


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
                    string re = resultDictionary["ok"].ToString();
                    if (!string.IsNullOrEmpty(re))
                    {
                        if (re.ToLower() == "true")
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
                else
                {
                    return false;
                }
            }
            catch (AggregateException)
            {
                return false;
            }
            catch (JsonReaderException)
            {
                // Should never occur
                return false;
            }

        }
    }
}
