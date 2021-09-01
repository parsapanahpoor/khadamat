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
    public class RequestForCheckoutRepository : GenericRepository<RequestForCheckout>, IRequestForCheckoutRepository
    {
        private readonly KhadamatContext _db;

        public RequestForCheckoutRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddRequestForCheckout(RequestForCheckout checkout)
        {
            RequestForCheckout request = new RequestForCheckout()
            {
                RequestForCheckoutStatusID = 1,
                Price = checkout.Price,
                DateTime = DateTime.Now,
                CartBankNumber = checkout.CartBankNumber,
                OwnerBankCart = checkout.OwnerBankCart,
                EmployeeID = checkout.EmployeeID
            };

            Add(request);
        }

        public List<RequestForCheckout> GetAllEmployeeRequestForCheckout(string EmployeeID)
        {
            return GetAll(p => p.EmployeeID == EmployeeID).ToList();
        }

        public List<RequestForCheckout> GetAllNewRequests()
        {
            return GetAll(includeProperties: "User")
                            .Where(p=>p.RequestForCheckoutStatusID == 1).ToList();
        }

        public List<RequestForCheckout> GetAllRequestsForCheckout()
        {
            return GetAll(includeProperties:"User").ToList();
        }

        public RequestForCheckout GetRequestForCheckoutbyID(int id)
        {
            return GetById(id);
        }

        public int GetRequestForCheckoutStatusID(int id)
        {
            RequestForCheckout request =  GetById(id);

            return request.RequestForCheckoutStatusID;
        }

        public void UpdateRequestForCheckout(RequestForCheckout request)
        {
            Update(request);
        }
    }
}
