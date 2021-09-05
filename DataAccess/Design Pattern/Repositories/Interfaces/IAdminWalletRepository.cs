using DataAccess.Design_Pattern.GenericRepositories;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public  interface IAdminWalletRepository : IGernericRepository<AdminWallet>
    {
        bool IsExistAdminWallet();
        void UpdateAdminWalletForCashPaymentTotheEmployeeFromUser(decimal AdminPercent);
        void UpdateAdminWalletForOnlinePaymentTotheEmployeeFromUser(decimal AdminPercent , decimal EmployeePercent);
        void AddAdminWallet();
        AdminWallet GetAdminWallet();
        void CheckoutWhitEmployeeAfterHisRequest(decimal price);
        void PaymentToCompanyPercentFromEmployeeOnlineToTheCompanyAccount(decimal price);
    }
}
