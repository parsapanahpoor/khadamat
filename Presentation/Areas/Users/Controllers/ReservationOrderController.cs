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
        public async Task<IActionResult> NextReservations()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<ReservationOrder> ListOFReservation = _context.reservaitionOrderRepository
                                                                    .GetUserNetReservationOrderForShowInUserPanel(user.Id);

            return View(ListOFReservation);
        }
        public async Task<IActionResult> LastestReservations()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<ReservationOrder> ListOFReservation = _context.reservaitionOrderRepository
                                                                    .GetUserLaterReservationOrderForShowInUserPanel(user.Id);
            return View(ListOFReservation);
        }

        public async Task<IActionResult> OrderReservationInfo(int? id , bool Later = false)
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

            ViewBag.EmployeeInfo =await _userManager.FindByIdAsync(orderReservationInfo.EmployeeID);

            return View(orderReservationInfo);
        }
     
    }
}
