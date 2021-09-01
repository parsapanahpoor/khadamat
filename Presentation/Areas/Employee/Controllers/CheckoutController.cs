using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Factor;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class CheckoutController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;


        public CheckoutController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        #endregion

        public async Task<IActionResult> ListOfCheckouts(bool Create = false)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!_context.EmployeeWalletRepository.IsExistEmployeeWallet(user.Id))
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            List<RequestForCheckout> List = _context.RequestForCheckoutRepository.GetAllEmployeeRequestForCheckout(user.Id);
            if (Create == true)
            {
                ViewBag.Create = true;
            }
            return View(List);
        }

        public async Task<IActionResult> CreateCheckout()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.EmployeeID = user.Id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCheckout(RequestForCheckout request)
        {            
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                EmployeeWallet employeeWallet = _context.EmployeeWalletRepository.GetEmployeeWalletByEmployeeID(user.Id);
                if (request.Price > employeeWallet.CreditAmount)
                {
                    ModelState.AddModelError("Price", "مبلغ درخواستی شما بیشتر از موجودی شماست");
                    return View(request);
                }
                _context.RequestForCheckoutRepository.AddRequestForCheckout(request );
                _context.SaveChangesDB();

                return Redirect("/Employee/Checkout/ListOfCheckouts?Create=true");
            }

            return View(request);
        }
    }
}
