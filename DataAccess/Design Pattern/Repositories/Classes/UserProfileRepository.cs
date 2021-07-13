using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Microsoft.AspNetCore.Http;
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

     

        public void AddUserProfileAfterRegisterAdminPanel(string id, IFormFile Avatar)
        {
            UserProfile pro = new UserProfile()
            {
                UserId = id,
                UserAvatar = "Defult.jpg"


            };
            
            if (Avatar != null)
            {


                pro.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(Avatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", pro.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Avatar.CopyTo(stream);
                }
            }

            Add(pro);
        }

        public void EditUserProfile(UserProfile userProfile ,  IFormFile Avatar)
        {

            if (userProfile.UserAvatar != "Defult.jpg")
            {
                string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", userProfile.UserAvatar);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }

                userProfile.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(Avatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", userProfile.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Avatar.CopyTo(stream);
                }
                }
            Update(userProfile);
        }

        public UserProfile GetUserProfileById(string id)
        {
            return GetAll().Where(p => p.UserId == id).First();
        }
    }
}
