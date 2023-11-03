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
using PLCore.Generators;
using PLCore.Security;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using PLDataLayer.Entities.SubEntities;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PageInfoesController : Controller
    {
       
        private readonly ISubEntityService _subEntityService;
        public PageInfoesController(ISubEntityService subEntityService)
        {
            _subEntityService = subEntityService;
            
        }

        // GET: Admin/PageInfoes
        [PermissionChecker(127)]
        public async Task<IActionResult> Index()
        {
            return View(await _subEntityService.GetPageInfos());
        }

        // GET: Admin/PageInfoes/Details/5
        [PermissionChecker(127)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageInfo = await _subEntityService.GetPageInfoById((int)id);
            if (pageInfo == null)
            {
                return NotFound();
            }

            return View(pageInfo);
        }

        // GET: Admin/PageInfoes/Create
        [PermissionChecker(128)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PageInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(128)]
        public async Task<IActionResult> Create(PageInfo pageInfo, IFormFile PInfo_Image)
        {
            if (!ModelState.IsValid)
            {
                return View(pageInfo);
            }
            if (PInfo_Image == null)
            {
                ModelState.AddModelError("PInfo_Image", "لطفا تصویر را انتخاب کنید !");
                return View(pageInfo);
            }
            if (PInfo_Image != null)
            {
                if (PInfo_Image.Length > .05 * 1024 * 1024)
                {
                    ModelState.AddModelError("PInfo_Image", "حجم تصویر حداکثر 50 کیلو بایت باشد !");
                    return View(pageInfo);
                }
            }
            if (PInfo_Image != null)
            {
                string imagePath = "";
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/page", PInfo_Image.FileName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                string ImageName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(PInfo_Image.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/page", ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    PInfo_Image.CopyTo(stream);
                }
                pageInfo.PInfo_Image = ImageName;
            }
            _subEntityService.CreatePageInfo(pageInfo);
            await _subEntityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        // GET: Admin/PageInfoes/Edit/5
        [PermissionChecker(129)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageInfo = await _subEntityService.GetPageInfoById((int)id);
            if (pageInfo == null)
            {
                return NotFound();
            }
            return View(pageInfo);
        }

        // POST: Admin/PageInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(129)]
        public async Task<IActionResult> Edit(int id, PageInfo pageInfo, IFormFile PInfo_Image)
        {
            if (id != pageInfo.PInfo_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (PInfo_Image != null)
                    {
                        if (PInfo_Image.Length > .05 * 1024 * 1024)
                        {
                            ModelState.AddModelError("PInfo_Image", "حجم تصویر حداکثر 50 کیلو بایت باشد !");
                            return View(pageInfo);
                        }
                    }
                    if (PInfo_Image != null)
                    {
                        string imagePath = "";
                        string nowimagePath = "";
                        string nowImgName = pageInfo.PInfo_Image;
                        nowimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/page", nowImgName);
                        imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/page", PInfo_Image.FileName);
                        if (System.IO.File.Exists(nowImgName))
                        {
                            System.IO.File.Delete(nowImgName);
                        }
                        string ImageName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(PInfo_Image.FileName);
                        imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/page", ImageName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            PInfo_Image.CopyTo(stream);
                        }
                        pageInfo.PInfo_Image = ImageName;
                    }
                    _subEntityService.UpdatePageInfo(pageInfo);
                    await _subEntityService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageInfoExists(pageInfo.PInfo_Id))
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
            return View(pageInfo);
        }

        // GET: Admin/PageInfoes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pageInfo = await _context.PageInfos
        //        .FirstOrDefaultAsync(m => m.PInfo_Id == id);
        //    if (pageInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(pageInfo);
        //}

        //// POST: Admin/PageInfoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var pageInfo = await _context.PageInfos.FindAsync(id);
        //    _context.PageInfos.Remove(pageInfo);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PageInfoExists(int id)
        {
            return _subEntityService.ExistPageInfo(id);
        }
    }
}
