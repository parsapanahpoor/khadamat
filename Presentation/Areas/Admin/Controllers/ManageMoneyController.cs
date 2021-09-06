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
    public class ManageMoneyController : Controller
    {
        #region Constructor

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _context;

        public ManageMoneyController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        #endregion

        public IActionResult ListOfCheckoutRequests(bool Update = false)
        {
            List<RequestForCheckout> List = _context.RequestForCheckoutRepository.GetAllRequestsForCheckout();

            if (Update == true)
            {
                ViewBag.Update = true;
            }

            return View(List);
        }

        public IActionResult ListOfNewRequests()
        {
            List<RequestForCheckout> List = _context.RequestForCheckoutRepository.GetAllNewRequests();
            return View(List);
        }
        public IActionResult ManageRequest(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (_context.RequestForCheckoutRepository.GetRequestForCheckoutStatusID((int)id) == 1)
            {
                RequestForCheckout request = _context.RequestForCheckoutRepository.GetRequestForCheckoutbyID((int)id);

                return View(request);
            }

            return View("~/Views/Shared/_404.cshtml");
        }

        public IActionResult SubmitRequest(int? id, int? Result)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (id == Result)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            RequestForCheckout request = _context.RequestForCheckoutRepository.GetRequestForCheckoutbyID((int)id);

            //مدیریت مالی و تسویه حساب با خدمت رسان 
            if (Result == 2)
            {
                request.RequestForCheckoutStatusID = 2;
                _context.FinancialTransactionRepository.CheckoutWhitEmployeeAfterHisRequest(request.Price, request.EmployeeID);

                if (_context.AdminWalletRepository.IsExistAdminWallet())
                {
                    _context.AdminWalletRepository.CheckoutWhitEmployeeAfterHisRequest(request.Price);
                }
                if (!_context.AdminWalletRepository.IsExistAdminWallet())
                {
                    _context.AdminWalletRepository.AddAdminWallet();
                    _context.SaveChangesDB();
                    _context.AdminWalletRepository.CheckoutWhitEmployeeAfterHisRequest(request.Price);
                }

                if (_context.EmployeeWalletRepository.IsExistEmployeeWallet(request.EmployeeID))
                {
                    _context.EmployeeWalletRepository.CheckOutWhitEmployeeAfterHisRequest(request.EmployeeID, request.Price);
                }
                else
                {
                    return View("~/Views/Shared/_404.cshtml");
                }
            }

            if (Result == 3)
            {
                request.RequestForCheckoutStatusID = 3;
            }
            _context.RequestForCheckoutRepository.UpdateRequestForCheckout(request);
            _context.SaveChangesDB();

            return Redirect("/Admin/ManageMoney/ListOfCheckoutRequests?Update=true");
        }

        public IActionResult ListOfAllFinancialTransaction()
        {
            List<FinancialTrnsaction> List = _context.FinancialTransactionRepository.GeAllFinancialTrnsactions();

            return View(List);
        }
    }
}
