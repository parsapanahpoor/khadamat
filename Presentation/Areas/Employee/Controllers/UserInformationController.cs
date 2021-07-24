using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class UserInformationController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;


        public UserInformationController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        #endregion

        public async Task<IActionResult> EmployeeDocument()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = _context.employeeRepository.GetEmployeeDocument(user.Id);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeDocument(EmployeeDocuments employee, IFormFile Picture, IFormFile Certificate,
            bool MariatalStatus, bool HealthStatus, bool MilitaryService, bool HistoryOfDetention, bool Obligation, string BirthDayeDate,
            string PersonalCodeCreateDate
            )
        {
            if (ModelState.IsValid)
            {

                if (MariatalStatus == true)
                {
                    employee.MaritalStatus = true;
                }
                if (MariatalStatus == false)
                {
                    employee.MaritalStatus = false;
                }

                if (HealthStatus == true)
                {
                    employee.HealthStatus = true;
                }
                if (HealthStatus == false)
                {
                    employee.HealthStatus = false;
                }

                if (MilitaryService == true)
                {
                    employee.MilitaryService = true;
                }
                if (MilitaryService == false)
                {
                    employee.MilitaryService = false;
                }

                if (HistoryOfDetention == true)
                {
                    employee.HistoryOfDetention = true;
                }
                if (HistoryOfDetention == false)
                {
                    employee.HistoryOfDetention = false;
                }
                if (Obligation != true)
                {
                    ModelState.AddModelError("", "لطفا تعهد نامه را تایید بفرمایید  ");
                    return View(employee);
                }

                #region CreateDate

                string[] std = BirthDayeDate.Split('/');

                DateTime BirthDayeDateTime = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );

                employee.BirthDay = BirthDayeDateTime;



                string[] stdd = PersonalCodeCreateDate.Split('/');

                DateTime PersonalCodeDateCreate = new DateTime(int.Parse(stdd[0]),
                    int.Parse(stdd[1]),
                    int.Parse(stdd[2]),
                    new PersianCalendar()
                    );

                employee.PersonalCodeDate = PersonalCodeDateCreate;
                #endregion


                var user = await _userManager.FindByIdAsync(employee.Userid);
                user.IsAccepted = null;
                var task = await _userManager.UpdateAsync(user);


                employee.PossitionId = 2;
                _context.employeeRepository.UpdateEmployeeDocumentFromEmployeePanel(employee, Picture, Certificate);



                _context.SaveChangesDB();
                return Redirect("/Employee/Home/Index");
            }
            return View(employee);
        }


    }
}
