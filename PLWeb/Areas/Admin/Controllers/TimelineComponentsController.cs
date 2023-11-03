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
    public class TimelineComponentsController : Controller
    {

        private readonly ISubEntityService _subEntityService;
        public TimelineComponentsController(ISubEntityService subEntityService)
        {
            _subEntityService = subEntityService;

        }

        // GET: Admin/TimelineComponents
        [PermissionChecker(98)]
        public async Task<IActionResult> Index(int? tlId)
        {
            ViewBag.tlid = tlId;
            if (tlId == null)
            {
                return View(await _subEntityService.GetTimelineComponentsAsync());
            }
            return View(await _subEntityService.GetTimelineComponentsOfTimeLineAsync((int)tlId));
        }

        // GET: Admin/TimelineComponents/Details/5
        [PermissionChecker(101)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timelineComponent = await _subEntityService.GetTimelineComponentByIdAsync((int)id);
            if (timelineComponent == null)
            {
                return NotFound();
            }

            return View(timelineComponent);
        }

        // GET: Admin/TimelineComponents/Create
        [PermissionChecker(99)]
        public IActionResult Create(int tlId)
        {
            TimelineComponent timelineComponent = new TimelineComponent
            {
                TL_Id = tlId,

            };
            return View(timelineComponent);
        }

        // POST: Admin/TimelineComponents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(99)]
        public async Task<IActionResult> Create(TimelineComponent timelineComponent, IFormFile TC_Image)
        {
            if (!ModelState.IsValid)
            {

                return View(timelineComponent);
            }
            if (TC_Image == null)
            {
                ModelState.AddModelError("TC_Image", "لطفا تصویر را انتخاب کنید !");
                return View(timelineComponent);
            }
            if (TC_Image != null)
            {
                if (TC_Image.Length > .05 * 1024 * 1024)
                {
                    ModelState.AddModelError("TC_Image", "حجم عکس از 50 کیلو بایت بیشتر است");
                    return View(timelineComponent);
                }
            }
            #region saveTCImage
            if (TC_Image != null)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/featured", TC_Image.FileName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                string tcimegeName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(TC_Image.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/featured", tcimegeName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    TC_Image.CopyTo(stream);
                }
                timelineComponent.TC_Image = tcimegeName;
            }
            #endregion
            await _subEntityService.CreateTimeLineComponentAsync(timelineComponent);
            await _subEntityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { tlId = timelineComponent.TL_Id });

        }

        // GET: Admin/TimelineComponents/Edit/5
        [PermissionChecker(102)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timelineComponent = await _subEntityService.GetTimelineComponentByIdAsync((int)id);
            if (timelineComponent == null)
            {
                return NotFound();
            }
            ViewData["TL_Id"] = new SelectList(await _subEntityService.GetTimelinesAsync(), "TL_Id", "TL_Text", timelineComponent.TL_Id);
            return View(timelineComponent);
        }

        // POST: Admin/TimelineComponents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(100)]
        public async Task<IActionResult> Edit(int id, TimelineComponent timelineComponent, IFormFile TC_Image)
        {
            if (id != timelineComponent.TC_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    #region saveTCImage
                    if (TC_Image != null)
                    {
                        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/featured", TC_Image.FileName);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        string tcimegeName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(TC_Image.FileName);
                        imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/featured", tcimegeName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            TC_Image.CopyTo(stream);
                        }
                        timelineComponent.TC_Image = tcimegeName;
                    }
                    #endregion
                    _subEntityService.UpdateTimeLineComponent(timelineComponent);
                    await _subEntityService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimelineComponentExists(timelineComponent.TC_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { tlId = timelineComponent.TL_Id });
            }

            return View(timelineComponent);
        }

        // GET: Admin/TimelineComponents/Delete/5
        [PermissionChecker(102)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timelineComponent = await _subEntityService.GetTimelineComponentByIdAsync((int)id);
            if (timelineComponent == null)
            {
                return NotFound();
            }

            return View(timelineComponent);
        }

        // POST: Admin/TimelineComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(102)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TimelineComponent timelineComponent = await _subEntityService.GetTimelineComponentByIdAsync(id);
            _subEntityService.RemoveSlider(id);
            await _subEntityService.SaveChangesAsync();
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/featured", timelineComponent.TC_Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TimelineComponentExists(int id)
        {
            return _subEntityService.ExistTLC(id);
        }
    }
}
