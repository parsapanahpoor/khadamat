using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class SelectJobForUserController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public SelectJobForUserController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult JobsList()
        {
            return View(_context.jobCategoryRepository.GetAllJobsCategories());
        }

        public async Task<IActionResult> ListOfEmployeeJobs(bool Add = false)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return View("~/Views/Shared/_404.cshtml");

            }

            var UserJobs = _context.userSelectedJobRepository.GetUserSelectedJobByUserid(user.Id);

            if (Add == true)
            {
                ViewBag.Add = true;
            }

            return View(UserJobs);
        }


        public async Task<IActionResult> EmployeeResume(int Jobid)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.job = _context.jobCategoryRepository.GetJobCatgeoriesById(Jobid);

            return View(new UserSelectedJob()
            { 
            
                JobCategoryId = Jobid ,
                Userid = user.Id

            
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeResume(UserSelectedJob userSelectedJob , IFormFile UserAvatarFile)
        {
            if (ModelState.IsValid)
            {
                _context.userSelectedJobRepository.AddJobToUser(userSelectedJob , UserAvatarFile);
                _context.SaveChangesDB();

                return Redirect("/Employee/SelectJobForUser/ListOfEmployeeJobs?Add=True");
            }

            ViewBag.job = _context.jobCategoryRepository.GetJobCatgeoriesById(userSelectedJob.JobCategoryId);

            return View(new UserSelectedJob()
            {

                JobCategoryId = userSelectedJob.JobCategoryId,
                Userid = userSelectedJob.Userid


            });
        }

        
    }
}
