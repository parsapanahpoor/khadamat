using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Entities.EmployeeReservation;
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
                _context.invoicingRepository.AddInvoicingByReservationOrderInformations(reservation , invoicing);
                _context.reservaitionOrderRepository.EndOfReservationOredr(reservation);

                _context.SaveChangesDB();

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

            return View();
        }

        public IActionResult InvoicingFromEmployeePanel(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ReservationOrder reservationOrder = _context.reservaitionOrderRepository.GetReservationOrderById((int)id);
            ViewBag.ReservationOrder = reservationOrder;

            ViewBag.ListOfInvoicingDetails = HttpContext.Session.GetComplexData<List<DataAccess.ViewModels.InvoicingDetail>>("invoicingDetailList");

            return View();
        }

        [HttpPost]
        public IActionResult AddInvoicingDetailsToSession(DataAccess.ViewModels.InvoicingDetail invoicingDetail)
        {
            HttpContext.Session.SetComplexData("invoicingDetailList", invoicingDetail);

            return Redirect("/Employee/Invoicing/InvoicingFromEmployeePanel?Id=" + invoicingDetail.ReservationOrderID);
        }
    }
}
