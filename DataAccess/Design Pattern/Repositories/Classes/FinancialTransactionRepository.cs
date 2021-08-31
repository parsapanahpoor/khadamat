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

        public void AddFinancialTransaction(Invoicing invoicing, decimal price)
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
    }
}
