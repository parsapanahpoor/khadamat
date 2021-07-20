using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class AccountController : Controller
    {

        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;


        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        #endregion
       
        public async Task<IActionResult> EditUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return View("~/Views/Shared/_404.cshtml");


            EditUserInAdminPanel editUser = new EditUserInAdminPanel()
            {

                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                AvatarName = user.UserAvatar



            };

            return View(editUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, EditUserInAdminPanel userEdited)
        {
            if (ModelState.IsValid)
            {


                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(userEdited.UserName))
                    return View("~/Views/Shared/_404.cshtml");

                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    View("~/Views/Shared/_404.cshtml");

                user.UserName = userEdited.UserName;
                user.PhoneNumber = userEdited.PhoneNumber;
                user.Email = userEdited.Email;

                var updateduser = _context.userRepository.UpdateUserAvatar(user, userEdited);

                var result = await _userManager.UpdateAsync(updateduser);
                _context.SaveChangesDB();

                if (result.Succeeded) return Redirect("/Employee/Home/Index?Edit=true");


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(user);
            }

            return View();
        }

    }
}
