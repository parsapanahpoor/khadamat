using DataAccess.Design_Pattern.GenericRepositories;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public interface IRequestForCheckoutRepository : IGernericRepository<RequestForCheckout>
    {
        List<RequestForCheckout> GetAllEmployeeRequestForCheckout(string EmployeeID);
        void AddRequestForCheckout(RequestForCheckout checkout );
        List<RequestForCheckout> GetAllRequestsForCheckout();
        List<RequestForCheckout> GetAllNewRequests();
        int GetRequestForCheckoutStatusID(int id );
        RequestForCheckout GetRequestForCheckoutbyID(int id);
        void UpdateRequestForCheckout(RequestForCheckout request);
    }
}
