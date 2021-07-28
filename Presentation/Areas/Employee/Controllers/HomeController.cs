using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Convertors;

namespace Presentation.Areas.Supporter.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;


        public HomeController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        #endregion


        public async Task<IActionResult> Index(int? status , bool Edit = false  )
        {
            var user =await _userManager.FindByNameAsync(User.Identity.Name);
            var Employee = _context.employeeRepository.GetEmployeeDocument(user.Id);

            if (!_context.userSelectedJobRepository.IsExistUserSelectedJob(user.Id))
            {
                ViewBag.UserSelectedJob = true;
            }

            ViewBag.Possition = Employee.PossitionId;

            if (Edit == true)
            {
                ViewBag.Edit = true;
            }

            if (status == 1)
            {
                ViewBag.Status = 1;
            }
            if (status == 2)
            {
                ViewBag.Status = 2;

            }
            if (status == 3)
            {
                ViewBag.Status = 3;

            }

            return View(user);
        }
    }
}
