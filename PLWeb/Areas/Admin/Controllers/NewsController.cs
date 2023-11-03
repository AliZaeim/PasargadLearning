using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PLCore.Convertors;
using PLCore.Generators;
using PLCore.Security;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using PLDataLayer.Entities.Blog;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NewsController : Controller
    {

        private readonly IBlogService _blogService;
        public NewsController(IBlogService blogService)
        {
            _blogService = blogService;

        }

        // GET: Admin/News
        [PermissionChecker(81)]
        public async Task<IActionResult> Index()
        {

            return View(await _blogService.GetNewsAsync());
        }

        // GET: Admin/News/Details/5
        [PermissionChecker(84)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _blogService.GetNewsByIdAsync((int)id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        [PermissionChecker(82)]
        public async Task<IActionResult> Create()
        {
            ViewData["NewsGroup_Id"] = new SelectList(await _blogService.GetNewsGroupsAsync(), "NewsGroup_Id", "NewsGroup_Title");
            ViewData["Publisher_Id"] = new SelectList(await _blogService.GetPublishersAsync(), "Publisher_Id", "Publisher_Title");
            List<string> codes = _blogService.GetNewsAsync().Result.Select(s => s.News_Code).ToList();
            News news = new News()
            {
                News_Date = DateTime.Now,
                News_Code = PLCore.Generators.GeneratorClass.GenerateKey(codes, 4)

            };
            return View(news);
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(82)]
        public async Task<IActionResult> Create(News news, IFormFile News_Image)
        {
            if (!ModelState.IsValid)
            {
                ViewData["NewsGroup_Id"] = new SelectList(await _blogService.GetNewsGroupsAsync(), "NewsGroup_Id", "NewsGroup_Title", news.NewsGroup_Id);
                ViewData["Publisher_Id"] = new SelectList(await _blogService.GetPublishersAsync(), "Publisher_Id", "Publisher_Title", news.Publisher_Id);
                return View(news);
            }
            if (News_Image != null)
            {
                if (News_Image.Length > .05 * 1024 * 1024)
                {
                    ModelState.AddModelError("News_Image", "حجم تصویر بیشتر از 50 کیلو بایت است !");
                    ViewData["NewsGroup_Id"] = new SelectList(await _blogService.GetNewsGroupsAsync(), "NewsGroup_Id", "NewsGroup_Title", news.NewsGroup_Id);
                    ViewData["Publisher_Id"] = new SelectList(await _blogService.GetPublishersAsync(), "Publisher_Id", "Publisher_Title", news.Publisher_Id);
                    return View(news);
                }

            }
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blog", News_Image.FileName);
            string newsfileName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(News_Image.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blog", newsfileName);
            news.News_Image = newsfileName;
            news.News_Date = DateTime.Now;
            news.OP_Create = User.Identity.Name + DateTime.Now.ToShamsiWithTime();
            _blogService.CreateNews(news);
            await _blogService.SaveChangesAsync();
            #region saveNewsFile

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await News_Image.CopyToAsync(stream);
            }
            #endregion saveNewsFile

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/News/Edit/5
        [PermissionChecker(83)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _blogService.GetNewsByIdAsync((int)id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["NewsGroup_Id"] = new SelectList(await _blogService.GetNewsGroupsAsync(), "NewsGroup_Id", "NewsGroup_Title", news.NewsGroup_Id);
            ViewData["Publisher_Id"] = new SelectList(await _blogService.GetPublishersAsync(), "Publisher_Id", "Publisher_Title", news.Publisher_Id);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(83)]
        public async Task<IActionResult> Edit(int News_Id, News news, IFormFile News_Image)
        {
            if (News_Id != news.News_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (News_Image != null)
                    {
                        if (News_Image.Length > .05 * 1024 * 1024)
                        {
                            ViewData["NewsGroup_Id"] = new SelectList(await _blogService.GetNewsGroupsAsync(), "NewsGroup_Id", "NewsGroup_Title", news.NewsGroup_Id);
                            ViewData["Publisher_Id"] = new SelectList(await _blogService.GetPublishersAsync(), "Publisher_Id", "Publisher_Title", news.Publisher_Id);
                            ModelState.AddModelError("News_Image", "حجم تصویر بیشتر از 50 کیلو بایت است !");
                            return View(news);
                        }

                    }
                    if (News_Image != null)
                    {
                        string curimgFile = news.News_Image;
                        if (!string.IsNullOrEmpty(curimgFile))
                        {
                            string currentimgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blog", curimgFile);
                            if (System.IO.File.Exists(currentimgPath))
                            {
                                System.IO.File.Delete(currentimgPath);
                            }
                        }

                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blog", News_Image.FileName);
                        string newsfileName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(News_Image.FileName);
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blog", newsfileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await News_Image.CopyToAsync(stream);
                        }
                        news.News_Image = newsfileName;
                    }
                    news.OP_Update +="-"+ User.Identity.Name+"|" + DateTime.Now.ToShamsiWithTime();
                    _blogService.UpdateNews(news);
                    await _blogService.SaveChangesAsync();
                    #region saveNewsFile

                    #endregion saveNewsFile

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.News_Id))
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
            ViewData["NewsGroup_Id"] = new SelectList(await _blogService.GetNewsGroupsAsync(), "NewsGroup_Id", "NewsGroup_Title", news.NewsGroup_Id);
            ViewData["Publisher_Id"] = new SelectList(await _blogService.GetPublishersAsync(), "Publisher_Id", "Publisher_Title", news.Publisher_Id);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        [PermissionChecker(86)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _blogService.GetNewsByIdAsync((int)id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(85)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            News news = await _blogService.GetNewsByIdAsync(id);
            news.IsDeleted = true;
            news.OP_Remove = User.Identity.Name;
            _blogService.UpdateNews(news);
            await _blogService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [PermissionChecker(86)]
        public IActionResult InsertFiles(int newsId)
        {
            NewsFile newsFile = new NewsFile()
            {
                News_Id = newsId,

            };
            return View(newsFile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        [PermissionChecker(86)]
        public async Task<IActionResult> InsertFiles(NewsFile newsFile, IFormFile NF_File)
        {
            if (!ModelState.IsValid)
            {
                return View(newsFile);
            }
            if (NF_File == null)
            {
                ModelState.AddModelError("NF_File", "لطفا فایل را انتخاب کنید !");
                return View(newsFile);
            }
            string extension = Path.GetExtension(NF_File.FileName);
            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".mp4", ".m4v", ".mpeg", ".mp3", ".pdf" };
            if (!allowedExtensions.Any(a => a == extension))
            {
                ModelState.AddModelError("NF_File", "فایل با این فرمت قابل آپلود نیست !");

                return View(newsFile);
            }
            #region saveNewsFile
            string path = Path.GetTempFileName();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/newsFiles", NF_File.FileName);
            string newsfileName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(NF_File.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/newsFiles", newsfileName);
            newsFile.NF_Date = DateTime.Now;
            newsFile.NF_Type = NF_File.ContentType;
            newsFile.NF_File = newsfileName;
            #endregion saveNewsFile
            newsFile.OP_Create = User.Identity.Name;
            _blogService.CreateNewsFile(newsFile);
            await _blogService.SaveChangesAsync();


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await NF_File.CopyToAsync(stream);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> EditFileInfo(int id)
        {
            NewsFile newsFile = await _blogService.GetNewsFileByIdAsync(id);
            return View(newsFile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(86)]
        public async Task<IActionResult> EditFileInfo(NewsFile newsFile)
        {
            newsFile.OP_Update += "-" + User.Identity.Name +"|"+ DateTime.Now.ToShamsiWithTime();
            _blogService.UpdateNewsFile(newsFile);
            await _blogService.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = newsFile.News_Id });
        }
        public IActionResult NewsFileIndex()
        {
            return View();
        }
        [PermissionChecker(87)]
        public async Task<IActionResult> DeleteFile(int id)
        {
            NewsFile newsFile = await _blogService.GetNewsFileByIdAsync(id);
            return View(newsFile);
        }
        [HttpPost, ActionName("DeleteFile")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(87)]
        public async Task<IActionResult> DeleteFileConfirmed(int id)
        {
            NewsFile newsFile = await _blogService.GetNewsFileByIdAsync(id);
            newsFile.IsDeleted = true;
            newsFile.OP_Remove += "-" + User.Identity.Name + "|" + DateTime.Now.ToShamsiWithTime();
            _blogService.UpdateNewsFile(newsFile);
            await _blogService.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = newsFile.News_Id });
        }

        private bool NewsExists(int id)
        {
            return _blogService.ExistNews(id);
        }
    }
}
