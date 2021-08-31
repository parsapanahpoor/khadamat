using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.EmployeeReservation;
using Models.Entities.Factor;
using Models.Entities.User;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InvoicingController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _context;


        public InvoicingController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        #endregion

        public async Task<IActionResult> CheckInvoicingFromAdmin(int? HoureID)
        {
            if (HoureID == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            Invoicing invoicing = _context.invoicingRepository.GetInvoicingByHourID((int)HoureID);
            HourReservation houre = _context.hourReservationRepository.GetHoureReservationByID((int)invoicing.HoureReservationID);
            if (invoicing == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (houre == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ViewBag.HoureInfo = houre;
            ViewBag.InvoicingDetail = _context.invoicingDetailsRepository.GetListOfInvoicingDetailByInvoicingId(invoicing.InvoicingID);
            ViewBag.Employee = await _userManager.FindByIdAsync(invoicing.EmployeeID);

            return View(invoicing);
        }

        public async Task<IActionResult> SubmitInvoicingFromAdmin(int? HoureID)
        {
            if (HoureID == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            Invoicing invoicing = _context.invoicingRepository.GetInvoicingByHourID((int)HoureID);
            HourReservation houre = _context.hourReservationRepository.GetHoureReservationByID((int)invoicing.HoureReservationID);
            if (invoicing == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (houre == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ViewBag.HoureInfo = houre;
            ViewBag.InvoicingDetail = _context.invoicingDetailsRepository.GetListOfInvoicingDetailByInvoicingId(invoicing.InvoicingID);
            ViewBag.Employee = await _userManager.FindByIdAsync(invoicing.EmployeeID);

            return View(invoicing);
        }
        public IActionResult SubmitInvoicing(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            Invoicing invoicing = _context.invoicingRepository.GetInvoicingByID((int)id);
            if (invoicing == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            _context.invoicingRepository.SubmitInvoicingFromAdminPanel(invoicing);

            decimal AllPrice = _context.invoicingDetailsRepository.GetFullPriceFromInvoicing(invoicing.InvoicingID);
            decimal EmployeePercent = _context.invoicingDetailsRepository.GetEmployeePercentFromInvoicing(invoicing.InvoicingID);
            decimal AdminPercent = _context.invoicingDetailsRepository.GetAdminPercerntFromInvoicing(invoicing.InvoicingID);

            //محاسبه ی عملیات مالی 
            #region FinancialTransaction
            //پرداخت نقدی مبلغ 
            if (invoicing.PaymentMethodID == 1)
            {
                _context.FinancialTransactionRepository.AddFinancialTransactionForCashPaymentToEmployeeFromUser(invoicing, AllPrice);

                //پرداخت سهم شرکت 
                if (_context.AdminWalletRepository.IsExistAdminWallet())
                {
                    _context.AdminWalletRepository.UpdateAdminWalletForCashPaymentTotheEmployeeFromUser(AdminPercent);
                }
                if (!_context.AdminWalletRepository.IsExistAdminWallet())
                {
                    _context.AdminWalletRepository.AddAdminWallet();
                    _context.SaveChangesDB();
                    _context.AdminWalletRepository.UpdateAdminWalletForCashPaymentTotheEmployeeFromUser(AdminPercent);
                }

                //پرداخت سهم خدمت رسان  
                if (_context.EmployeeWalletRepository.IsExistEmployeeWallet(invoicing.EmployeeID))
                {
                    _context.EmployeeWalletRepository.UpdateEmployeeWalletForCashPaymentFromUser(invoicing.EmployeeID, AdminPercent);
                }
                if (!_context.EmployeeWalletRepository.IsExistEmployeeWallet(invoicing.EmployeeID))
                {
                    _context.EmployeeWalletRepository.AddEmployeeWallet(invoicing.EmployeeID);
                    _context.SaveChangesDB();
                    _context.EmployeeWalletRepository.UpdateEmployeeWalletForCashPaymentFromUser(invoicing.EmployeeID, AdminPercent);
                }
            }
            if (invoicing.PaymentMethodID == 2)
            {
                _context.FinancialTransactionRepository.AddFinancialTransactionForOnlinePaymentToEmployeeFromUser(invoicing, AllPrice);
                //پرداخت سهم شرکت 
                if (_context.AdminWalletRepository.IsExistAdminWallet())
                {
                    _context.AdminWalletRepository.UpdateAdminWalletForOnlinePaymentTotheEmployeeFromUser(AdminPercent,EmployeePercent);
                }
                if (!_context.AdminWalletRepository.IsExistAdminWallet())
                {
                    _context.AdminWalletRepository.AddAdminWallet();
                    _context.SaveChangesDB();
                    _context.AdminWalletRepository.UpdateAdminWalletForOnlinePaymentTotheEmployeeFromUser(AdminPercent, EmployeePercent);
                }

                //پرداخت سهم خدمت رسان  
                if (_context.EmployeeWalletRepository.IsExistEmployeeWallet(invoicing.EmployeeID))
                {
                    _context.EmployeeWalletRepository.UpdateEmployeeWalletForOnlinePaymentFromUser(invoicing.EmployeeID, EmployeePercent);
                }
                if (!_context.EmployeeWalletRepository.IsExistEmployeeWallet(invoicing.EmployeeID))
                {
                    _context.EmployeeWalletRepository.AddEmployeeWallet(invoicing.EmployeeID);
                    _context.SaveChangesDB();
                    _context.EmployeeWalletRepository.UpdateEmployeeWalletForOnlinePaymentFromUser(invoicing.EmployeeID, EmployeePercent);
                }
            }
            #endregion
            _context.SaveChangesDB();

            return Redirect("/Admin/ReservationOrder/ListOfDateReservation?id=" + invoicing.DateReservationID);
        }
        public IActionResult AddInvoicingDetails(int InvoicingId, int? TariffID, decimal Price, string Description = "")
        {
            if (ModelState.IsValid)
            {
                Invoicing invoicing1 = _context.invoicingRepository.GetInvoicingByID(InvoicingId);

                if (TariffID != 0)
                {
                    Tariff tariff = _context.tariffRepository.GetTariffById((int)TariffID);
                    _context.invoicingDetailsRepository.AddInvoicingDetailFromEmployeePanelByPercent(InvoicingId, (int)TariffID, (int)tariff.TariffPercent, Price, Description);
                }
                else
                {
                    _context.invoicingDetailsRepository.AddInvoicingDetailFromEmployeePanel(InvoicingId, Price, Description);
                }
                _context.SaveChangesDB();

                return Redirect("/Admin/Invoicing/SubmitInvoicingFromAdmin?HoureID=" + invoicing1.HoureReservationID);
            }

            return View("~/Views/Shared/_404.cshtml");
        }
        public IActionResult DeleteInvoicingDetail(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            var invoicing = _context.invoicingDetailsRepository.GetInvoicingDetailByID((int)id);
            Invoicing invoicing1 = _context.invoicingRepository.GetInvoicingByID(invoicing.InvoicingID);
            if (invoicing == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            _context.invoicingDetailsRepository.DeleteInvoicingDetailSoftDelete(invoicing);
            _context.SaveChangesDB();

            return Redirect("/Admin/Invoicing/SubmitInvoicingFromAdmin?HoureID=" + invoicing1.HoureReservationID);
        }
        public IActionResult EditInvoicingDetail(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            var invoicing = _context.invoicingDetailsRepository.GetInvoicingDetailByID((int)id);
            Invoicing invoicing1 = _context.invoicingRepository.GetInvoicingByID(invoicing.InvoicingID);
            if (invoicing == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ViewBag.HourId = invoicing1.HoureReservationID;

            return View(invoicing);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditInvoicingDetail(InvoicingDetail invoicingDetail)
        {
            if (ModelState.IsValid)
            {
                _context.invoicingDetailsRepository.UpdateInvoicingDetail(invoicingDetail);
                _context.SaveChangesDB();

                Invoicing invoicing = _context.invoicingRepository.GetInvoicingByID(invoicingDetail.InvoicingID);
                return Redirect("/Admin/Invoicing/SubmitInvoicingFromAdmin?HoureID=" + invoicing.HoureReservationID);
            }

            return View(invoicingDetail);
        }
        public async Task<IActionResult> CheckInvoicingFromUserSide(int? HoureID)
        {
            if (HoureID == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            Invoicing invoicing = _context.invoicingRepository.GetInvoicingByHourID((int)HoureID);
            HourReservation houre = _context.hourReservationRepository.GetHoureReservationByID((int)invoicing.HoureReservationID);
            if (invoicing == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (houre == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ViewBag.HoureInfo = houre;
            ViewBag.InvoicingDetail = _context.invoicingDetailsRepository.GetListOfInvoicingDetailByInvoicingId(invoicing.InvoicingID);
            ViewBag.Employee = await _userManager.FindByIdAsync(invoicing.EmployeeID);

            return View(invoicing);
        }

    }
}
