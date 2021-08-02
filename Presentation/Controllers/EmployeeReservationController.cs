using DataAccess.Design_Pattern.UnitOfWork;
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

        private readonly IUnitOfWork _context;

        public EmployeeReservationController(IUnitOfWork context)
        {
            _context = context;
        }

        #endregion
        public IActionResult ShowEmployeeInfoPage(int? id)
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

            return View(userSelectedJob);
        }
    }
}
