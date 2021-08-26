using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public void AddTariff(Tariff tariff)
        {
            Tariff job = new Tariff()
            {
                ParentId = tariff.ParentId,
                TariffTitle = tariff.TariffTitle,
                IsDelete = false,
                TariffPercent = tariff.TariffPercent
            };

            Add(job);
        }

        public void DeleteTariff(Tariff tariff)
        {
            tariff.IsDelete = true;
            Update(tariff);
        }

        public List<Tariff> GetAllTariffes()
        {
            return GetAll().ToList();
        }

        public List<SelectListItem> GetMainTariffForCreateInvoicing()
        {
            return GetAll().Where(p=>p.ParentId == null)
                        .Select(g => new SelectListItem()
                        {
                            Text = g.TariffTitle,
                            Value = g.TariffId.ToString()
                        }).ToList();
        }

        public List<SelectListItem> GetSubTariffForCreateInvoicing(int Id)
        {
            return GetAll().Where(p => p.ParentId == Id)
                                   .Select(g => new SelectListItem()
                                   {
                                       Text = g.TariffTitle,
                                       Value = g.TariffId.ToString()
                                   }).ToList();
        }

        public Tariff GetTariffById(int id)
        {
            return GetAll(p => p.TariffId == id).First();
        }

        public void UpdateJobCaategory(Tariff tariff)
        {
            Update(tariff);
        }
    }
}
