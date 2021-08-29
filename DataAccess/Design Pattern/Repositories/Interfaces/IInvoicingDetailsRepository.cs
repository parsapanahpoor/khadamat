using DataAccess.Design_Pattern.GenericRepositories;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public interface IInvoicingDetailsRepository : IGernericRepository<InvoicingDetail>
    {
        void AddInvoicingDetailFromEmployeePanel( int InvoicingId , decimal Price, string Description = "");
        void AddInvoicingDetailFromEmployeePanelByPercent( int InvoicingId , int TariffID, int percent ,  decimal Price, string Description = "");
        List<InvoicingDetail> GetListOfInvoicingDetailByInvoicingId(int InvoicingID);
        InvoicingDetail GetInvoicingDetailByID(int id);
        void DeleteInvoicingDetailSoftDelete(InvoicingDetail invoicingDetail);
        void UpdateInvoicingDetail(InvoicingDetail invoicingDetail);
    }
}
