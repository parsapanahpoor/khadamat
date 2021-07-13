using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(bool Register = false , bool Login = false , bool EmployeeRegister = false
                                        , bool ConfirmEmail = false)
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

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
