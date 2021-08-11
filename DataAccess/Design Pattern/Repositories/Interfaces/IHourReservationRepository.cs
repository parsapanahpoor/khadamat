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
    public interface IHourReservationRepository : IGernericRepository<HourReservation>
    {

        bool AddHourReservationFromEmployeePanel(AddHourReservationFromEmployeeVM addHourReservation , string EmployeeId);
        List<HourReservation> GetEmployeeHourReservationByDateHourReservationID(int id);
        HourReservation GetHourReservation(int id);
        bool UpdateHourReservationFromEmployee(HourReservation hourReservation);
        void DeleteHourReservationFromEmployeePanel(HourReservation hourReservation);
        bool IsExistHoureReservationWhiteDateReservationID(int id);
        HourReservation GetHoureReservationByID(int id);
        List<HourReservation> GetTodayEmployeeHoureReservation(string EmployeeID);
        void UpdateHourReservationAfterDeleteReservationOrder(HourReservation hour);
        List<HourReservation> GetJustTodayEmployeeHourReservationByEmployeeId(string EmployeeID);
        bool ISEmployeeHaveHObTightNow(List<HourReservation> List);
        HourReservation AddHourReservationWhileOnlineProccess(string employID , int DateID);

    }
}
