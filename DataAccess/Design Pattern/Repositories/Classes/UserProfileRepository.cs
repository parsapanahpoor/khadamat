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
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        private readonly KhadamatContext _db;

        public UserProfileRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddUserProfileAfterRegister(string id)
        {
            UserProfile pro = new UserProfile()
            {
                UserId = id,
                UserAvatar = "Defult.jpg"

            };
            Add(pro);
        }
    }
}
