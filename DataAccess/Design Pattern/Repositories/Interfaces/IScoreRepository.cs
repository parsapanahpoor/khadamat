using DataAccess.Design_Pattern.GenericRepositories;
using Models.Entities.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public interface IScoreRepository : IGernericRepository<Scores>
    {
        bool IsExistScoreFromUserToEmployee(string userid , string EmployeeId);
        void AddScoreToTheEmployee(string userid , string EmployeeID ,  int point);
        int CalculateEmployeeScore(string EmployeeID);
    }
}
