using DataAccess.Design_Pattern.GenericRepositories;
using Microsoft.AspNetCore.Http;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public  interface IUserSelectedJobRepository : IGernericRepository<UserSelectedJob>
    {
        bool IsExistUserSelectedJob(string userid);
        void AddJobToUser(UserSelectedJob userSelectedJob , IFormFile UserAvatarFile);
        List<UserSelectedJob> GetUserSelectedJobByUserid(string userid);
        UserSelectedJob GetUserselectedJobByJobID(int jobid);
        void UpdateUserSelectedJob(UserSelectedJob userSelectedJob , IFormFile UserAvatarFile);
        void DeleteUserSelectedJob(UserSelectedJob userSelectedJob);
    }
}
