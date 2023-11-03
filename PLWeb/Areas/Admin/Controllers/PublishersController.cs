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
using PLDataLayer.Entities.Blog;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PublishersController : Controller
    {
        
        private readonly IBlogService _blogService;

        public PublishersController(IBlogService blogService)
        {
            _blogService = blogService;
            
        }

        // GET: Admin/Publishers
        [PermissionChecker(77)]
        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetPublishersAsync());
        }

        // GET: Admin/Publishers/Details/5
        [PermissionChecker(77)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _blogService.GetPublisherByIdAsync((int)id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Admin/Publishers/Create
        [PermissionChecker(78)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(78)]
        public async Task<IActionResult> Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _blogService.CreatePublisher(publisher);
                await _blogService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Admin/Publishers/Edit/5
        [PermissionChecker(79)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _blogService.GetPublisherByIdAsync((int)id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: Admin/Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(79)]
        public async Task<IActionResult> Edit(int id, Publisher publisher)
        {
            if (id != publisher.Publisher_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogService.UpdatePublisher(publisher);
                    await _blogService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.Publisher_Id))
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
            return View(publisher);
        }

        // GET: Admin/Publishers/Delete/5
        [PermissionChecker(80)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _blogService.GetPublisherByIdAsync((int)id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Admin/Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(80)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _blogService.GetPublisherByIdAsync(id);
            publisher.IsDeleted = true;
            _blogService.UpdatePublisher(publisher);
            await _blogService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return _blogService.ExistsPublisher(id);
        }
    }
}
