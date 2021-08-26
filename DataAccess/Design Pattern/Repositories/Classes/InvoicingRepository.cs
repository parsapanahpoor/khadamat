using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataAccess.ViewModels;
using DataContext.Context;
using Models.Entities.EmployeeReservation;
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

        public void AddInvoicingByReservationOrderInformations(ReservationOrder reservation, FirstStepForInvoicing first)
        {
            Invoicing invoicing = new Invoicing()
            {
                ReservationOrderID = reservation.ReservationOrderID,
                PaymentMethodID = first.PaymentMethodId,
                EmployeeID = reservation.EmployeeID,
                UserID = reservation.UserID,
                JobCategoryID = reservation.JobCategoryID,
                HoureReservationID = reservation.HoureReservationID,
                DateReservationID = reservation.DateReservationID,
                DateTime = DateTime.Now,
                Description = first.Description,
                IsDelete = false,
                IsFinally = false,
                IsOpen = true
            };

            Add(invoicing);
        }

        public Invoicing GetInvoicingByID(int id)
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory,ReservationOrder")
                            .FirstOrDefault(p=>p.InvoicingID == id);
        }
    }
}
