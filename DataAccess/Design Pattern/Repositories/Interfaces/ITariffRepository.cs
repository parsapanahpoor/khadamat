using DataAccess.Design_Pattern.GenericRepositories;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public  interface ITariffRepository : IGernericRepository<Tariff>
    {
    }
}
