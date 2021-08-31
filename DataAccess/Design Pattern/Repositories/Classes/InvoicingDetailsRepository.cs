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
    public class InvoicingDetailsRepository : GenericRepository<InvoicingDetail>, IInvoicingDetailsRepository
    {
        private readonly KhadamatContext _db;

        public InvoicingDetailsRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddInvoicingDetailFromEmployeePanel(int InvoicingId, decimal Price, string Description = "")
        {
            InvoicingDetail detail = new InvoicingDetail()
            {
                InvoicingID = InvoicingId,
                Price = (double)Price
            };
            if (Description != "")
            {
                detail.Description = Description;
            }

            Add(detail);
        }

        public void AddInvoicingDetailFromEmployeePanelByPercent(int InvoicingId, int TariffID, int percent, decimal Price, string Description = "")
        {
            InvoicingDetail detail = new InvoicingDetail()
            {
                InvoicingID = InvoicingId,
                Price = (double)Price,
                TariffID = TariffID,
                PerCent = percent
            };
            if (Description != "")
            {
                detail.Description = Description;
            }
            
            Add(detail);
        }

        public void DeleteInvoicingDetailSoftDelete(InvoicingDetail invoicingDetail)
        {
            Delete(invoicingDetail);
        }

        public decimal GetAdminPercerntFromInvoicing(int invoicingID)
        {
            IEnumerable<InvoicingDetail> List = GetAll(p => p.InvoicingID == invoicingID);

            decimal AdminPercent = 0;

            foreach (var item in List)
            {
                float comison = (float)item.PerCent;
                float darsad = (float)100;

                var per = comison / darsad;

                var AdminComision = item.Price * (per);

                AdminPercent = (decimal)((float)AdminPercent + (float)AdminComision);
            }

            return AdminPercent;

        }

        public decimal GetEmployeePercentFromInvoicing(int invoicingID)
        {
            IEnumerable<InvoicingDetail> List = GetAll(p => p.InvoicingID == invoicingID);

            decimal EmployeePercent = 0;

            foreach (var item in List)
            {
                float comison = (float)item.PerCent;
                float darsad = (float)100;

                var per = comison / darsad;

                var AdminComision = item.Price * (per);
                var SellerComision = item.Price - AdminComision;

                EmployeePercent = (decimal)((float)EmployeePercent + (float)SellerComision);
            }

            return EmployeePercent;
        }

        public decimal GetFullPriceFromInvoicing(int invoicingID)
        {
            IEnumerable<InvoicingDetail> List = GetAll(p => p.InvoicingID == invoicingID);

            decimal Sum = 0;

            foreach (var item in List)
            {
                Sum = Sum + (decimal)item.Price;
            }

            return Sum;
        }

        public InvoicingDetail GetInvoicingDetailByID(int id)
        {
            return GetById(id);
        }

        public List<InvoicingDetail> GetListOfInvoicingDetailByInvoicingId(int InvoicingID)
        {
            return GetAll(includeProperties: "Tariff")
                    .Where(p => p.InvoicingID == InvoicingID).ToList();
        }

        public void UpdateInvoicingDetail(InvoicingDetail invoicingDetail)
        {
            Update(invoicingDetail);
        }
    }
}
