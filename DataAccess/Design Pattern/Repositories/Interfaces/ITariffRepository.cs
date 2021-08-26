using DataAccess.Design_Pattern.GenericRepositories;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        List<Tariff> GetAllTariffes();
        void AddTariff(Tariff tariff);
        Tariff GetTariffById(int id);
        void UpdateJobCaategory(Tariff tariff);
        void DeleteTariff(Tariff tariff);
        List<SelectListItem> GetMainTariffForCreateInvoicing();
        List<SelectListItem> GetSubTariffForCreateInvoicing(int Id);
    }
}
