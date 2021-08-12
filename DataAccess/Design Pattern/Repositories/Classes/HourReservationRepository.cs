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

        public bool AddHourReservationFromEmployeePanel(AddHourReservationFromEmployeeVM addHourReservation, string EmployeeId)
        {
            string StartHourString = addHourReservation.StartHour.Remove(2, 1);
            string EndHourString = addHourReservation.EndHour.Remove(2, 1);

            int StartHourInt = Convert.ToInt32(StartHourString);
            int EndtHourInt = Convert.ToInt32(EndHourString);

            int WorkTime = Math.Abs(EndtHourInt - StartHourInt);

            int StartHourReservationInt = Convert.ToInt32(StartHourString.Remove(2, 2));
            int EndHourReservationInt = Convert.ToInt32(EndHourString.Remove(2, 2));

            if (WorkTime >= 200 && WorkTime <= 2200)
            {
                HourReservation hour = new HourReservation()
                {
                    StartHourReservation = addHourReservation.StartHour,
                    EndHourReservation = addHourReservation.EndHour,
                    Description = null,
                    ReservationStatusID = 2,
                    DataReservationID = addHourReservation.DataReservationID,
                    EmployeeID = EmployeeId,
                    StartHourReservationInt = StartHourReservationInt,
                    EndHourReservationInt = EndHourReservationInt
                };
                Add(hour);
                return true;
            }

            else
            {
                return false;
            }

        }

        public HourReservation AddHourReservationWhileOnlineProccess(string employID, int DateID)
        {
            int StartHour = DateTime.Now.Hour;
            if (StartHour >= 10)
            {
                string startHourString = $"{StartHour}:00";
                HourReservation hour = new HourReservation()
                {
                    Description = null,
                    ReservationStatusID = 2,
                    DataReservationID = DateID,
                    EmployeeID = employID,
                    EndHourReservationInt = 0,
                    EndHourReservation = "00:00",
                    StartHourReservationInt = StartHour,
                    StartHourReservation = startHourString
                };
                Add(hour);

                return hour;
            }
            else
            {
                string startHourString = $"0{StartHour}:00";
                HourReservation hour = new HourReservation()
                {
                    Description = null,
                    ReservationStatusID = 2,
                    DataReservationID = DateID,
                    EmployeeID = employID,
                    EndHourReservationInt = 0,
                    EndHourReservation = "00:00",
                    StartHourReservationInt = StartHour,
                    StartHourReservation = startHourString
                };
                Add(hour);

                return hour;
            }
        }

        public void DeleteHourReservationFromEmployeePanel(HourReservation hourReservation)
        {

            Delete(hourReservation);

        }

        public List<HourReservation> GetEmployeeHourReservationByDateHourReservationID(int id)
        {
            return GetAll(includeProperties: "ReservationStatus")
                            .Where(p => p.DataReservationID == id).ToList();

        }

        public HourReservation GetHoureReservationByID(int id)
        {
            return GetAll(includeProperties: "User,DataReservation")
                            .Where(p => p.HourReservationID == id).First();
        }

        public HourReservation GetHourReservation(int id)
        {
            return GetById(id);
        }

        public List<HourReservation> GetJustTodayEmployeeHourReservationByEmployeeId(string EmployeeID)
        {
            return GetAll(includeProperties: "User,ReservationOrder,DataReservation")
                                            .Where(p => p.EmployeeID == EmployeeID
                                                && p.DataReservation.ReservationDateTime.Year == DateTime.Now.Year
                                                && p.DataReservation.ReservationDateTime.Month == DateTime.Now.Month
                                                && p.DataReservation.ReservationDateTime.Day == DateTime.Now.Day
                                                && p.ReservationStatusID == 1).ToList();
        }

        public List<HourReservation> GetTodayEmployeeHoureReservation(string EmployeeID)
        {
            return GetAll(includeProperties: "User,ReservationOrder,DataReservation")
                                                   .Where(p => p.EmployeeID == EmployeeID && p.DataReservation.ReservationDateTime.Year == DateTime.Now.Year
                                                              && p.DataReservation.ReservationDateTime.Month == DateTime.Now.Month
                                                               && p.DataReservation.ReservationDateTime.Day == DateTime.Now.Day)
                                                                    .OrderByDescending(p => p.HourReservationID).ToList();
        }

        public bool ISEmployeeHaveHObTightNow(List<HourReservation> List)
        {
            bool result = false;

            foreach (var item in List)
            {
                if (item.StartHourReservationInt <= DateTime.Now.Hour && item.EndHourReservationInt > DateTime.Now.Hour)
                {
                    result = true;
                }

            }

            if (result == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsExistHoureReservationWhiteDateReservationID(int id)
        {
            if (GetAll(p => p.DataReservationID == id).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsExistHourReservation2HourBefore(List<HourReservation> List)
        {
            List<int> HourList = new List<int>();

            foreach (var item in List)
            {
                if ((item.StartHourReservationInt - 1) <= DateTime.Now.Hour && item.StartHourReservationInt >= DateTime.Now.Hour)
                {
                    HourList.Add(item.HourReservationID);
                }
            }

            if (HourList.Count() != 0)
            {
                return true;
            }

            return false;
        }

        public bool IsExistSpecialHour(string EmployeeID, int Datetime, string StartHour, string EndHour)
        {
            string StartHourString = StartHour.Remove(2, 1);
            int StartHourReservationInt = Convert.ToInt32(StartHourString.Remove(2, 2));

            string EndHourString = EndHour.Remove(2, 1);
            int EndHourReservationInt = Convert.ToInt32(EndHourString.Remove(2, 2));

            List<HourReservation> listStart = GetAll(p => p.EmployeeID == EmployeeID && p.DataReservationID == Datetime
                                                    && p.StartHourReservationInt <= StartHourReservationInt
                                                    && p.EndHourReservationInt > StartHourReservationInt).ToList();

            List<HourReservation> listEnd = GetAll(p => p.EmployeeID == EmployeeID && p.DataReservationID == Datetime
                                                               && p.StartHourReservationInt < EndHourReservationInt
                                                               && p.EndHourReservationInt >= EndHourReservationInt).ToList();
                                                              
            if (listStart.Count() == 0 && listEnd.Count() == 0)
            {
                return false;
            }

            return true;
        }

        public void UpdateHourReservationAfterDeleteReservationOrder(HourReservation hour)
        {
            Update(hour);
        }

        public bool UpdateHourReservationFromEmployee(HourReservation hourReservation)
        {
            string StartHourString = hourReservation.StartHourReservation.Remove(2, 1);
            string EndHourString = hourReservation.EndHourReservation.Remove(2, 1);

            int StartHourInt = Convert.ToInt32(StartHourString);
            int EndtHourInt = Convert.ToInt32(EndHourString);

            int WorkTime = Math.Abs(EndtHourInt - StartHourInt);

            int StartHourReservationInt = Convert.ToInt32(StartHourString.Remove(2, 2));
            int EndHourReservationInt = Convert.ToInt32(EndHourString.Remove(2, 2));

            if (WorkTime >= 200 && WorkTime <= 2200)
            {
                hourReservation.StartHourReservationInt = StartHourReservationInt;
                hourReservation.EndHourReservationInt = EndHourReservationInt;

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
