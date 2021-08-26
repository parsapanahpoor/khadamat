using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataAccess.ViewModels;
using DataContext.Context;
using Models.Entities.EmployeeReservation;
using Models.Entities.User;
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

        public void DeleteReservationOrder(ReservationOrder reservation)
        {
            Delete(reservation);
        }

        public List<string> GetAllEmployeeID()
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory")
                                                                                   .Select(p => p.EmployeeID).ToList();
        }

        public List<string> GetAllEmployeeIDHaveReservationToday()
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory")
                                                     .Where(p => p.DataReservation.ReservationDateTime.Year == DateTime.Now.Year
                                                                && p.DataReservation.ReservationDateTime.Month == DateTime.Now.Month
                                                                 && p.DataReservation.ReservationDateTime.Day == DateTime.Now.Day)
                                                                        .Select(p => p.EmployeeID).ToList();
        }
        public List<string> GetAllEmployeeIDHaveDeletedReservationToday()
        {
            return GetAllIgnorQueryFilter(includeProperties: "User,DataReservation,HourReservation,JobCategory")
                                                     .Where(p => p.DataReservation.ReservationDateTime.Year == DateTime.Now.Year
                                                                && p.DataReservation.ReservationDateTime.Month == DateTime.Now.Month
                                                                && p.DataReservation.ReservationDateTime.Day == DateTime.Now.Day
                                                                && p.IsDelete == true)
                                                                .Select(p => p.EmployeeID).ToList();
        }

        public List<ReservationOrder> GetAllReservationOrder()
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory")
                                                                .ToList();
        }
        public List<ReservationOrder> GetAllDeletedReservationOrder()
        {
            return GetAllIgnorQueryFilter(includeProperties: "User,DataReservation,HourReservation,JobCategory")
                                                    .Where(p=>p.IsDelete == true).ToList();
        }
        public List<ReservationOrder> GetAllTodayReservationOrder()
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory")
                                          .Where(p => p.DataReservation.ReservationDateTime.Year == DateTime.Now.Year
                                                     && p.DataReservation.ReservationDateTime.Month == DateTime.Now.Month
                                                      && p.DataReservation.ReservationDateTime.Day == DateTime.Now.Day).ToList();
        }

        public List<ReservationOrder> GetAllUserReservationOrderByUserid(string userid)
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory,Location")
                                        .Where(p => p.UserID == userid).ToList();
        }

        public ReservationOrder GetReservationOrderByHourReservationId(int id)
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory,Location")
                                .FirstOrDefault(p => p.HoureReservationID == id);
        }

        public ReservationOrder GetReservationOrderByHourReservationID(int HourID)
        {
            return GetAll(p => p.HoureReservationID == HourID).First();
        }

        public ReservationOrder GetReservationOrderById(int id)
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory,Location")
                                .FirstOrDefault(p => p.ReservationOrderID == id);

        }

        public List<ReservationOrder> GetTodayDeletedReservationOrder()
        {
            return GetAllIgnorQueryFilter(includeProperties: "User,DataReservation,JobCategory")
                                        .Where(p => p.DateTimeReservation.Year == DateTime.Now.Year
                                               && p.DateTimeReservation.Month == DateTime.Now.Month
                                               && p.DateTimeReservation.Day == DateTime.Now.Day
                                               && p.IsDelete == true).ToList();
        }

        public List<ReservationOrder> GetTodayEmployeeDeletedReservationOrder(string EmployeeID)
        {
            return GetAllIgnorQueryFilter(includeProperties: "User,DataReservation,JobCategory")
                                        .Where(p => p.DateTimeReservation.Year == DateTime.Now.Year
                                               && p.DateTimeReservation.Month == DateTime.Now.Month
                                               && p.DateTimeReservation.Day == DateTime.Now.Day
                                               && p.IsDelete == true
                                               && p.EmployeeID == EmployeeID).ToList();
        }
        public List<ReservationOrder> GetTodayEmployeeReservationOrder(string EmployeeID)
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory")
                                .Where(p => p.EmployeeID == EmployeeID && p.DataReservation.ReservationDateTime.Year == DateTime.Now.Year
                                           && p.DataReservation.ReservationDateTime.Month == DateTime.Now.Month
                                            && p.DataReservation.ReservationDateTime.Day == DateTime.Now.Day).ToList();

        }

        public List<ReservationOrder> GetTodayUserReservationOrder(string userid)
        {
            return GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory")
                                .Where(p => p.UserID == userid && p.DataReservation.ReservationDateTime.Year == DateTime.Now.Year
                                           && p.DataReservation.ReservationDateTime.Month == DateTime.Now.Month
                                            && p.DataReservation.ReservationDateTime.Day == DateTime.Now.Day).ToList();
        }

        public List<ReservationOrder> GetUserLaterReservationOrderForShowInUserPanel(string userid)
        {
            List<ReservationOrder> ReservationList = new List<ReservationOrder>();

            IEnumerable<ReservationOrder> AllReservs = GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory")
                .Where(p => p.UserID == userid);

            foreach (var item in AllReservs)
            {
                if (item.UserReservationStatus == 2 && item.DataReservation.ReservationDateTime < DateTime.Now)
                {
                    ReservationList.Add(item);
                }
                if (item.UserReservationStatus == 1 && item.DateTimeReservation < DateTime.Now)
                {
                    ReservationList.Add(item);
                }
            }


            return ReservationList.ToList();
        }

        public List<ReservationOrder> GetUserNetReservationOrderForShowInUserPanel(string userid)
        {
            List<ReservationOrder> ReservationList = new List<ReservationOrder>();

            IEnumerable<ReservationOrder> AllReservs = GetAll(includeProperties: "User,DataReservation,HourReservation,JobCategory")
                .Where(p => p.UserID == userid);

            foreach (var item in AllReservs)
            {
                if (item.UserReservationStatus == 2 && item.DataReservation.ReservationDateTime.Year >= DateTime.Now.Year &&
                   item.DataReservation.ReservationDateTime.Month >= DateTime.Now.Month &&
                        item.DataReservation.ReservationDateTime.Day >= DateTime.Now.Day)
                {
                    ReservationList.Add(item);
                }
                if (item.UserReservationStatus == 1 && item.DataReservation.ReservationDateTime.Year >= DateTime.Now.Year &&
                   item.DataReservation.ReservationDateTime.Month >= DateTime.Now.Month &&
                        item.DataReservation.ReservationDateTime.Day >= DateTime.Now.Day)
                {
                    ReservationList.Add(item);
                }
            }


            return ReservationList.ToList();

        }

        public void UpdateReservationOrder(ReservationOrder reservation)
        {
            Update(reservation);
        }

        public bool IsExistRservationOrderWithHourReservationID(int HourID)
        {
            return GetAll(p => p.HoureReservationID == HourID).Any();
        }

        public void EndOfReservationOredr(ReservationOrder reservation)
        {
            reservation.IsEnd = true;
            reservation.EndDateTimeReservation = DateTime.Now;

            Update(reservation);
        }
    }
}
