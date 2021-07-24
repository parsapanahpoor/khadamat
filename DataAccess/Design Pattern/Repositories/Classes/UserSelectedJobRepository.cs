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
    public class UserSelectedJobRepository : GenericRepository<UserSelectedJob>, IUserSelectedJobRepository
    {
        private readonly KhadamatContext _db;

        public UserSelectedJobRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

    }
}
