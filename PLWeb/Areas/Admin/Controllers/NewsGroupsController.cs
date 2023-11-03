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
    public class NewsGroupsController : Controller
    {

        private readonly IBlogService _blogService;
        public NewsGroupsController(IBlogService blogService)
        {
            _blogService = blogService;

        }

        // GET: Admin/NewsGroups
        [PermissionChecker(73)]
        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetNewsGroupsAsync());
        }



        // GET: Admin/NewsGroups/Create
        [PermissionChecker(74)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewsGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(74)]
        public async Task<IActionResult> Create(NewsGroup newsGroup)
        {
            if (!ModelState.IsValid)
            {
                return View(newsGroup);
            }
            _blogService.CreateNewsGroup(newsGroup);
            await _blogService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/NewsGroups/Edit/5
        [PermissionChecker(75)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsGroup = await _blogService.GetNewsGroupByIdAsync((int)id);
            if (newsGroup == null)
            {
                return NotFound();
            }
            return View(newsGroup);
        }

        // POST: Admin/NewsGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(75)]
        public async Task<IActionResult> Edit(int id, NewsGroup newsGroup)
        {
            if (id != newsGroup.NewsGroup_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogService.UpdateNewsGroup(newsGroup);
                    await _blogService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsGroupExists(newsGroup.NewsGroup_Id))
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
            return View(newsGroup);
        }

        // GET: Admin/NewsGroups/Delete/5
        [PermissionChecker(76)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsGroup = await _blogService.GetNewsGroupByIdAsync((int)id);
            if (newsGroup == null)
            {
                return NotFound();
            }

            return View(newsGroup);
        }

        // POST: Admin/NewsGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(76)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _blogService.RemoveNewsGroup(id);
            await _blogService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsGroupExists(int id)
        {
            return _blogService.ExistNewsGroup(id);
        }
    }
}
