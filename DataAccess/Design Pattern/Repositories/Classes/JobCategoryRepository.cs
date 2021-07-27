using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Microsoft.AspNetCore.Http;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Genarator;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class JobCategoryRepository : GenericRepository<JobCategory>, IJobCatgeoryRepository
    {
        private readonly KhadamatContext _db;

        public JobCategoryRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddJobCategory(JobCategory jobCategory , IFormFile JobPic)
        {

            JobCategory job = new JobCategory()
            { 
                ParentId = jobCategory.ParentId , 
                JobLogo = "JobDefualt.jpg" ,
                CategoryTitle = jobCategory.CategoryTitle ,
                IsDelete = false , 
                Percent = jobCategory.Percent


            };

            if (JobPic != null)
            {


                job.JobLogo = NameGenerator.GenerateUniqCode() + Path.GetExtension(JobPic.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/JobsLogo", job.JobLogo);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    JobPic.CopyTo(stream);
                }
            }

            Add(job);
        }

        public void DeleteJobCategory(JobCategory jobCategory)
        {
            jobCategory.IsDelete = true;
            Update(jobCategory);
        }

        public List<JobCategory> GetAllJobsCategories()
        {
            return GetAll().ToList();
        }

        public JobCategory GetJobCatgeoriesById(int id)
        {
            return GetAll(p => p.JobCategoryId == id).First();
        }

        public string GetJobCatgeoryNameById(int id)
        {
            return GetAll(p => p.JobCategoryId == id).Select(p=>p.CategoryTitle)
                                        .First();
        }

        public List<JobCategory> GetSubGroupOfJobCategorie(int id)
        {
            return GetAll(p => p.ParentId == id).ToList();
        }

        public void UpdateJobCaategory(JobCategory jobCategory, IFormFile JobPic)
        {
            if (JobPic != null)
            {
                if (jobCategory.JobLogo != "Defult.jpg")
                {

                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/JobsLogo", jobCategory.JobLogo);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                }
                jobCategory.JobLogo = NameGenerator.GenerateUniqCode() + Path.GetExtension(JobPic.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/JobsLogo", jobCategory.JobLogo);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    JobPic.CopyTo(stream);
                }

            }

            Update(jobCategory);
        }
    }
}
