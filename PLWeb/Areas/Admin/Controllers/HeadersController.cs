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
    public class HeadersController : Controller
    {
       
        private readonly ISubEntityService _subEntityService;
        public HeadersController(ISubEntityService subEntityService)
        {
            _subEntityService = subEntityService;
          
        }

        // GET: Admin/Headers
        [PermissionChecker(113)]
        public async Task<IActionResult> Index()
        {
            return View(await _subEntityService.GetHeadersAsync());
        }

        // GET: Admin/Headers/Details/5
        [PermissionChecker(116)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var header = await _subEntityService.GetHeaderByIdAsync((int)id);
            if (header == null)
            {
                return NotFound();
            }

            return View(header);
        }

        // GET: Admin/Headers/Create
        [PermissionChecker(114)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Headers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(114)]
        public async Task<IActionResult> Create( Header header)
        {
            if (ModelState.IsValid)
            {
                header.OP_Create = "-" + User.Identity.Name + " | " + DateTime.Now;
                _subEntityService.CreateHeader(header);
                await _subEntityService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(header);
        }

        // GET: Admin/Headers/Edit/5
        [PermissionChecker(115)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var header = await _subEntityService.GetHeaderByIdAsync((int)id);
            if (header == null)
            {
                return NotFound();
            }
            return View(header);
        }

        // POST: Admin/Headers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(115)]
        public async Task<IActionResult> Edit(int id, Header header)
        {
            if (id != header.Header_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    header.OP_Update = "-" + User.Identity.Name + " | " + DateTime.Now;
                    _subEntityService.UpdateHeader(header);
                    await _subEntityService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeaderExists(header.Header_id))
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
            return View(header);
        }

        // GET: Admin/Headers/Delete/5
        [PermissionChecker(117)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var header = await _subEntityService.GetHeaderByIdAsync((int)id);
            if (header == null)
            {
                return NotFound();
            }

            return View(header);
        }

        // POST: Admin/Headers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(117)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var header = await _subEntityService.GetHeaderByIdAsync(id);
            header.IsDeleted = true;
            header.OP_Remove = "-" + User.Identity.Name + " | " + DateTime.Now;
            _subEntityService.UpdateHeader(header);
            await _subEntityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeaderExists(int id)
        {
            return _subEntityService.ExistHeader(id);
        }
    }
}
