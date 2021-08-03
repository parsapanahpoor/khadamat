using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.EmployeeReservation;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class EmployeeReservationController : Controller
    {
        #region Context

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;


        public EmployeeReservationController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }

        #endregion
        public IActionResult ShowEmployeeInfoPage(int? id , string Houre = "empty")
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            UserSelectedJob userSelectedJob = _context.userSelectedJobRepository.GetUserSelectedJobByID((int)id);
            if (userSelectedJob == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ViewBag.ListOFEmployeeDateReservation = _context.dataReservationRepository
                                                    .GetDateReservationByEmployeeId(userSelectedJob.Userid);
            if (Houre != "empty")
            {
                ViewBag.Houre = true;
            }
            return View(userSelectedJob);
        }
        public IActionResult ReservationEmployee(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            HourReservation houre = _context.hourReservationRepository.GetHoureReservationByID((int)id);
            if (houre == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            //var user = _user
            return View();
        }
    }
}
