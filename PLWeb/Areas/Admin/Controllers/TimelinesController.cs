using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PLCore.Security;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using PLDataLayer.Entities.SubEntities;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TimelinesController : Controller
    {
        
        private readonly ISubEntityService _subEntityService;
        public TimelinesController(ISubEntityService subEntityService)
        {
            _subEntityService = subEntityService;
            
        }

        // GET: Admin/Timelines
        [PermissionChecker(93)]
        public async Task<IActionResult> Index()
        {
            return View(await _subEntityService.GetTimelinesAsync());
        }

        // GET: Admin/Timelines/Details/5
        [PermissionChecker(96)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeline = await _subEntityService.GetTiemLineByIdAsync((int)id);
            if (timeline == null)
            {
                return NotFound();
            }

            return View(timeline);
        }

        // GET: Admin/Timelines/Create
        [PermissionChecker(94)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Timelines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(94)]
        public async Task<IActionResult> Create(Timeline timeline)
        {
            if(!ModelState.IsValid)
            {
                return View(timeline);
            }
            
            _subEntityService.CreateTimeLine(timeline);
            await _subEntityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Timelines/Edit/5
        [PermissionChecker(95)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeline = await _subEntityService.GetTiemLineByIdAsync((int)id);
            if (timeline == null)
            {
                return NotFound();
            }
            return View(timeline);
        }

        // POST: Admin/Timelines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(95)]
        public async Task<IActionResult> Edit(int id, Timeline timeline)
        {
            if (id != timeline.TL_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _subEntityService.UpdateTimeLine(timeline);
                    await _subEntityService.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimelineExists(timeline.TL_Id))
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
            return View(timeline);
        }

        // GET: Admin/Timelines/Delete/5
        [PermissionChecker(97)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeline = await _subEntityService.GetTiemLineByIdAsync((int)id);
            if (timeline == null)
            {
                return NotFound();
            }

            return View(timeline);
        }

        // POST: Admin/Timelines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(97)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subEntityService.RemoveTimeLineByIdAsync(id);
            await _subEntityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimelineExists(int id)
        {
            return _subEntityService.TimeLineExist(id);
        }
    }
}
