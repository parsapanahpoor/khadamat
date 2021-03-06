using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.EmployeeReservation;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class EmployeeReservationController : Controller
    {
        #region Constructor
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

        public async Task<IActionResult> ChangeStatusByIconInPartial(int id, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            List<HourReservation> DateReservation = _context.hourReservationRepository
                                                    .GetJustTodayEmployeeHourReservationByEmployeeId(user.Id);
            if (_context.hourReservationRepository.ISEmployeeHaveHObTightNow(DateReservation))
            {
                user.EmployeeStatusID = 3;
            } 
            if (_context.hourReservationRepository.IsExistHourReservation2HourBefore(DateReservation))
            {
                user.EmployeeStatusID = 1;
            }
            if (!_context.hourReservationRepository.IsExistHourReservation2HourBefore(DateReservation) &&
                !_context.hourReservationRepository.ISEmployeeHaveHObTightNow(DateReservation))
            {
                if (id == 1)
                {
                    user.EmployeeStatusID = 1;
                }
                if (id == 2)
                {
                    user.EmployeeStatusID = 2;
                }
            }
            var result = await _userManager.UpdateAsync(user);
            _context.SaveChangesDB();

            if (_context.hourReservationRepository.ISEmployeeHaveHObTightNow(DateReservation))
            {
                return Redirect("/Employee/Home/Index?Status=3");
            }
            if (_context.hourReservationRepository.IsExistHourReservation2HourBefore(DateReservation))
            {
                return Redirect("/Employee/Home/Index?Status=1");
            }
            else
            {
                if (id == 1)
                {
                    return Redirect("/Employee/Home/Index?Status=1");
                }
                if (id == 2)
                {
                    return Redirect("/Employee/Home/Index?Status=2");
                }
            }

            return View();
        }

        #region DataReservation Part


        public async Task<IActionResult> ListOfEmployeeDateReservations(bool Create = false, bool Edit = false, bool Delete = false
                            , bool ErrorDelete = false, bool ErrorEdit = false, bool ErrorDate = false)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = _context.dataReservationRepository.GetListOfEmployeeDataReservation(user.Id);

            if (Create == true)
            {
                ViewBag.Create = true;
            }
            if (Edit == true)
            {
                ViewBag.Edit = true;
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }
            if (ErrorDelete == true)
            {
                ViewBag.ErrorDelete = true;
            }
            if (ErrorEdit == true)
            {
                ViewBag.ErrorEdit = true;
            }
            if (ErrorDate == true)
            {
                ViewBag.ErrorDate = true;
            }

            return View(model);
        }

        public async Task<IActionResult> GetListOfEmployeeDataReservationHistory()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = _context.dataReservationRepository.GetListOfEmployeeDataReservationHistory(user.Id);

            return View(model);
        }
        public IActionResult CreateDataReservation()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDataReservation(string ReservationDateTimeString)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);


                string[] std = ReservationDateTimeString.Split('/');

                DateTime eDateTime = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );

                DateTime DateTime = eDateTime;

                if (_context.dataReservationRepository.IsExistSpicialDateReservation(user.Id, DateTime))
                {
                    return Redirect("/Employee/EmployeeReservation/ListOfEmployeeDateReservations?ErrorDate=true");
                }

                _context.dataReservationRepository.AddDataReservationFromEmployeePanel(DateTime, user.Id);
                _context.SaveChangesDB();

                return Redirect("/Employee/EmployeeReservation/ListOfEmployeeDateReservations?Create=true");
            }

            return View();
        }

        public IActionResult EditDateReservation(int? id, bool Delete = false)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");

            }

            var model = _context.dataReservationRepository.GetDataReservationById((int)id);

            if (model == null)
            {
                return View("~/Views/Shared/_404.cshtml");

            }

            List<HourReservation> hourReservation = _context.hourReservationRepository
                    .GetEmployeeHourReservationByDateHourReservationID((int)id);

            if (hourReservation.Count() > 0)
            {
                return Redirect("/Employee/EmployeeReservation/ListOfEmployeeDateReservations?ErrorEdit=true");

            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDateReservation(DataReservation date, string ReservationDateTimeString)
        {
            if (ModelState.IsValid)
            {
                string[] std = ReservationDateTimeString.Split('/');

                DateTime eDateTime = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );

                DateTime DateTime = eDateTime;

                date.ReservationDateTime = DateTime;

                _context.dataReservationRepository.UpdateDateReservationFromEmployeePanel(date);
                _context.SaveChangesDB();

                return Redirect("/Employee/EmployeeReservation/ListOfEmployeeDateReservations?Edit=true");
            }

            return View(date);
        }

        public IActionResult DeleteDateReservation(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");

            }

            DataReservation date = _context.dataReservationRepository.GetDataReservationById((int)id);
            if (date == null)
            {
                return View("~/Views/Shared/_404.cshtml");

            }
            List<HourReservation> hourReservation = _context.hourReservationRepository
                      .GetEmployeeHourReservationByDateHourReservationID(date.DataReservationID);
            if (hourReservation.Count() > 0)
            {
                return Redirect("/Employee/EmployeeReservation/ListOfEmployeeDateReservations?ErrorDelete=true");

            }

            _context.dataReservationRepository.DeleteDateReservation(date);
            _context.SaveChangesDB();

            return Redirect("/Employee/EmployeeReservation/ListOfEmployeeDateReservations?Delete=true");
        }

        #endregion

        #region HourReservation Part

        public IActionResult ListOfDateReservation(int? id, bool Create = false, bool Edit = false, bool Delete = false, bool History = false)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            var model = _context.hourReservationRepository.GetEmployeeHourReservationByDateHourReservationID((int)id);

            if (model == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            if (Create == true)
            {
                ViewBag.Create = true;
            }
            if (Edit == true)
            {
                ViewBag.Edit = true;
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }
            if (History == true)
            {
                ViewBag.History = true;
            }

            ViewBag.DateReservation = id;

            return View(model);
        }

        public IActionResult CreateHourReservation(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");

            }

            ViewBag.Date = id;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHourReservation(AddHourReservationFromEmployeeVM addHourReservation)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                bool result = _context.hourReservationRepository.AddHourReservationFromEmployeePanel(addHourReservation, user.Id);

                if (_context.hourReservationRepository.IsExistSpecialHour(user.Id, addHourReservation.DataReservationID, addHourReservation.StartHour, addHourReservation.EndHour))
                {
                    ViewBag.ErrorHour = true;
                    ViewBag.Date = addHourReservation.DataReservationID;
                    return View(addHourReservation);
                }
                else
                {
                    if (result == true)
                    {
                        _context.SaveChangesDB();
                        return Redirect("/Employee/EmployeeReservation/ListOfDateReservation?id=" + addHourReservation.DataReservationID + "&&Create=true");
                    }
                    if (result == false)
                    {
                        ViewBag.Error = true;
                        ViewBag.Date = addHourReservation.DataReservationID;
                        return View(addHourReservation);
                    }
                }
            }

            ViewBag.Date = addHourReservation.DataReservationID;

            return View(addHourReservation);
        }

        public IActionResult UpdateHourReservation(int? id, bool Delete = false)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            HourReservation hour = _context.hourReservationRepository.GetHourReservation((int)id);

            if (hour == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (hour.ReservationStatusID == 1)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }

            return View(hour);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateHourReservation(HourReservation hourReservation)
        {
            if (ModelState.IsValid)
            {
                bool result = _context.hourReservationRepository.UpdateHourReservationFromEmployee(hourReservation);
                if (_context.hourReservationRepository.IsExistSpecialHour(hourReservation.EmployeeID, hourReservation.DataReservationID, hourReservation.StartHourReservation, hourReservation.EndHourReservation))
                {
                    ViewBag.ErrorHour = true;
                    return View(hourReservation);
                }
                else
                {
                    if (result == true)
                    {
                        _context.SaveChangesDB();
                        return Redirect("/Employee/EmployeeReservation/ListOfDateReservation?id=" + hourReservation.DataReservationID + "&&Edit=true");

                    }
                    if (result == false)
                    {
                        ViewBag.Error = true;
                        return View(hourReservation);
                    }
                }
            }
            return View(hourReservation);
        }
        public IActionResult DeleteHourReservation(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            HourReservation hour = _context.hourReservationRepository.GetHourReservation((int)id);
            if (hour == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            _context.hourReservationRepository.DeleteHourReservationFromEmployeePanel(hour);
            _context.SaveChangesDB();

            return Redirect("/Employee/EmployeeReservation/ListOfDateReservation?id=" + hour.DataReservationID + "&&Delete=true");
        }

        public async Task<IActionResult> HoureReservationInfo(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ReservationOrder reservation = _context.reservaitionOrderRepository
                                                        .GetReservationOrderByHourReservationId((int)id);
            if (reservation == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            ViewBag.User = await _userManager.FindByIdAsync(reservation.UserID);

            return View(reservation);
        }
        #endregion
    }
}
