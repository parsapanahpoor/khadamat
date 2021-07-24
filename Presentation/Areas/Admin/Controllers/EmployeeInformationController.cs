using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EmployeeInformationController : Controller
    {
        #region Constructor

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _context;


        public EmployeeInformationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        #endregion

        public IActionResult EmployeeInformation(string id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            var info = _context.employeeRepository.GetEmployeeDocument(id);
            if (info == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ViewBag.Id = info.Id;


            return View(info);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeInformation(EmployeeDocuments employee, bool PossId)
        {

            if (PossId != true && PossId != false)
            {
                ModelState.AddModelError("", "لطفا نتیجه ی برسی خود را وارد کنید    ");
                return View(employee);
            }

            if (ModelState.IsValid)
            {
    
                if (PossId == true)
                {
                    employee.PossitionId = 4;

                    var user = await _userManager.FindByIdAsync(employee.Userid);
                    user.IsAccepted = true;
                    var task = await _userManager.UpdateAsync(user);
                }
                if (PossId == false)
                {
                    employee.PossitionId = 3;

                    var user = await _userManager.FindByIdAsync(employee.Userid);
                    user.IsAccepted = false;
                    var task = await _userManager.UpdateAsync(user);

                }
                _context.employeeRepository.UpdateEmployeeInfoFromAdminPanel(employee);

                _context.SaveChangesDB();
                return Redirect("/Admin/Users/EmployeeList?Update=true");
            }

            ViewBag.Id = employee.Id;
            ModelState.AddModelError("", "مشکلی در درج اطلاعات رخ داده است لطفا صحت فیلد هارا برسی کنید     ");
            return View(employee);
        }

    }
}
