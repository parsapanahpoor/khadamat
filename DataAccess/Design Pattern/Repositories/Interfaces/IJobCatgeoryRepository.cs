using DataAccess.Design_Pattern.GenericRepositories;
using Microsoft.AspNetCore.Http;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public interface IJobCatgeoryRepository : IGernericRepository<JobCategory>
    {

        List<JobCategory> GetAllJobsCategories();
        void AddJobCategory(JobCategory jobCategory , IFormFile JobPic);
        JobCategory GetJobCatgeoriesById(int id);
        string GetJobCatgeoryNameById(int id);
        void UpdateJobCaategory(JobCategory jobCategory , IFormFile JobPic);
        void DeleteJobCategory(JobCategory jobCategory);
        List<JobCategory> GetSubGroupOfJobCategorie(int id);
    }
}
