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
    public class SlidersController : Controller
    {

        private readonly ISubEntityService _subEntityService;
        public SlidersController(ISubEntityService subEntityService)
        {

            _subEntityService = subEntityService;
        }

        // GET: Admin/Sliders
        [PermissionChecker(88)]
        public async Task<IActionResult> Index()
        {
            return View(await _subEntityService.GetSlidersAsync());
        }

        // GET: Admin/Sliders/Details/5
        [PermissionChecker(91)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _subEntityService.GetSliderByIdAsync((int)id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Admin/Sliders/Create
        [PermissionChecker(89)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(89)]
        public async Task<IActionResult> Create(Slider slider, IFormFile SliderImage)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            if (SliderImage == null)
            {
                ModelState.AddModelError("SliderImage", "لطفا تصویر اسلایدر را انتخاب کنید !");
                return View(slider);
            }
            if (SliderImage != null)
            {
                if (SliderImage.Length > .05 * 1025 * 1024)
                {
                    ModelState.AddModelError("SliderImage", "حجم عکس از 50 کیلو بایت بیشتر است");
                    return View(slider);
                }
            }
            #region saveSliderImage
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/slider", SliderImage.FileName);

            string sliderName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(SliderImage.FileName);

            slider.SliderImage = sliderName;
            #endregion saveSliderImage
            await _subEntityService.CreateSliderAsync(slider);
            await _subEntityService.SaveChangesAsync();
            imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/slider", sliderName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                SliderImage.CopyTo(stream);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Sliders/Edit/5
        [PermissionChecker(90)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _subEntityService.GetSliderByIdAsync((int)id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(90)]
        public async Task<IActionResult> Edit(int id, Slider slider, IFormFile SliderImage)
        {
            if (id != slider.SliderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (SliderImage != null)
                    {
                        if (SliderImage.Length > .05 * 1025 * 1024)
                        {
                            ModelState.AddModelError("SliderImage", "حجم عکس از 50 کیلو بایت بیشتر است");
                            return View(slider);
                        }
                        #region saveSliderImage
                        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/slider", SliderImage.FileName);
                        if (!string.IsNullOrEmpty(slider.SliderImage))
                        {
                            string sliderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/slider", slider.SliderImage);
                            if (System.IO.File.Exists(sliderPath))
                            {
                                System.IO.File.Delete(sliderPath);
                            }
                        }
                        string sliderName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(SliderImage.FileName);
                        imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/slider", sliderName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            SliderImage.CopyTo(stream);
                        }
                        slider.SliderImage = sliderName;
                        #endregion saveSliderImage
                    }
                    _subEntityService.UpdateSlider(slider);
                    await _subEntityService.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SliderId))
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
            return View(slider);
        }

        // GET: Admin/Sliders/Delete/5
        [PermissionChecker(92)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _subEntityService.GetSliderByIdAsync((int)id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Admin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(92)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _subEntityService.RemoveSlider(id);
            await _subEntityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
            return _subEntityService.GetSlidersAsync().Result.Any(a => a.SliderId == id);
        }
    }
}
