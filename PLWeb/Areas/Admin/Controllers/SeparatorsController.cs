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
    public class SeparatorsController : Controller
    {
        private readonly PLContext _context;
        private readonly ISubEntityService _subEntityService;

        public SeparatorsController(ISubEntityService subEntityService, PLContext context)
        {
            _context = context;
            _subEntityService = subEntityService;
        }

        // GET: Admin/Separators
        [PermissionChecker(134)]
        public async Task<IActionResult> Index()
        {
            return View(await _subEntityService.GetSeparators());
        }

        // GET: Admin/Separators/Details/5
        [PermissionChecker(134)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var separator = await _context.Separators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (separator == null)
            {
                return NotFound();
            }

            return View(separator);
        }

        // GET: Admin/Separators/Create
        [PermissionChecker(135)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Separators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(135)]
        public async Task<IActionResult> Create(Separator separator, IFormFile BgImage)
        {
            if (!ModelState.IsValid)
            {
                return View(separator);
            }
            if (BgImage == null)
            {
                ModelState.AddModelError("BgImage", "لطفا تصویر را انتخاب کنید !");
                return View(separator);
            }
            if (BgImage != null)
            {
                if (BgImage.Length > .05 * 1024 * 1024)
                {
                    ModelState.AddModelError("BgImage", "حجم تصویر حداکثر 50 کیلوبایت می تواند باشد");
                    return View(separator);
                }
            }
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/bg", BgImage.FileName);
            string fileName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(BgImage.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/bg", fileName);
            separator.BgImage = fileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                BgImage.CopyTo(stream);
            }
            _subEntityService.CreateSeparator(separator);
            await _subEntityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/Separators/Edit/5
        [PermissionChecker(136)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var separator = await _subEntityService.GetSeparatorById((int)id);
            if (separator == null)
            {
                return NotFound();
            }
            return View(separator);
        }

        // POST: Admin/Separators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(136)]
        public async Task<IActionResult> Edit(int id, Separator separator, IFormFile BgImage)
        {
            if (id != separator.Id)
            {
                return NotFound();
            }

            if (BgImage != null)
            {
                if (BgImage.Length > .05 * 1024 * 1024)
                {
                    ModelState.AddModelError("BgImage", "حجم تصویر حداکثر 50 کیلوبایت می تواند باشد");
                    return View(separator);
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (BgImage != null)
                    {
                        string curfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/bg", separator.BgImage);
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/bg", BgImage.FileName);
                        string fileName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(BgImage.FileName);
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/bg", fileName);
                        separator.BgImage = fileName;
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            BgImage.CopyTo(stream);
                        }
                        if (System.IO.File.Exists(curfilepath))
                        {
                            System.IO.File.Delete(curfilepath);
                        }
                    }

                    _subEntityService.UpdateSeparator(separator);
                    await _subEntityService.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeparatorExists(separator.Id))
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
            return View(separator);
        }

        // GET: Admin/Separators/Delete/5
        [PermissionChecker(137)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var separator = await _subEntityService.GetSeparatorById((int)id);
            if (separator == null)
            {
                return NotFound();
            }

            return View(separator);
        }

        // POST: Admin/Separators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(137)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var separator = await _subEntityService.GetSeparatorById(id);
            string curfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/bg", separator.BgImage);
            if (System.IO.File.Exists(curfilepath))
            {
                System.IO.File.Delete(curfilepath);
            }
            await _subEntityService.RemoveSeparator(id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeparatorExists(int id)
        {
            return _subEntityService.ExistSeparator(id);
        }
    }
}
