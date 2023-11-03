using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PLCore.Generators;
using PLCore.Security;
using PLCore.Services.Interfaces;
using PLDataLayer.Entities.Training;

namespace PLWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CourseGroupsController : Controller
    {
        
        private readonly ITrainingService _TrainingService;
        public CourseGroupsController(ITrainingService TrainingService)
        {
            _TrainingService = TrainingService;
           
        }

        // GET: Admin/CourseGroups
        [PermissionChecker(60)]
        public async Task<IActionResult> Index()
        {
            return View(await _TrainingService.GetCourseGroupsAsync());
        }

        // GET: Admin/CourseGroups/Details/5
        [PermissionChecker(60)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseGroup = await _TrainingService.GetCourseGroupByIdAsync((int)id);
            if (courseGroup == null)
            {
                return NotFound();
            }

            return View(courseGroup);
        }

        // GET: Admin/CourseGroups/Create
        [PermissionChecker(61)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CourseGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(61)]
        public async Task<IActionResult> Create(CourseGroup courseGroup, IFormFile CourseGroup_Image)
        {
            if (!ModelState.IsValid)
            {
                return View(courseGroup);
            }
            if (CourseGroup_Image == null)
            {
                ModelState.AddModelError("CourseGroup_Image", "لطفا تصویر را انتخاب کنید !");
                return View(courseGroup);
            }
            if (CourseGroup_Image != null)
            {
                if (CourseGroup_Image.Length > .05 * 1025 * 1024)
                {
                    ModelState.AddModelError("CourseGroup_Image", "حجم عکس از 50 کیلو بایت بیشتر است");
                    return View(courseGroup);
                }
            }
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/coursegroup", CourseGroup_Image.FileName);
            string imgName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(CourseGroup_Image.FileName);
            courseGroup.CourseGroup_Image = imgName;
            await _TrainingService.CreateCourseGroupAsync(courseGroup);
            await _TrainingService.SaveAsync();
            imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/coursegroup", imgName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                CourseGroup_Image.CopyTo(stream);
            }
            return RedirectToAction(nameof(Index));


        }
        [PermissionChecker(61)]
        public async Task<IActionResult> CreateSub()
        {
            List<CourseGroup> courseGroups = await _TrainingService.GetCourseGroupsAsync();
            ViewBag.ParentId = new SelectList(courseGroups, "CourseGroup_Id", "CourseGroup_Title");
            return View();
        }

        // POST: Admin/CourseGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(61)]
        public async Task<IActionResult> CreateSub(CourseGroup courseGroup)
        {
            if (!ModelState.IsValid)
            {
                List<CourseGroup> courseGroups = await _TrainingService.GetCourseGroupsAsync();
                ViewBag.ParentId = new SelectList(courseGroups, "CourseGroup_Id", "CourseGroup_Title", courseGroup.ParentId);
                return View(courseGroup);
            }
            await _TrainingService.CreateCourseGroupAsync(courseGroup);
            await _TrainingService.SaveAsync();
            return RedirectToAction(nameof(Index));


        }

        // GET: Admin/CourseGroups/Edit/5
        [PermissionChecker(62)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseGroup = await _TrainingService.GetCourseGroupByIdAsync((int)id);
            if (courseGroup == null)
            {
                return NotFound();
            }
            return View(courseGroup);
        }

        // POST: Admin/CourseGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(62)]
        public async Task<IActionResult> Edit(int id, CourseGroup courseGroup, IFormFile CourseGroup_Image)
        {
            if (id != courseGroup.CourseGroup_Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (CourseGroup_Image != null)
                {
                    if (CourseGroup_Image.Length > .05 * 1025 * 1024)
                    {
                        ModelState.AddModelError("CourseGroup_Image", "حجم عکس از 50 کیلو بایت بیشتر است");
                        return View(courseGroup);
                    }
                    if (!string.IsNullOrEmpty(courseGroup.CourseGroup_Image))
                    {
                        string beforeimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/coursegroup", CourseGroup_Image.FileName);
                        if (System.IO.File.Exists(beforeimagePath))
                        {
                            System.IO.File.Delete(beforeimagePath);
                        }
                    }

                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/coursegroup", CourseGroup_Image.FileName);
                    string imgName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(CourseGroup_Image.FileName);
                    courseGroup.CourseGroup_Image = imgName;
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/coursegroup", imgName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        CourseGroup_Image.CopyTo(stream);
                    }
                }

                _TrainingService.UpdateCourseGroup(courseGroup);
                await _TrainingService.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseGroupExists(courseGroup.CourseGroup_Id))
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
        [PermissionChecker(62)]
        public async Task<IActionResult> EditSub(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseGroup = await _TrainingService.GetCourseGroupByIdAsync((int)id);
            if (courseGroup == null)
            {
                return NotFound();
            }
            List<CourseGroup> courseGroups = await _TrainingService.GetCourseGroupsAsync();
            ViewBag.ParentId = new SelectList(courseGroups, "CourseGroup_Id", "CourseGroup_Title", courseGroup.ParentId);
            return View(courseGroup);
        }

        // POST: Admin/CourseGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(62)]
        public async Task<IActionResult> EditSub(int id, CourseGroup courseGroup, IFormFile CourseGroup_Image)
        {
            if (id != courseGroup.CourseGroup_Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                List<CourseGroup> courseGroups = await _TrainingService.GetCourseGroupsAsync();
                ViewBag.ParentId = new SelectList(courseGroups, "CourseGroup_Id", "CourseGroup_Title", courseGroup.ParentId);
                return View();
            }
            try
            {
                if (CourseGroup_Image != null)
                {
                    if (CourseGroup_Image.Length > .05 * 1025 * 1024)
                    {
                        ModelState.AddModelError("CourseGroup_Image", "حجم عکس از 50 کیلو بایت بیشتر است");
                        List<CourseGroup> courseGroups = await _TrainingService.GetCourseGroupsAsync();
                        ViewBag.ParentId = new SelectList(courseGroups, "CourseGroup_Id", "CourseGroup_Title", courseGroup.ParentId);
                        return View(courseGroup);
                    }
                    if (!string.IsNullOrEmpty(courseGroup.CourseGroup_Image))
                    {
                        string beforeimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/coursegroup", CourseGroup_Image.FileName);
                        if (System.IO.File.Exists(beforeimagePath))
                        {
                            System.IO.File.Delete(beforeimagePath);
                        }
                    }

                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/coursegroup", CourseGroup_Image.FileName);
                    string imgName = GeneratorClass.GenerateUniqueCode() + Path.GetExtension(CourseGroup_Image.FileName);
                    courseGroup.CourseGroup_Image = imgName;
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/coursegroup", imgName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        CourseGroup_Image.CopyTo(stream);
                    }
                }
                _TrainingService.UpdateCourseGroup(courseGroup);
                await _TrainingService.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseGroupExists(courseGroup.CourseGroup_Id))
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
        // GET: Admin/CourseGroups/Delete/5
        [PermissionChecker(63)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseGroup = await _TrainingService.GetCourseGroupByIdAsync((int)id);
            if (courseGroup == null)
            {
                return NotFound();
            }
            if (courseGroup.ParentId == null)
            {
                ViewBag.type = "گروه";
            }
            else
            {
                ViewBag.type = "زیرگروه";
            }
            return View(courseGroup);
        }

        // POST: Admin/CourseGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(63)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseGroup = await _TrainingService.GetCourseGroupByIdAsync(id);
            courseGroup.IsDeleted = true;
             _TrainingService.UpdateCourseGroup(courseGroup);
            await _TrainingService.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseGroupExists(int id)
        {
            return _TrainingService.ExistCourseGroup(id);
        }
    }
}
