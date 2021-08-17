using DataAccess.Design_Pattern.UnitOfWork;
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
        public async Task<IActionResult> ReservationOrderInformation(int? id, int? HoureID, bool Index = false, bool HoureIndex = false, bool UserPage = false
            , bool HistoryIndex = false)
        {
            if (id == null && HoureID == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (id != null)
            {
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
                if (UserPage == true)
                {
                    ViewBag.UserPage = true;
                }
                if (HistoryIndex == true)
                {
                    ViewBag.HistoryIndex = true;
                }

                return View(reservation);
            }
            else
            {
                ReservationOrder reservation = _context.reservaitionOrderRepository.GetReservationOrderByHourReservationId((int)HoureID);
                if (reservation == null)
                {
                    return View("~/Views/Shared/_404.cshtml");
                }
                ViewBag.Employee = await _userManager.FindByIdAsync(reservation.EmployeeID);
                if (HoureIndex == true)
                {
                    ViewBag.HoureIndex = true;
                }
                return View(reservation);
            }
        }
        public async Task<IActionResult> ListOfEmployeeForReservationProccess()
        {
            List<User> EmployeeList = (List<User>)await _userManager.GetUsersInRoleAsync("Employee");
            return View(EmployeeList);
        }
        public IActionResult CheckEmployeeWorkingDate(string id, bool ErrorEdit = false, bool Edit = false, bool ErrorDelete = false,
            bool Delete = false)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            var DateReservation = _context.dataReservationRepository.GetDateReservationByEmployeeId(id);
            if (ErrorEdit == true)
            {
                ViewBag.ErrorEdit = true;
            }
            if (Edit == true)
            {
                ViewBag.Edit = true;
            }
            if (ErrorDelete == true)
            {
                ViewBag.ErrorDelete = true;
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }

            return View(DateReservation);
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
                return Redirect("/Admin/ReservationOrder/CheckEmployeeWorkingDate?id=" + model.EmployeeID + "&&ErrorEdit=true");

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

                return Redirect("/Admin/ReservationOrder/CheckEmployeeWorkingDate?id=" + date.EmployeeID + "&&Edit=True");
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
                return Redirect("/Admin/ReservationOrder/CheckEmployeeWorkingDate?id=" + date.EmployeeID + "&&ErrorDelete=true");

            }

            _context.dataReservationRepository.DeleteDateReservation(date);
            _context.SaveChangesDB();

            return Redirect("/Admin/ReservationOrder/CheckEmployeeWorkingDate?id=" + date.EmployeeID + "&&Delete=True");
        }

        #region HoureReservation Part
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
                ViewBag.Alert = true;
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
                if (result == true)
                {
                    _context.SaveChangesDB();
                    return Redirect("/Admin/ReservationOrder/ListOfDateReservation?id=" + hourReservation.DataReservationID + "&&Edit=true");

                }
                if (result == false)
                {
                    ViewBag.Error = true;
                    return View(hourReservation);
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
            if (hour.ReservationStatusID == 2)
            {
                ReservationOrder reservation = _context.reservaitionOrderRepository.GetReservationOrderByHourReservationID(hour.HourReservationID);
                _context.reservaitionOrderRepository.DeleteReservationOrder(reservation);

                _context.hourReservationRepository.DeleteHourReservationFromEmployeePanel(hour);
            }
            if (hour.ReservationStatusID == 1)
            {
                ReservationOrder reservation = _context.reservaitionOrderRepository.GetReservationOrderByHourReservationId(hour.HourReservationID);

                _context.reservaitionOrderRepository.DeleteReservationOrder(reservation);
                _context.hourReservationRepository.DeleteHourReservationFromEmployeePanel(hour);
            }
            _context.SaveChangesDB();

            return Redirect("/Admin/ReservationOrder/ListOfDateReservation?id=" + hour.DataReservationID + "&&Delete=true");
        }

        #endregion

        #region List Of Users

        public async Task<IActionResult> ListOfUsers()
        {
            List<User> UsersList = (List<User>)await _userManager.GetUsersInRoleAsync("User");
            return View(UsersList);
        }
        public IActionResult CheckUserDateReservation(string id, bool Delete = false)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            List<ReservationOrder> reservations = _context.reservaitionOrderRepository.GetAllUserReservationOrderByUserid(id);
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }
            return View(reservations);
        }
        public IActionResult ShowDeletePage(int? id , bool HistoryIndex = false)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            ReservationOrder reservation = _context.reservaitionOrderRepository.GetReservationOrderById((int)id);
            if (HistoryIndex == true)
            {
                ViewBag.HistoryIndex = true;
            }
            return View(reservation);
        }
        public IActionResult DeleteOrderReservation(int? id, bool UserPage = false , bool HistoryIndex = false)
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
            _context.reservaitionOrderRepository.DeleteReservationOrder(reservation);
            HourReservation hour = _context.hourReservationRepository.GetHoureReservationByID((int)reservation.HoureReservationID);
            hour.ReservationStatusID = 2;
            _context.hourReservationRepository.UpdateHourReservationAfterDeleteReservationOrder(hour);

            _context.SaveChangesDB();
            if (UserPage == true)
            {
                return Redirect("/Admin/ReservationOrder/CheckUserDateReservation?id=" + reservation.UserID + "&&Delete=True");

            }  
            if (HistoryIndex == true)
            {
                return Redirect("/Admin/ReservationOrder/HistoryOfReservationOrder?id=" + reservation.UserID + "&&Delete=True");

            }

            return Redirect("/Admin/ReservationOrder/ListOfDateReservation?id=" + hour.DataReservationID + "&&Delete=true");
        }
        #endregion
        #region HistoryOfReservationOrder

        public IActionResult HistoryOfReservationOrder(bool Delete = false)
        {
            List<ReservationOrder> ReservationOrders = _context.reservaitionOrderRepository.GetAllReservationOrder();

            List<string> EmployeeID = _context.reservaitionOrderRepository.GetAllEmployeeID();
            ViewBag.EmployeeList = _context.userRepository.GetEmployeeForShowTodayReservation(EmployeeID);
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }
            return View(ReservationOrders);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HistoryOfReservationOrder(string StartDate = "", string EndDate = "")
        {
            List<ReservationOrder> list = new List<ReservationOrder>();
            list.AddRange(_context.reservaitionOrderRepository.GetAllReservationOrder());

            if (StartDate != null)
            {
                string[] std = StartDate.Split('/');
                DateTime StartDATE = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );

                //PersianCalendar p = new PersianCalendar();
                //DateTime x = p.ToDateTime(StartDATE.Year, StartDATE.Month, StartDATE.Day, StartDATE.Hour, StartDATE.Minute, StartDATE.Second, StartDATE.Millisecond);
                //int y, m, d;
                //y = x.Year;
                //m = x.Month;
                //d = x.Day;

                list = list.Where(p => p.DataReservation.ReservationDateTime.Year >= StartDATE.Year
                                            && p.DataReservation.ReservationDateTime.Month >= StartDATE.Month
                                                    && p.DataReservation.ReservationDateTime.Day >= StartDATE.Day
                                                    &&p.DataReservation.ReservationDateTime.Hour >= StartDATE.Hour
                                                    &&p.DataReservation.ReservationDateTime.Minute >= StartDATE.Minute
                                                    &&p.DataReservation.ReservationDateTime.Second >= StartDATE.Second
                                                    &&p.DataReservation.ReservationDateTime.Millisecond >= StartDATE.Millisecond).ToList();
            }
            if (EndDate != null)
            {
                string[] edd = EndDate.Split('/');
                DateTime EndDATE = new DateTime(int.Parse(edd[0]),
                    int.Parse(edd[1]),
                    int.Parse(edd[2]),
                    new PersianCalendar()
                    );

                //PersianCalendar p = new PersianCalendar();
                //DateTime x = p.ToDateTime(EndDATE.Year, EndDATE.Month, EndDATE.Day, EndDATE.Hour, EndDATE.Minute, EndDATE.Second, EndDATE.Millisecond);
                //int y, m, d;
                //y = x.Year;
                //m = x.Month;
                //d = x.Day;

                list = list.Where(p => p.DataReservation.ReservationDateTime.Year <= EndDATE.Year
                                            && p.DataReservation.ReservationDateTime.Month <= EndDATE.Month
                                                    && p.DataReservation.ReservationDateTime.Day <= EndDATE.Day).ToList();
            }

            List<string> EmployeeID = _context.reservaitionOrderRepository.GetAllEmployeeID();
            ViewBag.EmployeeList = _context.userRepository.GetEmployeeForShowTodayReservation(EmployeeID);

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            return View(list);
        }
        #endregion
    }
}
