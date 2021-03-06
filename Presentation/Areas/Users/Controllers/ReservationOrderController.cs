using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.EmployeeReservation;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize(Roles = "User")]
    public class ReservationOrderController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;


        public ReservationOrderController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        #endregion
        public async Task<IActionResult> NextReservations(bool Delete = false)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<ReservationOrder> ListOFReservation = _context.reservaitionOrderRepository
                                                                    .GetUserNetReservationOrderForShowInUserPanel(user.Id);
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }

            return View(ListOFReservation);
        }
        public async Task<IActionResult> LastestReservations()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<ReservationOrder> ListOFReservation = _context.reservaitionOrderRepository
                                                                    .GetUserLaterReservationOrderForShowInUserPanel(user.Id);
            return View(ListOFReservation);
        }

        public async Task<IActionResult> OrderReservationInfo(int? id, bool Later = false, bool Home = false)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ReservationOrder orderReservationInfo = _context.reservaitionOrderRepository.GetReservationOrderById((int)id);
            if (orderReservationInfo == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (Later == true)
            {
                ViewBag.Later = true;
            }
            if (Home == true)
            {
                ViewBag.Home = true;
            }

            ViewBag.EmployeeInfo = await _userManager.FindByIdAsync(orderReservationInfo.EmployeeID);

            return View(orderReservationInfo);
        }

        public IActionResult DeleteReservationOrder(int? ReservationOrderId)
        {
            if (ReservationOrderId == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ReservationOrder reservation = _context.reservaitionOrderRepository.GetReservationOrderById((int)ReservationOrderId);
            if (reservation == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
        

            return View(reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReservationOrder(ReservationOrder reservationOrder)
        {
            if (ModelState.IsValid)
            {
                reservationOrder.IsDelete = true;
                _context.reservaitionOrderRepository.UpdateReservationOrder(reservationOrder);

                HourReservation hour = _context.hourReservationRepository.GetHoureReservationByID((int)reservationOrder.HoureReservationID);
                if (reservationOrder.UserReservationStatus == 1)
                {
                    _context.hourReservationRepository.DeleteHourReservationFromEmployeePanel(hour);

                    var employee = await _userManager.FindByIdAsync((string)reservationOrder.EmployeeID);
                    employee.EmployeeStatusID =  1;
                    var result = await _userManager.UpdateAsync(employee);
                }
                if (reservationOrder.UserReservationStatus == 2)
                {
                    _context.hourReservationRepository.DeleteHourReservationFromEmployeePanel(hour);
                }

                _context.SaveChangesDB();
                return Redirect("/Users/ReservationOrder/NextReservations?Delete=true");
            }
     
            return View(reservationOrder);
        }

    }
}
