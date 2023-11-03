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
    public class AboutsController : Controller
    {
        private readonly PLContext _context;
        private readonly ISubEntityService _subEntityService;
        public AboutsController(ISubEntityService subEntityService, PLContext context)
        {
            _subEntityService = subEntityService;
            _context = context;
        }

        // GET: Admin/Abouts1
        [PermissionChecker(103)]
        public async Task<IActionResult> Index()
        {
            return View(await _subEntityService.GetAboutsAsync().ConfigureAwait(true));
        }

        // GET: Admin/Abouts1/Details/5
        [PermissionChecker(106)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _subEntityService.GetAboutByIdAsync((int)id).ConfigureAwait(true);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // GET: Admin/Abouts1/Create
        [PermissionChecker(104)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Abouts1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(104)]
        public async Task<IActionResult> Create(About about,IFormFile About_Image,IFormFile About_HImage)
        {
            if(!ModelState.IsValid)
            {
                return View(about);
            }
            if (About_Image == null)
            {
                ModelState.AddModelError("About_Image", "لطفا تصویر را انتخاب کنید !");
                return View(about);
            }
            if (About_Image != null)
            {
                if (About_Image.Length > .05 * 1025 * 1024)
                {
                    ModelState.AddModelError("About_Image", "حجم عکس از 50 کیلو بایت بیشتر است");
                    return View(about);
                }
            }
            if(About_HImage!=null)
            {
                if (About_Image.Length > .05 * 1025 * 1024)
                {
                    ModelState.AddModelError("About_HImage", "حجم عکس از 50 کیلو بایت بیشتر است");
                    return View(about);
                }
            }
            #region saveAboutImage
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about", About_Image.FileName);
            string aboutimgName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(About_Image.FileName);
            about.About_Image = aboutimgName;
            //====================
            string imagehPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about", About_HImage.FileName);
            string abouthimgName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(About_HImage.FileName);
            about.About_HImage = abouthimgName;

            about.OP_Create = User.Identity.Name;
            #endregion saveAboutImage
            await _subEntityService.CreateAboutAsync(about);
            await _subEntityService.SaveChangesAsync();
            imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about", aboutimgName);
            imagehPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about", abouthimgName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                About_Image.CopyTo(stream);
            }
            using (var stream = new FileStream(imagehPath, FileMode.Create))
            {
                About_HImage.CopyTo(stream);
            }
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Admin/Abouts1/Edit/5
        [PermissionChecker(105)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.Aboutes.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: Admin/Abouts1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(105)]
        public async Task<IActionResult> Edit(int id, About about,IFormFile About_Image,IFormFile About_HImage)
        {
            if (id != about.About_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (About_Image != null)
                    {
                        if (About_Image.Length > .05 * 1025 * 1024)
                        {
                            ModelState.AddModelError("About_Image", "حجم عکس از 50 کیلو بایت بیشتر است");
                            return View(about);
                        }
                        #region saveAboutImage
                        
                        string beforimgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about", about.About_Image);
                        if (System.IO.File.Exists(beforimgPath))
                        {
                            System.IO.File.Delete(beforimgPath);
                        }
                        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about", About_Image.FileName);
                        string aboutimgName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(About_Image.FileName);
                        about.About_Image = aboutimgName;
                        imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about", aboutimgName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            About_Image.CopyTo(stream);
                        }
                        #endregion saveAboutImage
                    }
                    if(About_HImage!=null)
                    {
                        if (About_HImage.Length > .05 * 1025 * 1024)
                        {
                            ModelState.AddModelError("About_HImage", "حجم عکس از 50 کیلو بایت بیشتر است");
                            return View(about);
                        }
                        #region saveAbouthImage

                        string beforhimgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about", about.About_Image);
                        if (System.IO.File.Exists(beforhimgPath))
                        {
                            System.IO.File.Delete(beforhimgPath);
                        }
                        string imagehPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about", About_HImage.FileName);
                        string abouthimgName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(About_HImage.FileName);
                        about.About_HImage = abouthimgName;
                        imagehPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about", abouthimgName);
                        using (var stream = new FileStream(imagehPath, FileMode.Create))
                        {
                            About_HImage.CopyTo(stream);
                        }
                        #endregion
                    }
                    about.OP_Update = User.Identity.Name;
                     _subEntityService.UpdateAbout(about);
                    await _subEntityService.SaveChangesAsync();     
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.About_Id))
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
            return View(about);
        }

        // GET: Admin/Abouts1/Delete/5
        [PermissionChecker(107)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.Aboutes
                .FirstOrDefaultAsync(m => m.About_Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // POST: Admin/Abouts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(107)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var about = await _context.Aboutes.FindAsync(id);
            about.OP_Remove = User.Identity.Name;
            _context.Aboutes.Remove(about);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutExists(int id)
        {
            return _context.Aboutes.Any(e => e.About_Id == id);
        }
    }
}
