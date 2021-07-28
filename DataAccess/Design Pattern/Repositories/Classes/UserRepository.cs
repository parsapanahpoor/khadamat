using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.ViewModels;
using DataContext.Context;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Genarator;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly KhadamatContext _db;

        public UserRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void DeleteUserAvatar(User user)
        {
            if (user.UserAvatar != "Defult.jpg")
            {
                string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", user.UserAvatar);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }
            }


        }

        public bool? GetEmployeeAcceptedPossition(string username)
        {
            var user = GetAll(p=>p.UserName == username).First();
            return user.IsAccepted;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {

            return GetAll().Where(p => p.UserName == username)
                                .Select(p => new SideBarUserPanelViewModel()
                                {
                                    UserName = username,
                                    ImageName = p.UserAvatar



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

        public int GetUserOnlineStatus(string username)
        {
            return GetAll(p => p.UserName == username)
                        .Select(p => p.EmployeeStatusID).First();
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

        public User UpdateUserAvatar(User user, EditUserInAdminPanel userEdited)
        {
            if (userEdited.UserAvatar != null)
            {
                if (user.UserAvatar != "Defult.jpg")
                {

                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", user.UserAvatar);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                }
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(userEdited.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", user.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    userEdited.UserAvatar.CopyTo(stream);
                }

            }

            return user;
        }

    }

}
