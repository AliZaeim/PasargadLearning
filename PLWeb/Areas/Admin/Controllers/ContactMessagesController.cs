using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using PLDataLayer.Entities.SubEntities;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ContactMessagesController : Controller
    {
       
        private readonly ISubEntityService _subEntityService;
        public ContactMessagesController(ISubEntityService subEntityService)
        {
            _subEntityService = subEntityService;
            
        }

        // GET: Admin/ContactMessages
        public async Task<IActionResult> Index()
        {
            return View(await _subEntityService.GetContactMessagesAsync());
        }

        // GET: Admin/ContactMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactMessage = await _subEntityService.GetContactByIdAsync((int)id);
            if (contactMessage == null)
            {
                return NotFound();
            }

            return View(contactMessage);
        }

        // GET: Admin/ContactMessages/Create
        

        // GET: Admin/ContactMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactMessage = await _subEntityService.GetContactByIdAsync((int)id);
            if (contactMessage == null)
            {
                return NotFound();
            }
            return View(contactMessage);
        }

        // POST: Admin/ContactMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int CM_Id,  ContactMessage contactMessage)
        {
            if (CM_Id != contactMessage.CM_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _subEntityService.UpdateMessage(contactMessage);
                    await _subEntityService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        return NotFound();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contactMessage);
        }

        // GET: Admin/ContactMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactMessage = await _subEntityService.GetContactByIdAsync((int)id);
            if (contactMessage == null)
            {
                return NotFound();
            }

            return View(contactMessage);
        }

        

        
    }
}
