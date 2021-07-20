using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {


        public IActionResult Index(bool Edit = false)
        {
            if (Edit == true)
            {
                ViewBag.Edit = true;
            }

            return View();
        }
    }
}
