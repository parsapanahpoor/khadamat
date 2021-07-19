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
    public class UserInformationController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;


        public UserInformationController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        #endregion

        public async Task<IActionResult> EmployeeDocument()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
           
            var model = _context.employeeRepository.GetEmployeeDocument(user.Id);
            
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeDocument(EmployeeDocuments  employee , IFormFile Picture ,IFormFile Certificate)
        {
            if (ModelState.IsValid)
            {
                //if (employee.EmployeeCertificate == null)
                //{
                //    ModelState.AddModelError("", "تصویر اجاره نامه ی خود را وارد کنید  ");
                //    return View(employee);
                //}
                //if (employee.PersonalPicture == null)
                //{
                //    ModelState.AddModelError("", "تصویر 3*4 خود را وارد کنید  ");
                //    return View(employee);
                //}
                if (employee.BankAccountNumber == null)
                {
                    ModelState.AddModelError("", "شماره حساب خود را وارد کنید  ");
                    return View(employee);
                }
                if (employee.HomePhoneNumber == null)
                {
                    ModelState.AddModelError("", "شماره تلفن ثابت خود را وارد کنید  ");
                    return View(employee);
                }

                var user = await _userManager.FindByIdAsync(employee.Userid);
                user.IsAccepted = false;
                var task = await  _userManager.UpdateAsync(user);


                employee.PossitionId = 2;
                _context.employeeRepository.UpdateEmployeeDocumentFromEmployeePanel(employee , Picture , Certificate);
                


                _context.SaveChangesDB();
                return Redirect("/Employee/Home/Index");
            }
            return View(employee);
        }
    }
}
