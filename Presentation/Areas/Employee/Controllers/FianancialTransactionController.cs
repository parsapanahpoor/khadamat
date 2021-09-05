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
    public class FianancialTransactionController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;


        public FianancialTransactionController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        #endregion

        public async Task<IActionResult> ListOFEmployeeFianancialTransaction()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<FinancialTrnsaction> financials = _context.FinancialTransactionRepository.GetAllEmployeeFinancialTransaction(user.Id);

            return View(financials);
        }
    }
}
