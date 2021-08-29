using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.EmployeeReservation;
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

        public async Task<IActionResult> CheckInvoicingFromUser(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            Invoicing invoicing = _context.invoicingRepository.GetInvoicingByReservationOrderID((int)id);
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
