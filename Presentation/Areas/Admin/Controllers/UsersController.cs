using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Convertors;
using Utilities.Genarator;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _context;


        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        #region UsersManager
        #region Indexes
        public ActionResult Index(int? id, bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;


            var model = _userManager.Users
                        .Select(u => new IndexViewModel()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            UserAvatar = u.UserAvatar,
                            PhoneNumber = u.PhoneNumber,
                            IsActive = u.IsActive,
                        }).ToList();
            return View(model);
        }

        public async Task<ActionResult> EmployeeList(bool update = false, bool Delete = false)
        {

            var usersid = await _userManager.GetUsersInRoleAsync("Employee");
            var model = usersid
                        .Select(u => new IndexViewModel()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            UserAvatar = u.UserAvatar,
                            IsActive = u.IsActive,
                            IsAccepted = u.IsAccepted,
                            Status = u.EmployeeStatusID
                        }).ToList();

            if (update == true)
            {
                ViewBag.Update = true;
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }
            return View(model);
        }
        public async Task<ActionResult> UsersIndex(bool update = false, bool Delete = false)
        {
            var usersid = await _userManager.GetUsersInRoleAsync("User");
            var model = usersid
                        .Select(u => new IndexViewModel()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            UserAvatar = u.UserAvatar,
                            IsActive = u.IsActive,
                        }).ToList();

            if (update == true)
            {
                ViewBag.Update = true;
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }

            return View(model);
        }

        public async Task<IActionResult> AdminsIndex(bool update = false, bool Delete = false)
        {

            var usersid = await _userManager.GetUsersInRoleAsync("Admin");
            var model = usersid
                        .Select(u => new IndexViewModel()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            UserAvatar = u.UserAvatar,
                            IsActive = u.IsActive,
                        }).ToList();

            if (update == true)
            {
                ViewBag.Update = true;
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }

            return View(model);
        }
        public async Task<ActionResult> SupportersIndex(bool update = false, bool Delete = false)
        {

            var usersid = await _userManager.GetUsersInRoleAsync("Support");
            var model = usersid
                        .Select(u => new IndexViewModel()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            UserAvatar = u.UserAvatar,
                            IsActive = u.IsActive,

                        }).ToList();

            if (update == true)
            {
                ViewBag.Update = true;
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }

            return View(model);
        }
        #endregion



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserFromAdminPanel model)
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
                    UserAvatar = "Defult.jpg",
                    IsDelete = false,
                    IsAccepted = true,
                    ActiveCode = RandomNumberGenerator.GetNumber(),
                    EmployeeStatusID = 1

                };

                #region Save Avatar



                if (model.UserAvatar != null)
                {


                    user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(model.UserAvatar.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", user.UserAvatar);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        model.UserAvatar.CopyTo(stream);
                    }
                }


                #endregion


                var result = await _userManager.CreateAsync(user, model.Password);



                _context.SaveChangesDB();

                if (result.Succeeded)
                {
                    return Redirect("/Admin/Users/Index?Create=true");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        public async Task<ActionResult> Edit(string id, bool Delete = false, bool Employee = false
                           , bool UserPage = false, bool Admin = false, bool Supporter = false

            )
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);


            EditUserInAdminPanel editUser = new EditUserInAdminPanel()
            {

                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                AvatarName = user.UserAvatar,
                Status = user.EmployeeStatusID



            };
            if (user == null) return NotFound();


            if (Employee == true)
            {


                ViewBag.Employee = true;

            }
            if (UserPage == true)
            {

                ViewBag.UserPage = true;

            }
            if (Admin == true)
            {

                ViewBag.Admin = true;

            }
            if (Supporter == true)
            {

                ViewBag.Supporter = true;

            }
            if (Delete == true)
            {

                ViewData["Delete"] = true;

            }

            return View(editUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserInAdminPanel userEdited, bool Employee = false,
                     bool UserPage = false, bool Admin = false, bool Supporter = false

            )
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(userEdited.UserName)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.UserName = userEdited.UserName;
            user.PhoneNumber = userEdited.PhoneNumber;
            user.Email = userEdited.Email;
            user.EmployeeStatusID = userEdited.Status;


            var updateduser = _context.userRepository.UpdateUserAvatar(user, userEdited);


            var result = await _userManager.UpdateAsync(updateduser);
            _context.SaveChangesDB();


            if (result.Succeeded)
            {
                if (Employee == true)
                {
                    return Redirect("/Admin/Users/EmployeeList?Update=true");

                }
                if (UserPage == true)
                {
                    return Redirect("/Admin/Users/UsersIndex?Update=true");

                }
                if (Admin == true)
                {
                    return Redirect("/Admin/Users/AdminsIndex?Update=true");

                }
                if (Supporter == true)
                {
                    return Redirect("/Admin/Users/SupportersIndex?Update=true");

                }

                return Redirect("/Admin/Users/Index?Edit=true");

            }



            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }




        public async Task<IActionResult> Delete(string id, bool Employee = false, bool UserPage = false, bool Admin = false, bool Supporter = false)
        {
            var user = await _userManager.FindByIdAsync(id);

            user.IsDelete = true;

            var result = await _userManager.UpdateAsync(user);

            _context.userRepository.DeleteUserAvatar(user);

            _context.SaveChangesDB();

            if (Employee == true)
            {
                return Redirect("/Admin/Users/EmployeeList?Delete=true");

            }
            if (UserPage == true)
            {
                return Redirect("/Admin/Users/UsersIndex?Delete=true");

            }
            if (Admin == true)
            {
                return Redirect("/Admin/Users/AdminsIndex?Delete=true");

            }
            if (Supporter == true)
            {
                return Redirect("/Admin/Users/SupportersIndex?Delete=true");

            }

            return Redirect("/Admin/Users/Index?Delete=true");
        }

        public async Task<IActionResult> LockUser(string Userid, int id, bool Employee = false
               , bool UserPage = false, bool Admin = false, bool Supporter = false
            )
        {
            var user = await _userManager.FindByIdAsync(Userid);

            if (id == 1)
            {
                user.IsActive = false;

            }
            if (id == 2)
            {
                user.IsActive = true;

            }
            var result = await _userManager.UpdateAsync(user);
            if (Employee == true)
            {
                return RedirectToAction(nameof(EmployeeList));

            }
            if (UserPage == true)
            {
                return RedirectToAction(nameof(UsersIndex));

            }
            if (Admin == true)
            {
                return RedirectToAction(nameof(AdminsIndex));

            }
            if (Supporter == true)
            {
                return RedirectToAction(nameof(SupportersIndex));

            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AcceptkUser(string Userid, int id)
        {
            var user = await _userManager.FindByIdAsync(Userid);

            if (id == 1)
            {
                user.IsAccepted = false;
                var info = _context.employeeRepository.GetEmployeeDocument(Userid);
                if (info != null)
                {
                    info.PossitionId = 3;
                }

            }
            if (id == 2)
            {
                user.IsAccepted = true;
                var info = _context.employeeRepository.GetEmployeeDocument(Userid);
                if (info != null)
                {
                    info.PossitionId = 4;
                }

            }
            var result = await _userManager.UpdateAsync(user);
            _context.SaveChangesDB();
            return RedirectToAction(nameof(EmployeeList));
        }

        #endregion

        #region RolesManager

        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string id, bool Employee = false, bool UserPage = false, bool Admin = false, bool Supporter = false)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var roles = _roleManager.Roles.ToList();
            var model = new AddUserToRoleViewModel() { UserId = id };

            foreach (var role in roles)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.UserRoles.Add(new UserRolesViewModel()
                    {
                        RoleName = role.Name
                    });
                }
            }


            if (Employee == true)
            {


                ViewBag.Employee = true;

            }
            if (UserPage == true)
            {

                ViewBag.UserPage = true;

            }
            if (Admin == true)
            {

                ViewBag.Admin = true;

            }
            if (Supporter == true)
            {

                ViewBag.Supporter = true;

            }
          


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model, bool Employee = false, bool UserPage = false, bool Admin = false, bool Supporter = false)
        {
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestRoles = model.UserRoles.Where(r => r.IsSelected)
                .Select(u => u.RoleName)
                .ToList();
            var result = await _userManager.AddToRolesAsync(user, requestRoles);

            if (result.Succeeded)
            {

                if (Employee == true)
                {
                    return Redirect("/Admin/Users/EmployeeList?Update=true");

                }
                if (UserPage == true)
                {
                    return Redirect("/Admin/Users/UsersIndex?Update=true");

                }
                if (Admin == true)
                {
                    return Redirect("/Admin/Users/AdminsIndex?Update=true");

                }
                if (Supporter == true)
                {
                    return Redirect("/Admin/Users/SupportersIndex?Update=true");

                }

                return RedirectToAction("index");
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string id, bool Employee = false, bool UserPage = false, bool Admin = false, bool Supporter = false)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var roles = _roleManager.Roles.ToList();
            var model = new AddUserToRoleViewModel() { UserId = id };

            foreach (var role in roles)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.UserRoles.Add(new UserRolesViewModel()
                    {
                        RoleName = role.Name
                    });
                }
            }


            if (Employee == true)
            {


                ViewBag.Employee = true;

            }
            if (UserPage == true)
            {

                ViewBag.UserPage = true;

            }
            if (Admin == true)
            {

                ViewBag.Admin = true;

            }
            if (Supporter == true)
            {

                ViewBag.Supporter = true;

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(AddUserToRoleViewModel model, bool Employee = false, bool UserPage = false, bool Admin = false, bool Supporter = false)
        {
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestRoles = model.UserRoles.Where(r => r.IsSelected)
                .Select(u => u.RoleName)
                .ToList();
            var result = await _userManager.RemoveFromRolesAsync(user, requestRoles);


            if (result.Succeeded)
            {

                if (Employee == true)
                {
                    return Redirect("/Admin/Users/EmployeeList?Delete=true");

                }
                if (UserPage == true)
                {
                    return Redirect("/Admin/Users/UsersIndex?Delete=true");

                }
                if (Admin == true)
                {
                    return Redirect("/Admin/Users/AdminsIndex?Delete=true");

                }
                if (Supporter == true)
                {
                    return Redirect("/Admin/Users/SupportersIndex?Delete=true");

                }

                return RedirectToAction("index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        #endregion
    }
}
