using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Convertors;
using Utilities.Genarator;
using Utilities.Senders;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;
        private readonly IMessageSender _messageSender;
        private IViewRenderService _viewRender;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork context
            , IMessageSender messageSender, IViewRenderService viewRender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _messageSender = messageSender;
            _viewRender = viewRender;

        }
        #endregion

        #region User Registeration 

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
                    IsActive = true,
                    IsDelete = false,
                    ForgotPasswordCode = RandomNumberGenerator.GetNumber(),
                    ActiveCode = RandomNumberGenerator.GetNumber()

                };



                var result = await _userManager.CreateAsync(user, model.Password);

                _context.userProfileRepository.AddUserProfileAfterRegister(user.Id);
                _context.SaveChangesDB();
                List<string> requestRoles = new List<string>();
                requestRoles.Add("User");

                var reslt = await _userManager.AddToRolesAsync(user, requestRoles);


                if (result.Succeeded)
                {
                    return Redirect("/Home/Index?Register=true");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        #endregion

        #region Employee Registration 

        [Route("/EmploeeRegister")]
        public IActionResult EmploeeRegister()
        {
            return View();
        }
        [HttpPost]
        [Route("/EmploeeRegister")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmploeeRegister(RegisterViewModel model)
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
                    IsActive = false,
                    IsDelete = false,
                    ForgotPasswordCode = RandomNumberGenerator.GetNumber(),
                    ActiveCode = RandomNumberGenerator.GetNumber(),

                };



                var result = await _userManager.CreateAsync(user, model.Password);

                _context.userProfileRepository.AddUserProfileAfterRegister(user.Id);
                _context.SaveChangesDB();
                List<string> requestRoles = new List<string>();
                requestRoles.Add("Employee");

                var reslt = await _userManager.AddToRolesAsync(user, requestRoles);


                if (result.Succeeded)
                {
                    var emailMessage =
                      Url.Action("ConfirmEmail", "Account",
                          new { username = user.UserName, ActiveCode = user.ActiveCode },
                          Request.Scheme);

                    await _messageSender.SendEmailAsync(model.Email, "This is Code For Activation EmployeeRegistration ", emailMessage);


                    return Redirect("/Home/Index?EmployeeRegister=true");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        #endregion

        #region ConfirmEmail


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userName, string ActiveCode)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(ActiveCode))
                return NotFound();
            var user = await _userManager.FindByNameAsync(userName);
            if (user.ActiveCode == ActiveCode)
            {

                if (user == null) return NotFound();
                user.IsActive = true;
                user.ActiveCode = RandomNumberGenerator.GetNumber();
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Redirect("/Home/Index?ConfirmEmail=true");
                }
                else
                {
                    return NotFound();

                }

            }

            return NotFound();

        }

        #endregion

        #region Login

        [HttpGet]

        public IActionResult Login(string returnUrl = null, bool EditProfile = false, bool Register = false, bool recovery = false, bool permission = false)
        {



            ViewBag.EditProfile = EditProfile;
            ViewBag.permission = permission;
            ViewBag.Register = Register;
            ViewBag.recovery = recovery;

            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            ViewData["returnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (!user.IsActive)
                {
                    ModelState.AddModelError("", "حساب کاربری شما فعال نمی باشد");
                    return View(model);
                }


                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);




                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("/Home/Index?Login=true");

                }

                if (result.IsLockedOut)
                {
                    ViewData["ErrorMessage"] = "اکانت شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیق قفل شده است";
                    return View(model);
                }

                ModelState.AddModelError("", "رمزعبور یا نام کاربری اشتباه است");
            }
            return View(model);
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {

            return Redirect("/Account/Login?permission=true");

        }
        #endregion

        #region LogOut

        [Route("/LogOut")]

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region CheckUserRoleForLogin
        public IActionResult ManageUserForLogin()
        {

            //List<int> UserRoles = _userManager.role(User.Identity.Name);

            if (User.IsInRole("User"))
            {

                return Redirect("/User/Home/Index");

            }
            else
            {
                if (User.IsInRole("Admin"))
                {

                    return Redirect("/Admin/Home/Index");

                }
                else
                {
                    if (User.IsInRole("Writer"))
                    {
                        return Redirect("/Admin/Users/Index");
                    }
                    if (User.IsInRole("Accounter"))
                    {
                        return Redirect("/Admin/Users/Index");
                    }

                }



            }

            return View();

        }


        #endregion

        #region ForgotPassword
        [Route("/ForgotPassword")]
        public IActionResult ForgotPassword()
        {

            return View();
        }
        [Route("/ForgotPassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(String Email)
        {
            if (ModelState.IsValid)
            {
                if (Email == null)
                {
                    return NotFound();
                }

                var user = await _userManager.FindByEmailAsync(Email);

                if (user == null)
                {
                    ModelState.AddModelError("Email", " حساب کاربری یافت نشده است ");
                    return View();
                }

                if (!user.IsActive)
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است .");
                    return View();
                }


                #region Send ForgetPasswordCode Email

                string body = _viewRender.RenderToStringAsync("_ForgotPassword", user);
                SendEmail.Send(user.Email, "فراموشی رمز عبور ", body);

                #endregion

                return Redirect("/Account/ConfirmForgetPasswordCode?id=" + user.Id);


            }

            return View();
        }

        public async Task<IActionResult> ConfirmForgetPasswordCode(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            if (!user.IsActive)
            {
                ModelState.AddModelError("ForgotPasswordCode", "حساب کاربری شما فعال نشده است .");
                return View();
            }

            ViewBag.Id = id;
            ViewBag.username = user.UserName;

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmForgetPasswordCode(string id, string ForgotPasswordCode)
        {
            var user = await _userManager.FindByIdAsync(id);

            ViewBag.Id = id;
            ViewBag.username = user.UserName;

            if (ModelState.IsValid)
            {

                if (id == null)
                {
                    return NotFound();
                }
                if (ForgotPasswordCode == null)
                {
                    return NotFound();
                }




                if (user == null)
                {
                    return NotFound();
                }
                if (!user.IsActive)
                {
                    ModelState.AddModelError("ForgotPasswordCode", "حساب کاربری شما فعال نشده است .");
                    return View();
                }

                if (user.ForgotPasswordCode == ForgotPasswordCode)
                {
                    return Redirect("/Account/RecoveryPassword?id=" + user.Id);

                }
                else
                {
                    ModelState.AddModelError("ForgotPasswordCode", "کد وارد شده معتبر نمی باشد ");

                    return View();
                }

            }



            return View(user);
        }

        public IActionResult RecoveryPassword(string id)

        {
            return View(new RecoverPasswordViewModel()
            {

                Userid = id
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> RecoveryPassword(RecoverPasswordViewModel recovery)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(recovery.Userid);
                if (user == null)
                {
                    return NotFound();
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = _userManager.ResetPasswordAsync(user, token, recovery.Password);



                _context.SaveChangesDB();




                return Redirect("/Home/Index?ForgotPassword=true");

            }


            return View(recovery);
        }

        #endregion
    }
}
