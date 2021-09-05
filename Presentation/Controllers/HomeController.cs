using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Models.Entities.Factor;
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

        [Route("OnlinePayment/{id}")]
        public IActionResult onlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                Invoicing invoicing = _context.invoicingRepository.GetInvoicingByID(id);
                List<InvoicingDetail> invoicingDetails = _context.invoicingDetailsRepository.GetListOfInvoicingDetailByInvoicingId(id);

                int Amount = 0;

                foreach (var item in invoicingDetails)
                {
                    Amount = Amount + (int)item.Price;
                }
                //در این مرحله باید مبلغی که به صورت آنلاین پرداخت شده به حساب ها وارد شود 

                var payment = new ZarinpalSandbox.Payment(Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    invoicing.IsPay = true;
                    _context.invoicingRepository.UpdateInvoicing(invoicing);
                    _context.SaveChangesDB();
                }
            }

            return View();
        }

        [Route("PaymentFactorPercent/{id}")]
        public IActionResult PaymentFactorPercent(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
          HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
          && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                FinancialTrnsaction financial = _context.FinancialTransactionRepository.GetFinancialTransactionByID(id);

                int Amount = (int)financial.Price;

                var payment = new ZarinpalSandbox.Payment(Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    financial.IsActiveForEmployeePay = true;
                    _context.FinancialTransactionRepository.UpdateFinancialTransaction(financial);

                    //مدیریت مالی و تسویه حساب با خدمت رسان 

                    if (_context.AdminWalletRepository.IsExistAdminWallet())
                    {
                        _context.AdminWalletRepository.PaymentToCompanyPercentFromEmployeeOnlineToTheCompanyAccount(financial.Price);
                    }
                    if (!_context.AdminWalletRepository.IsExistAdminWallet())
                    {
                        _context.AdminWalletRepository.AddAdminWallet();
                        _context.SaveChangesDB();
                        _context.AdminWalletRepository.PaymentToCompanyPercentFromEmployeeOnlineToTheCompanyAccount(financial.Price);
                    }

                    if (_context.EmployeeWalletRepository.IsExistEmployeeWallet(financial.EmployeeID))
                    {
                        _context.EmployeeWalletRepository.PaymentToCompanyPercentFromEmployeeOnlineToTheCompanyAccount(financial.EmployeeID, financial.Price);
                    }
                    else
                    {
                        return View("~/Views/Shared/_404.cshtml");
                    }

                    _context.SaveChangesDB();
                }
            }

            return View();
        }
    }
}
