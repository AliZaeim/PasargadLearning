using PLCore.Convertors;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLCore.DTOs.Course;
using PLCore.Services.Interfaces;
using PLDataLayer.Entities.Sale;
using PLCore.Security;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SteppedDiscountDetailsController : Controller
    {
        
        private readonly ITrainingService _trainingService;
        public SteppedDiscountDetailsController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
            
        }

        // GET: Admin/SteppedDiscountDetails
        [PermissionChecker(148)]
        public async Task<IActionResult> Index(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return View(await _trainingService.GetSteppedDiscountDetailsAsync());
            }
            else
            {
                ViewData["code"] = code;
                return View(await _trainingService.GetSteppedDiscountDetailsAsync(code));
            }
            
        }

        // GET: Admin/SteppedDiscountDetails/Details/5
        [PermissionChecker(151)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var steppedDiscountDetail = await _trainingService.GetSteppedDiscountDetailByIdAsync((int)id);
            if (steppedDiscountDetail == null)
            {
                return NotFound();
            }

            return View(steppedDiscountDetail);
        }

        // GET: Admin/SteppedDiscountDetails/Create
        [PermissionChecker(149)]
        public async Task<IActionResult> Create(string stcode)
        {
            if (string.IsNullOrEmpty(stcode))
            {
                return NotFound("کد تحفیف مشخص نشده است !");
            }

            SteppedDiscount steppedDiscount = await _trainingService.GetSteppedDiscountByCodeAsync(stcode);
            if (steppedDiscount == null)
            {
                return NotFound("کد تخفیف نامعتبر است !");
            }
            SteppedDiscountDatailsViewModel steppedDiscountDatailsViewModel = new SteppedDiscountDatailsViewModel()
            {
                Code = stcode,
                StId = steppedDiscount.Id,
                SteppedDiscount = steppedDiscount,
                type = steppedDiscount.SteppedDiscountType.Name,
                steppedDiscounts = await _trainingService.GetSteppedDiscountsAsync()
            };
            return View(steppedDiscountDatailsViewModel);
        }

        // POST: Admin/SteppedDiscountDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(149)]
        public async Task<IActionResult> Create(SteppedDiscountDatailsViewModel steppedDiscountDatailsViewModel)
        {
            if (!ModelState.IsValid)
            {

                steppedDiscountDatailsViewModel.SteppedDiscount = await _trainingService.GetSteppedDiscountByCodeAsync(steppedDiscountDatailsViewModel.Code);
                steppedDiscountDatailsViewModel.steppedDiscounts = await _trainingService.GetSteppedDiscountsAsync();
                return View(steppedDiscountDatailsViewModel);
            }
            SteppedDiscountDetail steppedDiscountDetail = new SteppedDiscountDetail();
            if (steppedDiscountDatailsViewModel.type == "person")
            {
                steppedDiscountDetail.FromPerson = steppedDiscountDatailsViewModel.FromPerson;
                steppedDiscountDetail.ToPerson = steppedDiscountDatailsViewModel.ToPerson;
                
            }
            if (steppedDiscountDatailsViewModel.type == "time")
            {
                steppedDiscountDetail.FromDate = DateConvertor.ChangeToMiladi(steppedDiscountDatailsViewModel.FromDate, steppedDiscountDatailsViewModel.FromTime);
                steppedDiscountDetail.ToDate = DateConvertor.ChangeToMiladi(steppedDiscountDatailsViewModel.ToDate, steppedDiscountDatailsViewModel.ToTime);
            }
            steppedDiscountDetail.StId = (int)steppedDiscountDatailsViewModel.StId;
            steppedDiscountDetail.Percent = (float)steppedDiscountDatailsViewModel.Percent;
            _trainingService.CreateSteppedDiscountDetail(steppedDiscountDetail);
            await _trainingService.SaveAsync();
            return RedirectToAction(nameof(Index), new { code = steppedDiscountDatailsViewModel.Code });




        }

        // GET: Admin/SteppedDiscountDetails/Edit/5
        [PermissionChecker(150)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var steppedDiscountDetail = await _trainingService.GetSteppedDiscountDetailByIdAsync((int)id);
            if (steppedDiscountDetail == null)
            {
                return NotFound();
            }
            SteppedDiscount steppedDiscount = await _trainingService.GetSteppedDiscountByIdAsync(steppedDiscountDetail.StId);
            SteppedDiscountDatailsViewModel steppedDiscountDatailsViewModel = new SteppedDiscountDatailsViewModel()
            {
                Id = steppedDiscountDetail.Id,
                StId = steppedDiscountDetail.StId,
                Code = steppedDiscount.Code,
                type = steppedDiscount.SteppedDiscountType.Name,
                Percent = steppedDiscountDetail.Percent
            };
            if (steppedDiscount.SteppedDiscountType.Name == "person")
            {
                steppedDiscountDatailsViewModel.FromPerson = steppedDiscountDetail.FromPerson;
                steppedDiscountDatailsViewModel.ToPerson = steppedDiscountDetail.ToPerson;
                
            }
            if (steppedDiscount.SteppedDiscountType.Name == "time")
            {
                steppedDiscountDatailsViewModel.FromDate = steppedDiscountDetail.FromDate.ToShamsiN();
                steppedDiscountDatailsViewModel.FromTime = steppedDiscountDetail.FromDate.Value.Hour.ToString("00") + ":" + steppedDiscountDetail.FromDate.Value.Minute.ToString("00");
                steppedDiscountDatailsViewModel.ToDate = steppedDiscountDetail.ToDate.ToShamsiN();
                steppedDiscountDatailsViewModel.ToTime = steppedDiscountDetail.ToDate.Value.Hour.ToString("00") + ":" + steppedDiscountDetail.ToDate.Value.Minute.ToString("00");
            }
            //ViewData["StId"] = new SelectList(_context.SteppedDiscounts, "Id", "Code", steppedDiscountDetail.StId);
            return View(steppedDiscountDatailsViewModel);
        }

        // POST: Admin/SteppedDiscountDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(150)]
        public async Task<IActionResult> Edit(int id, SteppedDiscountDatailsViewModel steppedDiscountDatailsViewModel)
        {
            if (id != steppedDiscountDatailsViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SteppedDiscountDetail steppedDiscountDetail = await _trainingService.GetSteppedDiscountDetailByIdAsync(steppedDiscountDatailsViewModel.Id).ConfigureAwait(false);
                    if (steppedDiscountDatailsViewModel.type == "person")
                    {
                        steppedDiscountDetail.FromPerson = steppedDiscountDatailsViewModel.FromPerson;
                        steppedDiscountDetail.ToPerson = steppedDiscountDatailsViewModel.ToPerson;

                    }
                    if (steppedDiscountDatailsViewModel.type == "time")
                    {
                        steppedDiscountDetail.FromDate = DateConvertor.ChangeToMiladi(steppedDiscountDatailsViewModel.FromDate, steppedDiscountDatailsViewModel.FromTime);
                        steppedDiscountDetail.ToDate = DateConvertor.ChangeToMiladi(steppedDiscountDatailsViewModel.ToDate, steppedDiscountDatailsViewModel.ToTime);
                    }
                    steppedDiscountDetail.StId = (int)steppedDiscountDatailsViewModel.StId;
                    steppedDiscountDetail.Percent = (float)steppedDiscountDatailsViewModel.Percent;
                    _trainingService.EditSteppedDiscountDetail(steppedDiscountDetail);
                    await _trainingService.SaveAsync();
                   
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_trainingService.ExistSteppedDiscountDetail(steppedDiscountDatailsViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { code = steppedDiscountDatailsViewModel.Code });
            }
           
            return View(steppedDiscountDatailsViewModel);
        }

        // GET: Admin/SteppedDiscountDetails/Delete/5
        [PermissionChecker(152)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var steppedDiscountDetail = await _trainingService.GetSteppedDiscountDetailByIdAsync((int)id);
            if (steppedDiscountDetail == null)
            {
                return NotFound();
            }

            return View(steppedDiscountDetail);
        }

        // POST: Admin/SteppedDiscountDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(152)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var steppedDiscountDetail = await _trainingService.GetSteppedDiscountDetailByIdAsync(id);
            string stcode = steppedDiscountDetail.SteppedDiscount.Code;
            _trainingService.RemoveSteppedDiscountDetail(id);
            await _trainingService.SaveAsync();
            return RedirectToAction(nameof(Index), new { code = stcode });
        }

        
    }
}
