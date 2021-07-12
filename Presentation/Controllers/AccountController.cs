using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        #region Constructor
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly IUnitOfWork _context;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager/*, IUnitOfWork context*/)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_context = context;
        }
        #endregion

        #region Register

        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("/Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {



                if (_context.userRepository.IsExistUserName(model.UserName))
                {
                    ModelState.AddModelError("", "این نام کاربری توسط فرد دیگری انتخاب شده است ");
                    return View(model);
                }
                if (_context.userRepository.IsExistEmail(FixedText.FixEmail(model.Email)))
                {
                    ModelState.AddModelError("", "این ایمیل  توسط فرد دیگری انتخاب شده است");
                    return View(model);
                }
                if (_context.userRepository.IsExistPhoneNumber(FixedText.FixEmail(model.PhoneNumber)))
                {
                    ModelState.AddModelError("PhoneNumber", "شماره تلفن وارد شده توسط فرد دیگری انتخاب شده است  ");
                    return View(model);
                }
                var user = new User()
                {
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    Email = FixedText.FixEmail(model.Email),
                    EmailConfirmed = true,
                    RegisterDate = DateTime.Now,
                    UserAvatar = "Defult.jpg",
                    IsActive = true,
                    IsDelete = false,
                    ActiveCode = RandomNumberGenerator.GetNumber(),

                };

                var result = await _userManager.CreateAsync(user, model.Password);

                List<string> requestRoles = new List<string>();
                requestRoles.Add("User");

                var reslt = await _userManager.AddToRolesAsync(user, requestRoles);


                if (result.Succeeded)
                {
                    return Redirect("/Login?Register=true");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }


        #endregion

    }
}
