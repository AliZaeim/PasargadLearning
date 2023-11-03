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
    public class PackInfoesController : Controller
    {

        private readonly ISubEntityService _subEntityService;

        public PackInfoesController(ISubEntityService subEntityService)
        {
            _subEntityService = subEntityService;

        }

        // GET: Admin/PackInfoes
        [PermissionChecker(130)]
        public async Task<IActionResult> Index()
        {
            return View(await _subEntityService.GetPackInfos());
        }

        // GET: Admin/PackInfoes/Details/5
        [PermissionChecker(130)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packInfo = await _subEntityService.GetPackInfoById((int)id);
            if (packInfo == null)
            {
                return NotFound();
            }

            return View(packInfo);
        }

        // GET: Admin/PackInfoes/Create
        [PermissionChecker(131)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PackInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(131)]
        public async Task<IActionResult> Create(PackInfo packInfo)
        {
            if (ModelState.IsValid)
            {

                _subEntityService.CreatePackInfo(packInfo);
                await _subEntityService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(packInfo);
        }

        // GET: Admin/PackInfoes/Edit/5
        [PermissionChecker(132)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packInfo = await _subEntityService.GetPackInfoById((int)id);
            if (packInfo == null)
            {
                return NotFound();
            }
            return View(packInfo);
        }

        // POST: Admin/PackInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(132)]
        public async Task<IActionResult> Edit(int id, PackInfo packInfo)
        {
            if (id != packInfo.PackInfo_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _subEntityService.UpdatePackInfo(packInfo);
                    await _subEntityService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackInfoExists(packInfo.PackInfo_Id))
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
            return View(packInfo);
        }

        // GET: Admin/PackInfoes/Delete/5
        [PermissionChecker(133)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packInfo = await _subEntityService.GetPackInfoById((int)id);
            if (packInfo == null)
            {
                return NotFound();
            }

            return View(packInfo);
        }

        // POST: Admin/PackInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(133)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subEntityService.RemovePackInfo(id);
            _subEntityService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PackInfoExists(int id)
        {
            return _subEntityService.ExistPackInfo(id);
        }
    }
}
