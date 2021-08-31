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

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _context;


        public HomeController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        #endregion


        public IActionResult Index()
        {
            if (_context.AdminWalletRepository.IsExistAdminWallet())
            {
                ViewBag.IsExist = true;
                AdminWallet wallet = _context.AdminWalletRepository.GetAdminWallet();
                ViewBag.AdminPercent = wallet.WalletAmount;
                ViewBag.DebtAmount = wallet.DebtAmount;
                ViewBag.Credit = wallet.CreditAmount;
                ViewBag.AllBalance = (wallet.DebtAmount) + (wallet.WalletAmount);
            }


            return View();
        }
    }
}
