using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PLCore.Security;
using PLCore.Services.Interfaces;
using PLDataLayer.Entities.Sale;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SteppedDiscountsController : Controller
    {
       
        private readonly ITrainingService _trainingService;
        public SteppedDiscountsController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
           
        }

        // GET: Admin/SteppedDiscounts
        [PermissionChecker(143)]
        public async Task<IActionResult> Index()
        {
            
            return View(await _trainingService.GetSteppedDiscountsAsync());
        }

        // GET: Admin/SteppedDiscounts/Details/5
        [PermissionChecker(146)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var steppedDiscount = await _trainingService.GetSteppedDiscountByIdAsync((int)id);
                
            if (steppedDiscount == null)
            {
                return NotFound();
            }

            return View(steppedDiscount);
        }

        // GET: Admin/SteppedDiscounts/Create
        [PermissionChecker(144)]
        public async Task<IActionResult> Create()
        {
            ViewData["TypeId"] = new SelectList(await _trainingService.GetSteppedDiscountTypesAsync(), "Id", "Title");
            List<string> preCodes = _trainingService.GetSteppedDiscounts().Select(s => s.Code).ToList();
            SteppedDiscount steppedDiscount = new SteppedDiscount()
            {
                Code = PLCore.Generators.GeneratorClass.GenerateKey(preCodes.ToList(), 6)
            };
            return View(steppedDiscount);
        }

        // POST: Admin/SteppedDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(144)]
        public async Task<IActionResult> Create(SteppedDiscount steppedDiscount)
        {
            if (ModelState.IsValid)
            {
                if (await _trainingService.ExistSteppedDiscount(steppedDiscount.Code))
                {
                    ModelState.AddModelError("Code", "کد تخفیف تکراری است !");
                    return View(steppedDiscount);
                }
                steppedDiscount.RegDate = DateTime.Now;
                _trainingService.CreateSteppedDiscount(steppedDiscount);
                await _trainingService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(await _trainingService.GetSteppedDiscountTypesAsync(), "Id", "Title", steppedDiscount.TypeId);
            return View(steppedDiscount);
        }

        // GET: Admin/SteppedDiscounts/Edit/5
        [PermissionChecker(145)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var steppedDiscount = await _trainingService.GetSteppedDiscountByIdAsync((int)id);
            if (steppedDiscount == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(await _trainingService.GetSteppedDiscountTypesAsync(), "Id", "Title", steppedDiscount.TypeId);
            return View(steppedDiscount);
        }

        // POST: Admin/SteppedDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(145)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Comment,TypeId,RegDate,IsActive")] SteppedDiscount steppedDiscount)
        {
            if (id != steppedDiscount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _trainingService.EditSteppedDiscount(steppedDiscount);
                    await _trainingService.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_trainingService.ExistSteppedDiscount(steppedDiscount.Id))
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
            ViewData["TypeId"] = new SelectList(await _trainingService.GetSteppedDiscountTypesAsync(), "Id", "Title", steppedDiscount.TypeId);
            return View(steppedDiscount);
        }

        // GET: Admin/SteppedDiscounts/Delete/5
        [PermissionChecker(147)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var steppedDiscount = await _trainingService.GetSteppedDiscountByIdAsync((int)id);
            if (steppedDiscount == null)
            {
                return NotFound();
            }

            return View(steppedDiscount);
        }

        // POST: Admin/SteppedDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(147)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            _trainingService.RemoveSteppedDiscount(id);
            await _trainingService.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
