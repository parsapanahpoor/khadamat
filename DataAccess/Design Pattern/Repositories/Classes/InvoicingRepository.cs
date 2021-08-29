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

        public Invoicing AddInvoicingByReservationOrderInformations(ReservationOrder reservation, FirstStepForInvoicing first)
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
                IsOpen = true,
                LocationID = reservation.LocationID
            };
            if (first.PaymentMethodId == 1)
            {
                invoicing.IsPay = true;
            }
            else
            {
                invoicing.IsPay = false;
            }

            Add(invoicing);

            return invoicing;
        }

        public void CloseInvoicingFromEmployeePanel(Invoicing invoicing)
        {
            invoicing.IsOpen = false;

            Update(invoicing);
        }

        public Invoicing GetInvoicingByID(int id)
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory,ReservationOrder,Location")
                            .FirstOrDefault(p => p.InvoicingID == id);
        }

        public Invoicing GetInvoicingByReservationOrderID(int reservationID)
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory,ReservationOrder,Location")
                            .FirstOrDefault(p => p.ReservationOrderID == reservationID);
        }

        public bool ISExistInvoicingWithHourReservationID(int id)
        {
            return GetAll().Any(p => p.HoureReservationID == id);
        }

        public bool IsInvoicingFinallyByHourID(int HourID)
        {
            Invoicing invoicing = GetAll(p => p.HoureReservationID == HourID).Single();
            if (invoicing.IsFinally == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateInvoicing(Invoicing invoicing)
        {
            Update(invoicing);
        }
    }
}
