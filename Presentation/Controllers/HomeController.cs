using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _context;

        public HomeController(IUnitOfWork context)
        {
            _context = context;

        }


        public IActionResult Index(bool Register = false, bool Login = false, bool EmployeeRegister = false
                                        , bool ConfirmEmail = false, bool ForgotPassword = false, bool AddReservation = false)
        {
            if (Register == true)
            {
                ViewBag.Create = true;
            }
            if (Login == true)
            {
                ViewBag.Login = true;
            }
            if (EmployeeRegister == true)
            {
                ViewBag.EmployeeRegister = true;
            }
            if (ConfirmEmail == true)
            {
                ViewBag.ConfirmEmail = true;
            }
            if (ForgotPassword == true)
            {
                ViewBag.ForgotPassword = true;
            }
            if (AddReservation == true)
            {
                ViewBag.AddReservation = true;
            }

            return View(_context.jobCategoryRepository.GetAllJobsCategories());
        }


        public IActionResult SubGruopCategories(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            return View(_context.jobCategoryRepository.GetSubGroupOfJobCategorie((int)id));
        }

        public IActionResult ShowListOfEmployeesThatHaveThisJob(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            var model = _context.userSelectedJobRepository.GetListOfEmployeeThatHaveThisJob((int)id);

            return View(model);
        }
        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            list.AddRange(_context.tariffRepository.GetSubTariffForCreateInvoicing(id));
            return Json(new SelectList(list, "Value", "Text"));
        }
    }
}
