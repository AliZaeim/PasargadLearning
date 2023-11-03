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
    public class ContactInfoesController : Controller
    {

        private readonly ISubEntityService _subEntityService;
        public ContactInfoesController(ISubEntityService subEntityService)
        {
            _subEntityService = subEntityService;

        }

        // GET: Admin/ContactInfoes
        [PermissionChecker(108)]
        public async Task<IActionResult> Index()
        {
            return View(await _subEntityService.GetContactInfosAsync());
        }

        // GET: Admin/ContactInfoes/Details/5
        [PermissionChecker(111)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactInfo = await _subEntityService.GetContactInfoById((int)id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return View(contactInfo);
        }

        // GET: Admin/ContactInfoes/Create
        [PermissionChecker(109)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ContactInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(109)]
        public async Task<IActionResult> Create(ContactInfo contactInfo)
        {
            if (ModelState.IsValid)
            {
                contactInfo.OP_Create = User.Identity.Name + " | " + DateTime.Now;
                _subEntityService.CreateContactInfo(contactInfo);
                await _subEntityService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactInfo);
        }

        // GET: Admin/ContactInfoes/Edit/5
        [PermissionChecker(110)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactInfo = await _subEntityService.GetContactInfoById((int)id);
            if (contactInfo == null)
            {
                return NotFound();
            }
            return View(contactInfo);
        }

        // POST: Admin/ContactInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(110)]
        public async Task<IActionResult> Edit(int id, ContactInfo contactInfo)
        {
            if (id != contactInfo.CI_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contactInfo.OP_Update = "-" + User.Identity.Name + " | " + DateTime.Now;
                    _subEntityService.UpdateContactInfo(contactInfo);
                    await _subEntityService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactInfoExists(contactInfo.CI_Id))
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
            return View(contactInfo);
        }

        // GET: Admin/ContactInfoes/Delete/5
        [PermissionChecker(112)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactInfo = await _subEntityService.GetContactInfoById((int)id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return View(contactInfo);
        }

        // POST: Admin/ContactInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(112)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactInfo = await _subEntityService.GetContactInfoById(id);
            contactInfo.OP_Remove = "-" + User.Identity.Name + " | " + DateTime.Now;
            _subEntityService.UpdateContactInfo(contactInfo);
            await _subEntityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactInfoExists(int id)
        {
            return _subEntityService.ExistContactInfo(id);
        }
    }
}
