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

namespace Presentation.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize(Roles = "User")]
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

        public IActionResult PaymentInvoicingAmount(int? id)
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
            List<InvoicingDetail> invoicingDetails = _context.invoicingDetailsRepository.GetListOfInvoicingDetailByInvoicingId((int)invoicing.InvoicingID);
            if (invoicingDetails != null)
            {
                int Amount = 0;  

                foreach (var item in invoicingDetails)
                {
                    Amount = (int)(Amount + item.Price);
                }

                #region Online Payment

                var payment = new ZarinpalSandbox.Payment((int)Amount);

                var res = payment.PaymentRequest("پرداخت  ", "https://localhost:44394/OnlinePayment/" + invoicing.InvoicingID, "Parsapanahpoor@yahoo.com", "09117878804");

                if (res.Result.Status == 100)
                {
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
                }

                #endregion

            }
            return View("~/Views/Shared/_404.cshtml");
        }
    }
}
