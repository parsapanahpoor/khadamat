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
    public interface IDataReservationRepository : IGernericRepository<DataReservation>
    {
        List<DataReservation> GetListOfEmployeeDataReservation(string userid);
        List<DataReservation> GetListOfEmployeeDataReservationHistory(string userid);
        void AddDataReservationFromEmployeePanel(DateTime date , string userid);
        DataReservation GetDataReservationById(int id);
        void UpdateDateReservationFromEmployeePanel(DataReservation data);
        void DeleteDateReservation(DataReservation data);
        List<DataReservation> GetDateReservationByEmployeeId(string EmployeeID);
    }
}
