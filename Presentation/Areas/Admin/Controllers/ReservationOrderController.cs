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

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReservationOrderController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _context;


        public ReservationOrderController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        #endregion

        public IActionResult Index()
        {
            List<ReservationOrder> reservations = _context.reservaitionOrderRepository.GetAllTodayReservationOrder();

            List<string> EmployeeID = _context.reservaitionOrderRepository.GetAllEmployeeIDHaveReservationToday();
            ViewBag.EmployeeList = _context.userRepository.GetEmployeeForShowTodayReservation(EmployeeID);

            return View(reservations);
        }
        public async Task<IActionResult> ReservationOrderInformation(int? id , bool Index = false)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ReservationOrder reservation = _context.reservaitionOrderRepository.GetReservationOrderById((int)id);
            if (reservation == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ViewBag.Employee = await _userManager.FindByIdAsync(reservation.EmployeeID);
            if (Index == true)
            {
                ViewBag.Index = true;
            }
            return View(reservation);
        }
        public async Task<IActionResult> ListOfEmployeeForReservationProccess()
        {
            List<User> usersid = (List<User>)await _userManager.GetUsersInRoleAsync("Employee");
            return View(usersid);
        }
    }
}
