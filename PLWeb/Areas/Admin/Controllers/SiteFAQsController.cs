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
    public class SiteFAQsController : Controller
    {
        
        private readonly ISubEntityService _subEntityService;
        public SiteFAQsController(ISubEntityService subEntityService)
        {
            _subEntityService = subEntityService;
            
        }

        // GET: Admin/SiteFAQs
        [PermissionChecker(122)]
        public async Task<IActionResult> Index()
        {
            return View(await _subEntityService.GetSiteFAQs());
        }

        // GET: Admin/SiteFAQs/Details/5
        [PermissionChecker(125)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteFAQ = await _subEntityService.GetSiteFAQById((int)id);
            if (siteFAQ == null)
            {
                return NotFound();
            }

            return View(siteFAQ);
        }

        // GET: Admin/SiteFAQs/Create
        [PermissionChecker(123)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SiteFAQs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(123)]
        public async Task<IActionResult> Create( SiteFAQ siteFAQ)
        {
            if (ModelState.IsValid)
            {
                siteFAQ.SiteFAQ_Date = DateTime.Now;
                siteFAQ.OP_Create = User.Identity.Name + " | " + DateTime.Now;
                _subEntityService.CreateSiteFAQ(siteFAQ);
                await _subEntityService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siteFAQ);
        }

        // GET: Admin/SiteFAQs/Edit/5
        [PermissionChecker(124)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteFAQ = await _subEntityService.GetSiteFAQById((int)id);
            if (siteFAQ == null)
            {
                return NotFound();
            }
            return View(siteFAQ);
        }

        // POST: Admin/SiteFAQs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(124)]
        public async Task<IActionResult> Edit(int id, SiteFAQ siteFAQ)
        {
            if (id != siteFAQ.SiteFAQ_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    siteFAQ.OP_Update += "-" + User.Identity.Name + " | " + DateTime.Now;
                    _subEntityService.UpdateSiteFAQ(siteFAQ);
                    await _subEntityService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteFAQExists(siteFAQ.SiteFAQ_Id))
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
            return View(siteFAQ);
        }

        // GET: Admin/SiteFAQs/Delete/5
        [PermissionChecker(126)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteFAQ = await _subEntityService.GetSiteFAQById((int)id);
            if (siteFAQ == null)
            {
                return NotFound();
            }

            return View(siteFAQ);
        }

        // POST: Admin/SiteFAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(126)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siteFAQ = await _subEntityService.GetSiteFAQById(id);
            siteFAQ.OP_Remove += "-" + User.Identity.Name + " | " + DateTime.Now;
            _subEntityService.UpdateSiteFAQ(siteFAQ);
            await _subEntityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteFAQExists(int id)
        {
            return _subEntityService.ExistSiteFAQ(id);
        }
    }
}
