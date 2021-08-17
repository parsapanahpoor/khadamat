using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.ViewModels;
using Models.Entities.EmployeeReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public interface IReservationOrderRepository : IGernericRepository<ReservationOrder>
    {
        ReservationOrder AddRservationOrderFromSession(EmployeeReservationViewModel reservationOrder);
        ReservationOrder GetReservationOrderById(int id);
        List<ReservationOrder> GetUserNetReservationOrderForShowInUserPanel(string userid);
        List<ReservationOrder> GetUserLaterReservationOrderForShowInUserPanel(string userid);
        ReservationOrder GetReservationOrderByHourReservationId(int id );
        List<ReservationOrder> GetTodayEmployeeReservationOrder(string EmployeeID);
        List<ReservationOrder> GetTodayUserReservationOrder(string userid);
        List<ReservationOrder> GetAllTodayReservationOrder();
        List<string> GetAllEmployeeIDHaveReservationToday();
        List<string> GetAllEmployeeIDHaveDeletedReservationToday();
        List<string> GetAllEmployeeID();
        void DeleteReservationOrder(ReservationOrder reservation);
        List<ReservationOrder> GetAllUserReservationOrderByUserid(string userid);
        List<ReservationOrder> GetAllReservationOrder( );
        List<ReservationOrder> GetAllDeletedReservationOrder( );
        void UpdateReservationOrder(ReservationOrder reservation);
        ReservationOrder GetReservationOrderByHourReservationID(int HourID);
        List<ReservationOrder> GetTodayDeletedReservationOrder( );
    }
}
