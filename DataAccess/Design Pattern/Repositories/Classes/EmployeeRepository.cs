using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public  class EmployeeRepository : GenericRepository<EmployeeDocuments>, IEmployeeRepository
    {
        private readonly KhadamatContext _db;

        public EmployeeRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddEmployeeDocument(string userid)
        {
            EmployeeDocuments employee = new EmployeeDocuments()
            { 
                
                Userid = userid

            };

            Add(employee);
        }
    }
}
