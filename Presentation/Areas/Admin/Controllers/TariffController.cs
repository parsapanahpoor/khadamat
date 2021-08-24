using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TariffController : Controller
    {
        private readonly IUnitOfWork _context;
        public TariffController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;

            return View(_context.tariffRepository.GetAllTariffes());
        }

        public IActionResult Create(int? id)
        {

            return View(new Tariff()
            {
                ParentId = id
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tariff tariff)
        {
            if (ModelState.IsValid)
            {
                _context.tariffRepository.AddTariff(tariff);
                _context.SaveChangesDB();

                return Redirect("/Admin/Tariff/Index?Create=true");
            }
            return View(tariff);
        }

        public IActionResult Edit(int? id, bool Delete = false, bool Level3 = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tariff = _context.tariffRepository.GetTariffById((int)id);
            if (Tariff == null)
            {
                return NotFound();
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }
            if (Level3 == true)
            {
                ViewBag.Level3 = true;
            }

            return View(Tariff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tariff tariff)
        {

            if (ModelState.IsValid)
            {

                _context.tariffRepository.UpdateJobCaategory(tariff);
                _context.SaveChangesDB();

                return Redirect("/Admin/Tariff/Index?Edit=true");
            }
            return View(tariff);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/_404.cshtml");

            }
            var Tarif = _context.tariffRepository.GetTariffById((int)id);
            if (Tarif == null)
            {
                return View("~/Views/Shared/_404.cshtml");

            }

            _context.tariffRepository.DeleteTariff(Tarif);
            _context.SaveChangesDB();

            return Redirect("/Admin/Tariff/Index?Delete=true");
        }

    }
}
