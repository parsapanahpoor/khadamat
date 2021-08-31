using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class EmployeeWalletRepository : GenericRepository<EmployeeWallet>, IEmployeeWalletRepository
    {
        private readonly KhadamatContext _db;

        public EmployeeWalletRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddEmployeeWallet(string EmployeeId)
        {
            EmployeeWallet wallet = new EmployeeWallet()
            {
                EmployeeId = EmployeeId,
                CreditAmount = 0,
                DebtAmount = 0
            };

            Add(wallet);
        }

        public bool IsExistEmployeeWallet(string EmployeeId)
        {
            bool response = GetAll(p => p.EmployeeId == EmployeeId).Any();

            if (response == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateEmployeeWalletForCashPaymentFromUser(string employeeId, decimal AdminPercent)
        {
            EmployeeWallet wallet = GetAll(p => p.EmployeeId == employeeId).First();
            wallet.DebtAmount = wallet.DebtAmount + AdminPercent;

            Update(wallet);
        }
    }
}
