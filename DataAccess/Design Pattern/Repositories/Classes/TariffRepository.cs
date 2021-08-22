using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class TariffRepository : GenericRepository<Tariff>, ITariffRepository
    {
        private readonly KhadamatContext _db;

        public TariffRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }
    }
}
