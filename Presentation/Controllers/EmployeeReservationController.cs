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
        public async Task<IActionResult> ShowEmployeeInfoPage(int? id, string Houre = "empty")
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
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
                                                    .GetDateReservationAfterTodayByEmployeeId(userSelectedJob.Userid);
            ViewBag.userSelectedjobsid = id;
            ViewBag.EmployeePoint = _context.ScoreRepository.CalculateEmployeeScore(userSelectedJob.Userid);

            if (!_context.ScoreRepository.IsExistScoreFromUserToEmployee(user.Id , userSelectedJob.Userid))
            {
                ViewBag.Score = true;
            }
            if (Houre != "empty")
            {
                ViewBag.Houre = true;
            }
            return View(userSelectedJob);
        }
        public IActionResult OnlineReservation(int? EmployeeInfo)
        {
            if (EmployeeInfo == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            UserSelectedJob EmployeeDuc = _context.userSelectedJobRepository.GetUserSelectedJobByID((int)EmployeeInfo);
            DataReservation date = _context.dataReservationRepository.AddDateTimeReservationWhileOnlineProcees(EmployeeDuc.Userid);
            _context.SaveChangesDB();

            HourReservation hour = _context.hourReservationRepository.AddHourReservationWhileOnlineProccess(EmployeeDuc.Userid, date.DataReservationID);
            _context.SaveChangesDB();

            return Redirect($"/EmployeeReservation/ReservationEmployee?Houre={hour.HourReservationID}&&EmployeeInfo={EmployeeInfo}");
        }
        public async Task<IActionResult> ReservationEmployee(int? Houre, int? EmployeeInfo)
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
            if (houre.EndHourReservationInt == 0)
            {
                return View(new EmployeeReservationViewModel()
                {

                    UserReservationStatus = 1,
                    EmployeeID = EmployeeDuc.Userid,
                    UserID = user.Id,
                    JobCategoryID = EmployeeDuc.JobCategoryId,
                    HoureReservationID = houre.HourReservationID,
                    DateReservationID = houre.DataReservationID,
                    DateTimeReservation = DateTime.Now

                });
            }
            else
            {
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
            List<Location> Locations = _context.locationAddressRepository.GetUserLocations(user.Id);

            return View(Locations);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetLocationForReservation(string Location, string postalCode)
        {
            if (HttpContext.Session.GetComplexData<EmployeeReservationViewModel>("ReservationData") == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                _context.locationAddressRepository.AddLocationBeforeReserve(user.Id, Location, postalCode);
                _context.SaveChangesDB();

                List<Location> Locationss = _context.locationAddressRepository.GetUserLocations(user.Id);

                return View(Locationss);
            }
            List<Location> Locations = _context.locationAddressRepository.GetUserLocations(user.Id);


            return View(Locations);
        }

        public async Task<IActionResult> SubmitReservationRequest(int? LocationId)
        {
            if (LocationId == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (HttpContext.Session.GetComplexData<EmployeeReservationViewModel>("ReservationData") == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            EmployeeReservationViewModel reserve = HttpContext.Session.GetComplexData<EmployeeReservationViewModel>("ReservationData");
            reserve.LocationID = (int)LocationId;

            var hour = _context.hourReservationRepository.GetHoureReservationByID(reserve.HoureReservationID);
            if (hour.ReservationStatusID == 1)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            hour.ReservationStatusID = 1;
            _context.hourReservationRepository.UpdateHourReservationFromEmployee(hour);

            if (reserve.UserReservationStatus == 1)
            {
                var Employee = await _userManager.FindByIdAsync(reserve.EmployeeID);
                Employee.EmployeeStatusID = 3;
            }

            ReservationOrder Reservation = _context.reservaitionOrderRepository.AddRservationOrderFromSession(reserve);
            _context.SaveChangesDB();

            return Redirect("/Home/Index?AddReservation=true&&ReservationId=" + Reservation.ReservationOrderID);
        }

    }
}
