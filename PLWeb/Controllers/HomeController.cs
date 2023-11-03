using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLCore.DTOs.Course;
using PLCore.DTOs.General;
using PLCore.DTOs.PUser;
using PLCore.Services.Interfaces;
using PLDataLayer.Entities.Blog;
using PLDataLayer.Entities.SubEntities;
using PLDataLayer.Entities.Training;
using PLDataLayer.Entities.User;
using PLCore.Convertors;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net.Http;
using PLCore.Utility;

namespace PLWeb.Controllers
{
    public class HomeController : Controller
    {
        public static readonly HttpClient HttpClient = new HttpClient();
        private readonly IBlogService _blogService;
        private readonly ITrainingService _trainingService;
        private readonly IUserService _userService;
        private readonly ISubEntityService _subEntityService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IBlogService blogService, ITrainingService trainingService, IUserService userService, ISubEntityService subEntityService, IHttpContextAccessor httpContextAccessor)
        {
            _blogService = blogService;
            _trainingService = trainingService;
            _userService = userService;
            _subEntityService = subEntityService;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
           
            return View();
        }


        //public IActionResult GetSkyRoomUsers(int roomId)
        //{
        //    HttpResponseMessage response;
        //    PostData data = new PostData()
        //    {
        //        Action = "getRoomUsers",
        //        APIUrl = "https://www.skyroom.online/skyroom/api/apikey-76735-441-cd11c11a967dfa0f83e48276eca91bfe"
        //    };
        //    var paramz = new Dictionary<string, object>();
        //    paramz.Add("room_id", roomId);
        //    try
        //    {
        //        response = HttpClient.PostAsJsonAsync<Dictionary<string, object>>(
        //            data.APIUrl,
        //            new Dictionary<String, object>
        //            {
        //                { "action", data.Action },
        //                { "params", paramz }
        //            }
        //        )
        //        .Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Read result from stream
        //            string res = response.Content.ReadAsStringAsync().Result;

        //            // Deserialize result
        //            var resultDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(res);

        //            List<PLCore.DTOs.SkyRoom.SkyUserViewModel> Skyusers = new List<PLCore.DTOs.SkyRoom.SkyUserViewModel>();
        //            Skyusers = JsonConvert.DeserializeObject<List<PLCore.DTOs.SkyRoom.SkyUserViewModel>>(resultDictionary["result"].ToString());

        //            int count = resultDictionary.Count;

        //            // Send json to client
        //            //return new JsonResult(resultDictionary["result"].ToString());
        //            return View(Skyusers);
        //        }
        //        else
        //        {
        //            // Return error indicator
        //            return Content("Internal Server Error");
        //        }
        //    }
        //    catch (AggregateException)
        //    {
        //        return Content("Network Error");
        //    }
        //    catch (JsonReaderException)
        //    {
        //        // Should never occur
        //        return Content("Invalid Response");
        //    }
        //}
        [Route("about")]
        public IActionResult About()
        {
            About about = _subEntityService.GetAboutsAsync().Result.LastOrDefault();
            return View(about);
        }

        [HttpPost]
        public async Task<int> AddDown(int fileId)
        {
            CourseFile courseFile = await _trainingService.GetCourseFileByIdAsync(fileId);
            int down = courseFile.CF_Download + 1;
            courseFile.CF_Download = down;
            _trainingService.UpdateCourseFile(courseFile);
            await _trainingService.SaveAsync();
            return down;
        }
        public async Task<IActionResult> Courses(int? gid, string search)
        {
            PersianCalendar pc = new PersianCalendar();
            if (gid != null)
            {
                ShowCoursesViewModel showCoursesViewModel = new ShowCoursesViewModel();

                List<Course> courses = new List<Course>();

                CourseGroup courseGroup = await _trainingService.GetCourseGroupByIdAsync((int)gid);
                List<CourseGroup> courseGroups = await _trainingService.GetCourseGroupsAsync();
                showCoursesViewModel.CourseGroup = courseGroup;
                courses.AddRange(await _trainingService.GetCoursesByGroupId((int)gid));
                if (courseGroup.ParentId != null)
                {
                    CourseGroup pcg = await _trainingService.GetCourseGroupByIdAsync((int)courseGroup.ParentId);
                    showCoursesViewModel.ParentCourseGroup = pcg;
                    if (pcg.ParentId != null)
                    {
                        CourseGroup ppcg = await _trainingService.GetCourseGroupByIdAsync((int)pcg.ParentId);
                        showCoursesViewModel.ParentofParentCourseGroup = ppcg;
                    }
                }

                foreach (var g in courseGroup.CourseGroups)
                {
                    CourseGroup ch1 = await _trainingService.GetCourseGroupByIdAsync(g.CourseGroup_Id);
                    if (ch1.Courses != null)
                    {
                        courses.AddRange(await _trainingService.GetCoursesByGroupId(g.CourseGroup_Id));
                    }

                    foreach (var ch in ch1.CourseGroups)
                    {
                        CourseGroup ch2 = await _trainingService.GetCourseGroupByIdAsync(ch.CourseGroup_Id);
                        if (ch2.Courses != null)
                        {
                            courses.AddRange(await _trainingService.GetCoursesByGroupId(ch.CourseGroup_Id));
                        }

                    }
                }
                courses = courses.Where(w => w.Course_IsActive == true).OrderByDescending(r => r.Course_UpdateDate).ThenByDescending(r => r.Course_CreateDate).ToList();

                showCoursesViewModel.Courses = courses;


                return View(showCoursesViewModel);
            }
            else
            {
                if (!string.IsNullOrEmpty(search))
                {
                    if (search.Contains("/"))
                    {
                        string[] dte = search.Split("/");
                        ShowCoursesViewModel showCoursesViewModel = new ShowCoursesViewModel()
                        {

                            CourseGroup = null,
                            ParentCourseGroup = null,
                            ParentofParentCourseGroup = null,
                            SearchMode = true,
                            SerachText = "ماه" + " " + dte[1] + " " + "سال" + " " + dte[0]
                        };
                        if (dte != null)
                        {
                            int y = int.Parse(dte[0]);
                            int m = int.Parse(dte[1]);
                            showCoursesViewModel.Courses = await _trainingService.GetCoursesByMounth(y, m);
                            showCoursesViewModel.Courses = showCoursesViewModel.Courses.Where(w => w.Course_IsActive == true).OrderByDescending(r => r.Course_UpdateDate).ThenByDescending(r => r.Course_CreateDate).ToList();
                        }
                        else
                        {
                            showCoursesViewModel.Courses = await _trainingService.GetCoursesAsync();
                            showCoursesViewModel.Courses = showCoursesViewModel.Courses.Where(w => w.Course_IsActive == true).OrderByDescending(r => r.Course_UpdateDate).ThenByDescending(r => r.Course_CreateDate).ToList();
                        }
                        return View(showCoursesViewModel);
                    }
                    else
                    {

                        ShowCoursesViewModel showCoursesViewModel = new ShowCoursesViewModel()
                        {

                            Courses = await _trainingService.Search_in_Courses(search.Trim()),
                            CourseGroup = null,
                            ParentCourseGroup = null,
                            ParentofParentCourseGroup = null,
                            SearchMode = true,
                            SerachText = search.Trim()
                        };
                        if (search.StartsWith("t"))
                        {
                            int urid = int.Parse(search.Trim().Remove(0, 1));
                            UserRole userRole = await _userService.GetUserRoleByIdAsync(urid);
                            if (userRole != null)
                            {
                                search = "دوره های" + " " + userRole.User.UserFirstName + " " + userRole.User.UserFamily;
                                showCoursesViewModel.Courses = await _trainingService.GetCoursesByRole(urid);

                                showCoursesViewModel.SerachText = search.Trim();
                                showCoursesViewModel.SearchMode = false;
                            }


                        }
                        showCoursesViewModel.Courses = showCoursesViewModel.Courses.Where(w => w.Course_IsActive == true).OrderByDescending(r => r.Course_UpdateDate).ThenByDescending(r => r.Course_CreateDate).ToList();
                        return View(showCoursesViewModel);
                    }
                }
                else
                {
                    ShowCoursesViewModel showCoursesViewModel = new ShowCoursesViewModel()
                    {
                        Courses = await _trainingService.GetCoursesAsync(),
                        CourseGroup = null,
                        ParentCourseGroup = null,
                        ParentofParentCourseGroup = null,
                        SearchMode = false
                    };
                    showCoursesViewModel.Courses = showCoursesViewModel.Courses.Where(w => w.Course_IsActive == true).OrderByDescending(r => r.Course_UpdateDate).ThenByDescending(r => r.Course_CreateDate).ToList();
                    return View(showCoursesViewModel);
                }

            }

        }
        [HttpPost]

        public async Task<IActionResult> Courses(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {


                ShowCoursesViewModel showCoursesViewModel = new ShowCoursesViewModel()
                {

                    Courses = await _trainingService.Search_in_Courses(search.Trim()),
                    CourseGroup = null,
                    ParentCourseGroup = null,
                    ParentofParentCourseGroup = null,
                    SearchMode = true,
                    SerachText = search.Trim()
                };

                showCoursesViewModel.Courses = showCoursesViewModel.Courses.Where(w => w.Course_IsActive == true).OrderByDescending(r => r.Course_UpdateDate).ThenByDescending(r => r.Course_CreateDate).ToList();
                return View(showCoursesViewModel);
            }
            else
            {
                ShowCoursesViewModel showCoursesViewModel = new ShowCoursesViewModel()
                {
                    Courses = await _trainingService.GetCoursesAsync(),

                    CourseGroup = null,
                    ParentCourseGroup = null,
                    ParentofParentCourseGroup = null,
                    SearchMode = false,
                    SerachText = string.Empty
                };
                showCoursesViewModel.Courses = showCoursesViewModel.Courses.Where(w => w.Course_IsActive == true).OrderByDescending(r => r.Course_CreateDate).ToList();
                return View(showCoursesViewModel);
            }

        }

        public async Task<IActionResult> CourseDetails(int id, string key)
        {
            ViewData["key"] = key;
            ViewData["cid"] = id;
            Course course = await _trainingService.GetCourseAsync(id);
            if (course != null)
            {
                if (course.Course_IsActive == true)
                {
                    await _trainingService.UpdateCourseVisits(id);
                    _trainingService.Save();
                    return View(course);
                }
                else
                {
                    return NotFound("دوره مورد نظر یافت نشد !");
                }
            }
            else
            {
                return NotFound("دوره مورد نظر یافت نشد !");
            }

        }
        [Authorize]
        public async Task<IActionResult> AddUserRoleToCourse(int cId)
        {
            UserRole userRole = await _userService.GetUserRoleBy_UserName_RoleId(User.Identity.Name, 4);
            Course course = await _trainingService.GetCourseAsync(cId);

            List<UserRole> strudents = await _trainingService.GetCourseUsersByRoleAsync(course.CourseUsers.ToList(), 4, course.Course_Id);
            if (userRole == null)
            {
                return NotFound("کاربر مجوز ثبت نام ندارد !");
            }
            if (cId == 0)
            {
                return NotFound("دوره ای یافت نشد !");
            }
            if (course == null)
            {
                return NotFound("دوره ای یافت نشد !");
            }
            if (course.Course_IsActive == false)
            {
                return NotFound("دسترسی به دوره امکان پذیر نیست !");
            }
            if (DateTime.Now > course.Course_EndDateRegistration)
            {
                return NotFound("مهلت ثبت نام به پایان رسیده است !");
            }
            if (course.Course_Capacity - strudents.Where(w => w.IsActive == true).ToList().Count <= 0)
            {
                return NotFound("ظرفیت دوره تکمیل شده است !");
            }
            if (course.CourseUsers.Any(a => a.URId == userRole.URId))
            {
                return NotFound("شما قبلا در این دوره ثبت نام شده اید !");
            }

            CourseUser courseUser = new CourseUser()
            {
                URId = userRole.URId,
                Course_Id = course.Course_Id,
                CU_CreateDate = DateTime.Now
            };
            if (course.Course_Fee == 0)
            {
                courseUser.IsActive = true;
            }
            else
            {
                courseUser.IsActive = false;
                _trainingService.CreateCourseUser(courseUser);
                await _trainingService.SaveAsync();
                return RedirectToAction("PrepareFee", "Account", new { cId = course.Course_Id, urId = userRole.URId });

            }
            _trainingService.CreateCourseUser(courseUser);
            await _trainingService.SaveAsync();
            AddUserToCourseResultViewModel addUserToCourseResultViewModel = new AddUserToCourseResultViewModel()
            {
                IsSuccess = true,
                Course = course,
                UserRole = userRole
            };
            if (course.CourseStatus.CourseStatus_Title != "دانلودی")
            {
                return View(nameof(RegisterResult), addUserToCourseResultViewModel);
            }
            else
            {
                return Redirect("/Home/CourseDetails?id=" + course.Course_Id);
            }
            
        }
        public async Task<IActionResult> RegisterResult(int urId = 0, int cid = 0)
        {
            if (urId != 0 && cid != 0)
            {
                Course course = await _trainingService.GetCourseAsync(cid);
                UserRole userRole = await _userService.GetUserRoleByIdAsync(urId);
                AddUserToCourseResultViewModel addUserToCourseResultViewModel = new AddUserToCourseResultViewModel()
                {
                    IsSuccess = true,
                    Course = course,
                    UserRole = userRole
                };
                return PartialView(addUserToCourseResultViewModel);
            }
            return NotFound();

        }
        [Route("Teachers")]
        public async Task<IActionResult> Teachers()
        {
            List<UserRole> teachers = await _userService.GetTeachersAsync();
            return View(teachers);
        }
        public async Task<IActionResult> TeacherInfo(int urId)
        {
            var userrole = await _userService.GetUserRoleByIdAsync(urId);
            return View(userrole);
        }
        [Route("Archive")]
        public async Task<IActionResult> Archive(string type)
        {
            ArchiveViewModel archiveViewModel = new ArchiveViewModel();

            if (string.IsNullOrEmpty(type))
            {
                archiveViewModel.Type = "all";
                archiveViewModel.Title = "همه";
                archiveViewModel.News = null;
                return View(archiveViewModel);
            }
            else
            {
                type = type.Trim().ToLower();
                archiveViewModel.Type = type;
                if (type == "video")
                {
                    archiveViewModel.Title = "ویدئو ها";
                }
                if (type == "audio")
                {
                    archiveViewModel.Title = "صوت ها";
                }
                if (type == "application")
                {
                    archiveViewModel.Title = "فایل ها";
                }
                if (type == "image")
                {
                    archiveViewModel.Title = "تصاویر";
                }
                List<News> news = await _blogService.GetNewsAsync();
                archiveViewModel.News = news.Where(w => w.NewsFiles.Any(a => a.NF_Type.Contains(type))).ToList();
                archiveViewModel.NewsFiles = await _blogService.GetNewsFilesByTypeAsync(type);
                return View(archiveViewModel);
            }

        }
        [Route("News/{key?}")]
        public async Task<IActionResult> News(string key)
        {
            NewsViewModel newsViewModel = new NewsViewModel()
            {
                Key = key
            };
            if (string.IsNullOrEmpty(key))
            {
                newsViewModel.News = await _blogService.GetNewsAsync();
                return View(newsViewModel);
            }
            else
            {
                if (key.StartsWith("p"))
                {
                    int pid = 0;
                    newsViewModel.Key = key;
                    key = key.Remove(0, 1);
                    pid = int.Parse(key);
                    newsViewModel.Publisher = await _blogService.GetPublisherByIdAsync(pid);
                    List<News> news = await _blogService.GetNewsAsync();
                    newsViewModel.News = news.Where(w => w.Publisher_Id == pid).ToList();
                    newsViewModel.Key = "p" + newsViewModel.Publisher.Publisher_Title;
                    return View(newsViewModel);
                }
                if (key.StartsWith("g"))
                {
                    int gid = 0;
                    key = key.Remove(0, 1);
                    gid = int.Parse(key);
                    newsViewModel.NewsGroup = await _blogService.GetNewsGroupByIdAsync(gid);
                    newsViewModel.Key = "g" + newsViewModel.NewsGroup.NewsGroup_Title;
                    List<News> news = await _blogService.GetNewsAsync();
                    newsViewModel.News = news.Where(w => w.NewsGroup_Id == gid).ToList();

                    return View(newsViewModel);
                }
                if (key.StartsWith("d"))
                {
                    DateTime dte;
                    newsViewModel.Key = key;
                    key = key.Remove(0, 1);
                    key = key.Replace(" ", "/");
                    dte = DateTime.Parse(key);
                    newsViewModel.Date = dte;
                    List<News> news = await _blogService.GetNewsAsync();
                    newsViewModel.News = news.Where(w => w.News_Date.Date == dte).ToList();

                    return View(newsViewModel);
                }
                if (key.StartsWith("a"))
                {
                    newsViewModel.Key = key;
                    key = key.Remove(0, 1);

                    key = key.Replace(" ", "/");
                    string[] ym = key.Split("|");
                    int y = int.Parse(ym[0].ToString());
                    int m = int.Parse(ym[1].ToString());
                    PersianCalendar pc = new PersianCalendar();
                    List<News> news = await _blogService.GetNewsAsync();
                    newsViewModel.News = news.Where(w => pc.GetYear(w.News_Date) == y && pc.GetMonth(w.News_Date) == m).ToList();

                    return View(newsViewModel);
                }
                if (key.StartsWith("t"))
                {
                    newsViewModel.Key = key;
                    key = key.Remove(0, 1);
                    List<News> news = await _blogService.GetNewsAsync();
                    newsViewModel.News = news.Where(w => w.News_Text.Contains(key.Trim()) || w.News_Abstract.Contains(key.Trim()) || w.News_Title.Contains(key.Trim())).ToList();
                    return View(newsViewModel);
                }
                return View(new NewsViewModel());

            }

        }
        [HttpPost]
        [Route("News/{key?}")]
        public async Task<IActionResult> News(int? id, string key)
        {
            id = 10;
            NewsViewModel newsViewModel = new NewsViewModel()
            {
                Key = key
            };
            if (string.IsNullOrEmpty(key))
            {
                newsViewModel.News = await _blogService.GetNewsAsync();
                return View(newsViewModel);
            }
            else
            {

                newsViewModel.Key = "s" + key.Trim();
                List<News> newsS = await _blogService.GetNewsAsync();
                newsViewModel.News = await _blogService.SearchNews(key);
                return View(newsViewModel);
            }

        }

        [Route("News/d/{key}")]
        public async Task<ActionResult> NewsDetailes(string key, string srch)
        {
            if (string.IsNullOrEmpty(key))
            {
                return NotFound();
            }
            if (srch == null)
            {
                srch = "";
            }
            ViewData["key"] = srch;
            return View(await _blogService.GetNewsByCodeAsync(key.Trim()));
        }
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult SendMessage()
        {
            SendMessageViewModel sendMessageViewModel = new SendMessageViewModel()
            {
                IsSuccess = false
            };
            return PartialView(sendMessageViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(SendMessageViewModel sendMessageViewMoedel)
        {
            if (!ModelState.IsValid)
            {

                return PartialView(sendMessageViewMoedel);
            }
            ContactMessage contactMessage = new ContactMessage()
            {
                CM_Date = DateTime.Now,
                CM_Message = sendMessageViewMoedel.CM_Message,
                CM_FullName = sendMessageViewMoedel.CM_FullName,
                CM_Email = sendMessageViewMoedel.CM_Email,
                CM_Subject = sendMessageViewMoedel.CM_Subject
            };
            _subEntityService.CreateMessage(contactMessage);
            await _subEntityService.SaveChangesAsync();
            ModelState.Clear();
            sendMessageViewMoedel.CM_FullName = "";
            sendMessageViewMoedel.CM_Email = "";
            sendMessageViewMoedel.CM_Subject = "";
            sendMessageViewMoedel.CM_Message = "";
            sendMessageViewMoedel.IsSuccess = true;

            return PartialView(sendMessageViewMoedel);
        }
        public IActionResult FrAQu()
        {
            return View();
        }


        public IActionResult AskQuestion()
        {
            AskQuestionViewModel askQuestionViewModel = new AskQuestionViewModel()
            {
                IsSuccess = false
            };
            return PartialView(askQuestionViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AskQuestion(AskQuestionViewModel askQuestionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(askQuestionViewModel);
            }
            SiteFAQ siteFAQ = new SiteFAQ()
            {
                SiteFAQ_Name = askQuestionViewModel.SiteFAQ_Name,
                SiteFAQ_Subject = askQuestionViewModel.SiteFAQ_Subject,
                SiteFAQ_Email = askQuestionViewModel.SiteFAQ_Email,
                SiteFAQ_Date = DateTime.Now,
                SiteFAQ_Question = askQuestionViewModel.SiteFAQ_Question
            };
            _subEntityService.CreateSiteFAQ(siteFAQ);
            await _subEntityService.SaveChangesAsync();
            askQuestionViewModel.SiteFAQ_Email = string.Empty;
            askQuestionViewModel.SiteFAQ_Name = string.Empty;
            askQuestionViewModel.SiteFAQ_Question = string.Empty;
            askQuestionViewModel.SiteFAQ_Subject = string.Empty;
            askQuestionViewModel.IsSuccess = true;
            return PartialView(askQuestionViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Search(string srch)
        {
            if (!string.IsNullOrEmpty(srch))
            {
                SearchAllViewModel searchAllViewModel = new SearchAllViewModel()
                {
                    News = await _blogService.SearchNews(srch),
                    Courses = await _trainingService.Search_in_Courses(srch),
                    SearchText = srch
                };
                return View(nameof(SearchResult), searchAllViewModel);
            }
            else
            {
                return View(nameof(Index));
            }

        }
        public IActionResult SearchResult()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GetEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (_subEntityService.ExistEmail(email).Result == true)
                {
                    return "این ایمیل قبلا ثبت شده است !";
                }
                EmailBank emailBank = new EmailBank()
                {
                    EBEmail = email,
                    RegisterDate = DateTime.Now
                };
                _subEntityService.CreateEmailBank(emailBank);
                await _subEntityService.SaveChangesAsync();
                return "ایمیل ثبت شد";

            }
            else
            {
                return "ایمیل را وارد کنید";
            }



        }

    }
}