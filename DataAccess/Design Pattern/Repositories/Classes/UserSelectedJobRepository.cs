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
    public class UserSelectedJobRepository : GenericRepository<UserSelectedJob>, IUserSelectedJobRepository
    {
        private readonly KhadamatContext _db;

        public UserSelectedJobRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddJobToUser(UserSelectedJob userSelectedJob , IFormFile UserAvatarFile)
        {
            UserSelectedJob job = new UserSelectedJob()
            {
                Userid = userSelectedJob.Userid,
                JobCategoryId = userSelectedJob.JobCategoryId,
                ResumeDescription = userSelectedJob.ResumeDescription,
                UserAvatar = "Defult.jpg"

        };

            if (UserAvatarFile != null)
            {


                job.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(UserAvatarFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/EmployeeResumeForJobs", job.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    UserAvatarFile.CopyTo(stream);
                }
            }

            Add(job);

        }

    public bool IsExistUserSelectedJob(string userid)
    {
        return GetAll().Any(p => p.Userid == userid);
    }
}
}
