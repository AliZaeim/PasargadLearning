using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCore.Generators;
using PLCore.Security;
using PLCore.Secutiry;
using PLCore.Services.Interfaces;
using PLDataLayer.Entities.SubEntities;

namespace VigiMarket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class InstaPostsController : Controller
    {
        private readonly ISubEntityService _subscriberService;

        public InstaPostsController(ISubEntityService subscriberService)
        {
            _subscriberService = subscriberService;
        }
        [PermissionChecker(118)]
        public async Task<IActionResult> Index()
        {
            return View(await _subscriberService.GetInstaPosts());
        }
        [PermissionChecker(119)]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(119)]
        public IActionResult Create(InstaPost instaPost, IFormFile InstaPostImage)
        {
            if (!ModelState.IsValid)
            {
                return View(instaPost);
            }
            if (InstaPostImage == null)
            {
                ModelState.AddModelError("InstaPostImage", "لطفا برای پست تصویر انتخاب کنید");
                return View(instaPost);
            }

            if (InstaPostImage != null)
            {
                if (InstaPostImage.Length > .05 * 1024 * 1024)
                {

                    ModelState.AddModelError("InstaPostImage", "حجم تصویر از 50 کیلو بایت نمی تواند بیشتر باشد ");
                    return View(instaPost);
                }
            }
            if (InstaPostImage != null)
            {
                string imagePath = "";
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/instagram", InstaPostImage.FileName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                string ImageName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(InstaPostImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/instagram", ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    InstaPostImage.CopyTo(stream);
                }
                instaPost.InstaPostImage = ImageName;
            }

            instaPost.InstaPostDateTime = DateTime.Now;
            _subscriberService.Create_Insta(instaPost);
            _subscriberService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [PermissionChecker(120)]
        public async Task<IActionResult> Edit(int id)
        {
            InstaPost instaPost = await _subscriberService.GetInstaPostWithId(id);
            return View(instaPost);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(120)]
        public IActionResult Edit(InstaPost instaPost, IFormFile InstaPostImage)
        {
            if (!ModelState.IsValid)
            {
                return View(instaPost);
            }
            if (instaPost.InstaPostImage == null)
            {
                ModelState.AddModelError("InstaPostImage", "لطفا برای پست تصویر انتخاب کنید");
                return View(instaPost);
            }

            if (InstaPostImage != null)
            {
                if (InstaPostImage.Length > .05 * 1024 * 1024)
                {

                    ModelState.AddModelError("InstaPostImage", "حجم تصویر از 50 کیلو بایت نمی تواند بیشتر باشد ");
                    return View(instaPost);
                }
            }
            if (InstaPostImage != null)
            {
                if (!string.IsNullOrEmpty(instaPost.InstaPostImage))
                {
                    string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/instagram", instaPost.InstaPostImage);
                    if (System.IO.File.Exists(currentImagePath))
                    {
                        System.IO.File.Delete(currentImagePath);
                    }
                }

                string imagePath = "";
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/instagram", instaPost.InstaPostImage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                string ImageName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(InstaPostImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/instagram", ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    InstaPostImage.CopyTo(stream);
                }
                instaPost.InstaPostImage = ImageName;
            }


            _subscriberService.Edit_Insta(instaPost);
            _subscriberService.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        [PermissionChecker(121)]
        public async Task<IActionResult> Delete(int id)
        {
            InstaPost instaPost = await _subscriberService.GetInstaPostWithId(id);
            return View(instaPost);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(121)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            InstaPost instaPost = await _subscriberService.GetInstaPostWithId(id);

            string imagePath = "";
            imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/instagram", instaPost.InstaPostImage);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _subscriberService.RemoveInstaPost(id);
            await _subscriberService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}