using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class FinancialTransactionRepository : GenericRepository<FinancialTrnsaction>, IFinancialTransactionRepository
    {
        private readonly KhadamatContext _db;

        public FinancialTransactionRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public FinancialTrnsaction AddFinancialForPaymentFromEmployeeToTheCompanyAccountOnline(decimal Price, string EmployeeID, string EmployeeName)
        {
            FinancialTrnsaction financial = new FinancialTrnsaction()
            {
                FinancialTransactionStatusID = 4,
                Price = Price,
                EmployeeID = EmployeeID,
                ReciverPerson = "شرکت ",
                DepositeFromPerson = EmployeeName,
                Description = "تسویه حساب بدهکاری خدمت رسان به صورت آنلاین به حساب شرکت ",
                DateTime = DateTime.Now,
                IsActiveForEmployeePay = false
            };
            Add(financial);

            return financial;
        }

        public void AddFinancialTransactionForCashPaymentToEmployeeFromUser(Invoicing invoicing, decimal price)
        {
            FinancialTrnsaction financial = new FinancialTrnsaction()
            {
                FinancialTransactionStatusID = 2,
                InvoicingId = invoicing.InvoicingID,
                Price = price,
                EmployeeID = invoicing.EmployeeID,
                UserID = invoicing.UserID,
                Description = "پزداخت نقدی مبلغ خدمت از مشتری به خدمت رسان ",
                DateTime = DateTime.Now,
                IsActiveForEmployeePay = true
            };

            Add(financial);
        }

        public void AddFinancialTransactionForOnlinePaymentToEmployeeFromUser(Invoicing invoicing, decimal price)
        {
            FinancialTrnsaction financial = new FinancialTrnsaction()
            {
                FinancialTransactionStatusID = 1,
                InvoicingId = invoicing.InvoicingID,
                Price = price,
                UserID = invoicing.UserID,
                EmployeeID = invoicing.EmployeeID,
                Description = "پزداخت آنلاین مبلغ خدمت از مشتری به حساب خدمت رسان",
                DateTime = DateTime.Now,
                IsActiveForEmployeePay = true
            };

            Add(financial);
        }

        public void CheckoutWhitEmployeeAfterHisRequest(decimal price, string EmployeeID)
        {
            FinancialTrnsaction financial = new FinancialTrnsaction()
            {
                FinancialTransactionStatusID = 3,
                Price = price,
                EmployeeID = EmployeeID,
                DepositeFromPerson = "حساب شرکت ",
                DateTime = DateTime.Now,
                Description = "واریز مبلغ درخواستی از شرکت به حصاب خدمت رسان  ",
                IsActiveForEmployeePay = true
            };

            Add(financial);
        }

        public List<FinancialTrnsaction> GetAllEmployeeFinancialTransaction(string EmployeeID)
        {
            return GetAll(includeProperties:"User")
                        .Where(p => p.EmployeeID == EmployeeID && p.IsActiveForEmployeePay == true).ToList();
        }

        public List<FinancialTrnsaction> GetAllEmployeePaymentTotheCompanyAccount(string EmployeeID)
        {
            return GetAll(p => p.EmployeeID == EmployeeID && p.FinancialTransactionStatusID == 4 && p.IsActiveForEmployeePay == true)
                                    .ToList();
        }

        public FinancialTrnsaction GetFinancialTransactionByID(int id)
        {
            return GetById(id);
        }

        public List<FinancialTrnsaction> GetListOFUserFinacialTransaction(string Userid)
        {
            return GetAll(includeProperties:"User").Where(p => p.UserID == Userid)
                                               .ToList();
        }

        public void UpdateFinancialTransaction(FinancialTrnsaction financial)
        {
            Update(financial);
        }
    }
}
