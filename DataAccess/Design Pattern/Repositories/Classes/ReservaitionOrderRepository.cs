using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataAccess.ViewModels;
using DataContext.Context;
using Models.Entities.EmployeeReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class ReservaitionOrderRepository : GenericRepository<ReservationOrder>, IReservationOrderRepository
    {
        private readonly KhadamatContext _db;

        public ReservaitionOrderRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }


        public ReservationOrder AddRservationOrderFromSession(EmployeeReservationViewModel reservationOrder)
        {
            ReservationOrder reserve = new ReservationOrder()
            {
                UserReservationStatus = reservationOrder.UserReservationStatus,
                EmployeeID = reservationOrder.EmployeeID,
                UserID = reservationOrder.UserID,
                JobCategoryID = reservationOrder.JobCategoryID,
                LocationID = (int)reservationOrder.LocationID,
                HoureReservationID = reservationOrder.HoureReservationID,
                DateReservationID = reservationOrder.DateReservationID,
                DateTimeReservation = reservationOrder.DateTimeReservation,
                Description = reservationOrder.Description
            };

            Add(reserve);
            return reserve;
        }
    }
}
