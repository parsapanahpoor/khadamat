using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class JobCategoriesController : Controller
    {
        private readonly IUnitOfWork _context;
        public JobCategoriesController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;

            return View(_context.jobCategoryRepository.GetAllJobsCategories());
        }

        public IActionResult Create(int? id , bool Level3 = false)
        {
            if (Level3 == true)
            {
                ViewBag.Level3 = true;
            }

            return View(new JobCategory()
            {
                ParentId = id
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobCategory jobCategory, IFormFile JobPic )
        {
            if (ModelState.IsValid)
            {
                _context.jobCategoryRepository.AddJobCategory(jobCategory, JobPic);
                _context.SaveChangesDB();

                return Redirect("/Admin/JobCategories/Index?Create=true");
            }
            return View(jobCategory);
        }

        public IActionResult Edit(int? id, bool Delete = false , bool Level3 = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategories = _context.jobCategoryRepository.GetJobCatgeoriesById((int)id);
            if (productCategories == null)
            {
                return NotFound();
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }
            if (Level3 == true)
            {
                ViewBag.Level3 = true;
            }

            return View(productCategories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JobCategory jobCategory, IFormFile JobPic)
        {

            if (ModelState.IsValid)
            {

                _context.jobCategoryRepository.UpdateJobCaategory(jobCategory, JobPic);
                _context.SaveChangesDB();

                return Redirect("/Admin/JobCategories/Index?Edit=true");
            }
            return View(jobCategory);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");

            }
            var category = _context.jobCategoryRepository.GetJobCatgeoriesById((int)id);
            if (category == null)
            {
                return View("~/Views/Shared/_404.cshtml");

            }

            _context.jobCategoryRepository.DeleteJobCategory(category);
            _context.SaveChangesDB();

            return Redirect("/Admin/JobCategories/Index?Delete=true");
        }
    }
}
