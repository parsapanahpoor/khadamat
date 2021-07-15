using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.ViewModels;
using DataContext.Context;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly KhadamatContext _db;

        public UserRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {

            return GetAll(includeProperties: "UserProfile").Where(p => p.UserName == username)
                                .Select(p => new SideBarUserPanelViewModel()
                                {
                                    UserName = username,
                                    ImageName = p.UserProfile.UserAvatar



                                }).Single();

        }

        public User GetUserByForgotPasswordCode(string Code)
        {
            var user = GetAll(p => p.ForgotPasswordCode == Code).First();
            return user; 
        }

        public string GetUserByUserName(string username)
        {
            return GetAll(p => p.UserName == username).Select(p => p.Id).First();
        }

        public bool IsExistEmail(string email)
        {
            return GetAll().Any(p => p.Email == email);

        }

        public bool IsExistPhoneNumber(string phonenumber)
        {
            return GetAll().Any(p => p.PhoneNumber == phonenumber);
        }

        public bool IsExistUserName(string username)
        {
            return GetAll().Any(p => p.UserName == username);
        }

    }

}
