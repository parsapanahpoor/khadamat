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
    public class DataReservationRepository : GenericRepository<DataReservation>, IDataReservationRepository
    {
        private readonly KhadamatContext _db;

        public DataReservationRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddDataReservationFromEmployeePanel(DateTime date, string userid)
        {
            DataReservation dataReservation = new DataReservation()
            {
                EmployeeID = userid,
                ReservationDateTime = (DateTime)date
            };

            Add(dataReservation);
        }

        public void DeleteDateReservation(DataReservation data)
        {
            Delete(data);
        }

        public DataReservation GetDataReservationById(int id)
        {
            return GetAll(p => p.DataReservationID == id).First();
        }

        public List<DataReservation> GetListOfEmployeeDataReservation(string userid)
        {
            return GetAll(includeProperties: "HourReservation")
                        .OrderBy(p => p.DataReservationID).ToList();
        }

        public void UpdateDateReservationFromEmployeePanel(DataReservation data)
        {
            Update(data);
        }
    }
}
