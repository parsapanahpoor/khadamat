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
    public class AdminWalletRepository : GenericRepository<AdminWallet>, IAdminWalletRepository
    {
        private readonly KhadamatContext _db;

        public AdminWalletRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddAdminWallet()
        {
            AdminWallet wallet = new AdminWallet()
            {
                WalletAmount = 0,
                DebtAmount = 0,
                CreditAmount = 0,
            };

            Add(wallet);
        }

        public void CheckoutWhitEmployeeAfterHisRequest(decimal price)
        {
            AdminWallet wallet = GetAll().First();
            wallet.DebtAmount = wallet.DebtAmount - price;

            Update(wallet);
        }

        public AdminWallet GetAdminWallet()
        {
            return GetAll().First();
        }

        public bool IsExistAdminWallet()
        {
            bool respone = GetAll().Any();

            if (respone == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateAdminWalletForCashPaymentTotheEmployeeFromUser(decimal AdminPercent)
        {
            AdminWallet wallet = GetAll().Single();
            wallet.CreditAmount = wallet.CreditAmount + AdminPercent;

            Update(wallet);
        }

        public void UpdateAdminWalletForOnlinePaymentTotheEmployeeFromUser(decimal AdminPercent, decimal EmployeePercent)
        {
            AdminWallet wallet = GetAll().Single();
            wallet.WalletAmount = wallet.WalletAmount + AdminPercent;
            wallet.DebtAmount = wallet.DebtAmount + EmployeePercent;

            Update(wallet);
        }
    }
}
