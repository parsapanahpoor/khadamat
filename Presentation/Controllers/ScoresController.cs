using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class ScoresController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;

        public ScoresController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        #endregion

        public async Task<IActionResult> AddScoreToTheEmployee(int? ReturnId , string EmployeeID , int? Point)
        {
            if (ReturnId == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (EmployeeID == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            if (Point == null)
            {
                return View("~/Views/Shared/_404.cshtml");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (_context.ScoreRepository.IsExistScoreFromUserToEmployee(user.Id , EmployeeID))
            {
                return View("~/Views/Shared/_404.cshtml");
            }
            _context.ScoreRepository.AddScoreToTheEmployee(user.Id,EmployeeID,(int)Point);
            _context.SaveChangesDB();

            return Redirect("/EmployeeReservation/ShowEmployeeInfoPage?id=" + ReturnId);
        }
    }
}
