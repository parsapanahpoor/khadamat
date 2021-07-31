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
    public class HourReservationRepository : GenericRepository<HourReservation>, IHourReservationRepository
    {
        private readonly KhadamatContext _db;

        public HourReservationRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public bool AddHourReservationFromEmployeePanel(AddHourReservationFromEmployeeVM addHourReservation , string EmployeeId)
        {
            string StartHourString = addHourReservation.StartHour.Remove(2, 1);
            string EndHourString = addHourReservation.EndHour.Remove(2, 1);

            int StartHourInt = Convert.ToInt32(StartHourString);
            int EndtHourInt = Convert.ToInt32(EndHourString);

            int WorkTime = Math.Abs(EndtHourInt - StartHourInt);

            if (WorkTime >= 200 && WorkTime <= 2200)
            {
                HourReservation hour = new HourReservation()
                {
                    StartHourReservation = addHourReservation.StartHour,
                    EndHourReservation = addHourReservation.EndHour, 
                    Description = null , 
                    ReservationStatusID = 2 , 
                    DataReservationID = addHourReservation.DataReservationID,
                    EmployeeID = EmployeeId,
                    StartHourReservationInt = StartHourInt,
                    EndHourReservationInt = EndtHourInt
                };
                Add(hour);
                return true;
            }

            else
            {
                return false;
            }

        }

        public void DeleteHourReservationFromEmployeePanel(HourReservation hourReservation)
        {
            if (hourReservation.ReservationStatusID == 2)
            {
                Delete(hourReservation);
            }
        }

        public List<HourReservation> GetEmployeeHourReservationByDateHourReservationID(int id)
        {
            return GetAll(includeProperties: "ReservationStatus")
                            .Where(p=>p.DataReservationID == id).ToList();
                        
        }

        public HourReservation GetHourReservation(int id)
        {
            return GetById(id);
        }

        public bool UpdateHourReservationFromEmployee(HourReservation hourReservation)
        {
            string StartHourString = hourReservation.StartHourReservation.Remove(2, 1);
            string EndHourString = hourReservation.EndHourReservation.Remove(2, 1);

            int StartHourInt = Convert.ToInt32(StartHourString);
            int EndtHourInt = Convert.ToInt32(EndHourString);

            int WorkTime = Math.Abs(EndtHourInt - StartHourInt);

            if (WorkTime >= 200 && WorkTime <= 2200)
            {
                hourReservation.StartHourReservationInt = StartHourInt;
                hourReservation.EndHourReservationInt = EndtHourInt ;

                Update(hourReservation);
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
