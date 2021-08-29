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
            _context.SaveChangesDB();

            return Redirect("/Admin/ReservationOrder/ListOfDateReservation?id="+invoicing.DateReservationID);
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
    }
}
