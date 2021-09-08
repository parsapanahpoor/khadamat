using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Models.Entities.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class ScoreRepository : GenericRepository<Scores>, IScoreRepository
    {
        private readonly KhadamatContext _db;

        public ScoreRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddScoreToTheEmployee(string userid, string EmployeeID , int point)
        {
            Scores score = new Scores()
            { 
                UserID = userid,
                EmployeeID = EmployeeID,
                Point = point
            };

            Add(score);
        }

        public int CalculateEmployeeScore(string EmployeeID)
        {
            if (GetAll(p=>p.EmployeeID == EmployeeID).Any())
            {
                decimal Sum = GetAll(p => p.EmployeeID == EmployeeID).Select(p => p.Point).Sum();
                decimal Count = GetAll(p => p.EmployeeID == EmployeeID).Count();

                decimal Calculate = Math.Floor(Sum / Count) ;
                return (int)Calculate;
            }
            return 0;
        }

        public bool IsExistScoreFromUserToEmployee(string userid, string EmployeeId)
        {
            bool Result = GetAll(p => p.UserID == userid && p.EmployeeID == EmployeeId).Any();
            if (Result == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
