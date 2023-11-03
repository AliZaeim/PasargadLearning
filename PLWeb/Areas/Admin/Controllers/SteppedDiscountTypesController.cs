using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PLCore.Security;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using PLDataLayer.Entities.Sale;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SteppedDiscountTypesController : Controller
    {
       
        private readonly ITrainingService _trainingService;
        public SteppedDiscountTypesController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
          
        }

        // GET: Admin/SteppedDiscountTypes
        [PermissionChecker(138)]
        public async Task<IActionResult> Index()
        {
            return View(await _trainingService.GetSteppedDiscountTypesAsync());
        }
        // GET: Admin/SteppedDiscountTypes/Create
        [PermissionChecker(139)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SteppedDiscountTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(139)]
        public async Task<IActionResult> Create(SteppedDiscountType steppedDiscountType)
        {
            if (ModelState.IsValid)
            {
                _trainingService.CreateSteppedDiscountType(steppedDiscountType);
                await _trainingService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(steppedDiscountType);
        }

        // GET: Admin/SteppedDiscountTypes/Edit/5
        [PermissionChecker(140)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var steppedDiscountType = await _trainingService.GetSteppedDiscountTypeByIdAsync((int)id);
            if (steppedDiscountType == null)
            {
                return NotFound();
            }
            return View(steppedDiscountType);
        }

        // POST: Admin/SteppedDiscountTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(140)]
        public async Task<IActionResult> Edit(int id, SteppedDiscountType steppedDiscountType)
        {
            if (id != steppedDiscountType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _trainingService.EditSteppedDiscountType(steppedDiscountType);
                    await _trainingService.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_trainingService.ExistSteppedDiscountType(steppedDiscountType.Id))
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
            return View(steppedDiscountType);
        }

        // GET: Admin/SteppedDiscountTypes/Delete/5
        [PermissionChecker(141)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var steppedDiscountType = await _trainingService.GetSteppedDiscountTypeByIdAsync((int)id);
            if (steppedDiscountType == null)
            {
                return NotFound();
            }

            return View(steppedDiscountType);
        }

        // POST: Admin/SteppedDiscountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(141)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _trainingService.RemoveSteppedDiscountType(id);
            await _trainingService.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
