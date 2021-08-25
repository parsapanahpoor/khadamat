using DataAccess.Design_Pattern.GenericRepositories;
using DataContext.Context;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class InvoicingRepository : GenericRepository<Invoicing>, IInvoicingRepository
    {
        private readonly KhadamatContext _db;

        public InvoicingRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }
    }
}
