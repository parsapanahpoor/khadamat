using DataAccess.Design_Pattern.Repositories.Classes;
using DataContext.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Context
        private readonly KhadamatContext _db;

        public UnitOfWork(KhadamatContext db)
        {
            _db = db;
            userRepository = new UserRepository(_db);
            jobCategoryRepository = new JobCategoryRepository(_db);
            employeeRepository = new EmployeeRepository(_db);
            userSelectedJobRepository = new UserSelectedJobRepository(_db);
            dataReservationRepository = new DataReservationRepository(_db);
            hourReservationRepository = new HourReservationRepository(_db);
            locationAddressRepository = new LocationAddressRepository(_db);

        }

        #endregion

        #region Repositories
        public UserRepository userRepository { get; private set; }
        public EmployeeRepository employeeRepository { get; private set; }
        public JobCategoryRepository jobCategoryRepository { get; private set; }
        public UserSelectedJobRepository userSelectedJobRepository { get; private set; }
        public DataReservationRepository dataReservationRepository { get; private set; }
        public HourReservationRepository hourReservationRepository { get; private set; }
        public LocationAddressRepository locationAddressRepository { get; private set; }


        #endregion

        #region Implement

        public void SaveChangesDB()
        {
            _db.SaveChanges();
        }
        public int SaveChangesDBID()
        {
          return  _db.SaveChanges();
        }

        public Task<int> SaveChangesDBAsync()
        {
            return _db.SaveChangesAsync();
        }

        #endregion


        #region Dispose

        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

  

        #endregion
    }
}
