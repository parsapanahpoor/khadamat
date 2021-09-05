using DataAccess.Design_Pattern.GenericRepositories;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public interface IEmployeeWalletRepository : IGernericRepository<EmployeeWallet>
    {
        bool IsExistEmployeeWallet(string EmployeeId);
        void UpdateEmployeeWalletForCashPaymentFromUser(string employeeId , decimal AdminPercent);
        void UpdateEmployeeWalletForOnlinePaymentFromUser(string employeeId , decimal EmployeePercent);
        void AddEmployeeWallet(string EmployeeId);
        EmployeeWallet GetEmployeeWalletByEmployeeID(string EmployeeID);
        void CheckOutWhitEmployeeAfterHisRequest(string EmployeeID , decimal Price);
        void PaymentToCompanyPercentFromEmployeeOnlineToTheCompanyAccount(string EmployeeID , decimal price);
    }
}
