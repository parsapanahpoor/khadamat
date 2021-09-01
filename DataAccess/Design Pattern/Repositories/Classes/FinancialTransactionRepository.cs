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

        public void AddFinancialTransactionForCashPaymentToEmployeeFromUser(Invoicing invoicing, decimal price)
        {
            FinancialTrnsaction financial = new FinancialTrnsaction()
            {
                FinancialTransactionStatusID = 2,
                InvoicingId = invoicing.InvoicingID,
                Price = price,
                EmployeeID = invoicing.EmployeeID,
                UserID = invoicing.EmployeeID,
                Description = "پزداخت نقدی مبلغ خدمت از مشتری به خدمت رسان ",
                DateTime = DateTime.Now
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
                UserID = invoicing.EmployeeID,
                Description = "پزداخت آنلاین مبلغ خدمت از مشتری به حساب خدمت رسان",
                DateTime = DateTime.Now
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
                Description = "واریز مبلغ درخواستی از شرکت به حصاب خدمت رسان  "
            };

            Add(financial);
        }
    }
}
