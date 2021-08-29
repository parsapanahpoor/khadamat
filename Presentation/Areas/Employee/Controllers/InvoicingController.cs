using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Entities.EmployeeReservation;
using Models.Entities.Factor;
using Models.Entities.User;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class InvoicingController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;


        public InvoicingController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        #endregion

        public IActionResult WarrningForSubmitInvoicing(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            ViewBag.ReservationOrderID = id;

            return View();
        }

        [HttpPost]
        public IActionResult WarrningForSubmitInvoicing(FirstStepForInvoicing invoicing)
        {
            if (ModelState.IsValid)
            {
                ReservationOrder reservation = _context.reservaitionOrderRepository.GetReservationOrderById(invoicing.ReservationOrderID);
                Invoicing invoicing1 = _context.invoicingRepository.AddInvoicingByReservationOrderInformations(reservation, invoicing);
                _context.reservaitionOrderRepository.EndOfReservationOredr(reservation);

                _context.SaveChangesDB();
                return Redirect("/Employee/Invoicing/InvoicingDetails?Id=" + invoicing1.InvoicingID);
            }

            return View("~/Views/Shared/_404.cshtml");
        }

        public IActionResult InvoicingDetails(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            Invoicing invoicing = _context.invoicingRepository.GetInvoicingByID((int)id);
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

            return View(invoicing);
        }

        [HttpPost]
        public IActionResult AddInvoicingDetails(int InvoicingId, int? TariffID, decimal Price, string Description = "")
        {
            if (ModelState.IsValid)
            {
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

                return Redirect("/Employee/Invoicing/InvoicingDetails?Id=" + InvoicingId);
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
            if (invoicing == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            _context.invoicingDetailsRepository.DeleteInvoicingDetailSoftDelete(invoicing);
            _context.SaveChangesDB();

            return Redirect("/Employee/Invoicing/InvoicingDetails?Id=" + invoicing.InvoicingID);
        }
        public IActionResult LastStepFromEmployeePanel(int? id)
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
            _context.invoicingRepository.CloseInvoicingFromEmployeePanel(invoicing);
            _context.SaveChangesDB();

            return Redirect("/Employee/Home/Index?CloseInvoicing=True");
        }
        public async Task<IActionResult> CheckPayedInvoicings(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            Invoicing invoicing = _context.invoicingRepository.GetInvoicingByID((int)id);
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
