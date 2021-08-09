﻿using DataAccess.Design_Pattern.GenericRepositories;
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

        public List<DataReservation> GetDateReservationByEmployeeId(string EmployeeID)
        {
            return GetAll(includeProperties: "User,HourReservation")
                            .Where(p => p.EmployeeID == EmployeeID)
                                    .OrderBy(p=>p.ReservationDateTime).ToList();
        }

        public List<DataReservation> GetListOfEmployeeDataReservation(string userid)
        {
            return GetAll(includeProperties: "HourReservation")
                        .Where(p=>p.ReservationDateTime.Year >= DateTime.Now.Year
                                           && p.ReservationDateTime.Month >= DateTime.Now.Month
                                            && p.ReservationDateTime.Day >= DateTime.Now.Day)
                        .OrderBy(p => p.DataReservationID).ToList();
        }

        public List<DataReservation> GetListOfEmployeeDataReservationHistory(string userid)
        {
            return GetAll(includeProperties: "HourReservation")
                                 .Where(p => p.ReservationDateTime <= DateTime.Now)
                                 .OrderBy(p => p.ReservationDateTime).ToList();
        }

        public List<DataReservation> GetTodayEmployeeDateReservation(string EmployeeID)
        {
            return GetAll(includeProperties: "User,ReservationOrder,HourReservation")
                                          .Where(p => p.EmployeeID == EmployeeID && p.ReservationDateTime.Year >= DateTime.Now.Year
                                                     && p.ReservationDateTime.Month >= DateTime.Now.Month
                                                      && p.ReservationDateTime.Day >= DateTime.Now.Day).ToList();
        }

        public void UpdateDateReservationFromEmployeePanel(DataReservation data)
        {
            Update(data);
        }
    }
}
