using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
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
    public class EmployeeReservationController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;


        public EmployeeReservationController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        #endregion

        public async Task<IActionResult> ChangeStatusByIconInPartial(int id , string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (id == 1)
            {
                user.EmployeeStatusID = 1;

            }
            if (id == 2)
            {
                user.EmployeeStatusID = 2;
            }
            if (id == 3)
            {
                user.EmployeeStatusID = 3;
            }

            var result = await _userManager.UpdateAsync(user);
            _context.SaveChangesDB();

            if (id == 1)
            {
                return Redirect("/Employee/Home/Index?Status=1");
            }
            if (id == 2)
            {
                return Redirect("/Employee/Home/Index?Status=2");
            }
            if (id == 3)
            {
                return Redirect("/Employee/Home/Index?Status=3");
            }

            return View();
        }



    }
}
