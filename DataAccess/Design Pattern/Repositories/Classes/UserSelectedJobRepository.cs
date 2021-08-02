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

        public void AddJobToUser(UserSelectedJob userSelectedJob, IFormFile UserAvatarFile)
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

        public void DeleteUserSelectedJob(UserSelectedJob userSelectedJob)
        {

                if (userSelectedJob.UserAvatar != "Defult.jpg")
                {

                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/EmployeeResumeForJobs", userSelectedJob.UserAvatar);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                }
   


            Delete(userSelectedJob);
        }

        public List<UserSelectedJob> GetListOfEmployeeThatHaveThisJob(int id)
        {
            return GetAll(includeProperties: "User,JobCategory")
                            .Where(p => p.JobCategoryId == id)
                            .ToList();
        }

        public UserSelectedJob GetUserSelectedJobByID(int id)
        {
            return GetAll(includeProperties: "User,JobCategory")
                            .First(p => p.JobCategorySelectedID == id);
        }

        public UserSelectedJob GetUserselectedJobByJobID(int jobid)
        {
            return GetAll(includeProperties: "JobCategory")
                            .Where(p => p.JobCategorySelectedID == jobid).First();
        }

        public List<UserSelectedJob> GetUserSelectedJobByUserid(string userid)
        {
            return GetAll(includeProperties: "JobCategory")
                                .Where(p => p.Userid == userid).ToList();
        }

        public List<int> GetUserSelectedJobIDJobByUserid(string userid)
        {

            return GetAll(includeProperties: "JobCategory")
                                .Where(p => p.Userid == userid).Select(p=>p.JobCategoryId)
                                                .ToList();
        }

        public bool IsExistUserSelectedJob(string userid)
        {
            return GetAll().Any(p => p.Userid == userid);
        }

        public bool IsExistUserWithCurrentJob(int jobid, string userid)
        {
            return GetAll(p => p.JobCategoryId == jobid && p.Userid == userid).Any();
        }

        public void UpdateUserSelectedJob(UserSelectedJob userSelectedJob, IFormFile UserAvatarFile)
        {
            if (UserAvatarFile != null)
            {
                if (userSelectedJob.UserAvatar != "Defult.jpg")
                {

                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/EmployeeResumeForJobs", userSelectedJob.UserAvatar);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                }
                userSelectedJob.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(UserAvatarFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/EmployeeResumeForJobs", userSelectedJob.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    UserAvatarFile.CopyTo(stream);
                }

            }

            Update(userSelectedJob);
        }
    }
}
