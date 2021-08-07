using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        public async Task<IActionResult> ReservationEmployee(int? Houre , int? EmployeeInfo)
        {
            if (Houre == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (EmployeeInfo == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            HourReservation houre = _context.hourReservationRepository.GetHoureReservationByID((int)Houre);
            UserSelectedJob EmployeeDuc = _context.userSelectedJobRepository.GetUserSelectedJobByID((int)EmployeeInfo);

            if (houre == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (EmployeeDuc == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.HoureInfo = houre;
            ViewBag.EmployeeInfo = EmployeeDuc;

            return View(new EmployeeReservationViewModel()
            { 
            
                UserReservationStatus = 2,
                EmployeeID = EmployeeDuc.Userid,
                UserID = user.Id,
                JobCategoryID = EmployeeDuc.JobCategoryId,
                HoureReservationID = houre.HourReservationID,
                DateReservationID = houre.DataReservationID,
                DateTimeReservation = DateTime.Now
            
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReservationEmployee(EmployeeReservationViewModel employee)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetComplexData("ReservationData", employee);
                return RedirectToAction(nameof(GetLocationForReservation));
            }

            return View("~/Views/Shared/_404.cshtml");
        }

        public async Task<IActionResult> GetLocationForReservation()
        {
            if (HttpContext.Session.GetComplexData<EmployeeReservationViewModel>("ReservationData") == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View();
        }

    }
}
