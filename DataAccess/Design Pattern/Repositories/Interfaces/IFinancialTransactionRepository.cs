using DataAccess.Design_Pattern.GenericRepositories;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public interface IFinancialTransactionRepository : IGernericRepository<FinancialTrnsaction>
    {
        void AddFinancialTransactionForCashPaymentToEmployeeFromUser(Invoicing invoicing , decimal price);
        void AddFinancialTransactionForOnlinePaymentToEmployeeFromUser(Invoicing invoicing , decimal price);
        void CheckoutWhitEmployeeAfterHisRequest(decimal price , string EmployeeID);
        List<FinancialTrnsaction> GetAllEmployeePaymentTotheCompanyAccount(string EmployeeID);
        FinancialTrnsaction AddFinancialForPaymentFromEmployeeToTheCompanyAccountOnline(decimal Price , string EmployeeID , string EmployeeName);
        FinancialTrnsaction GetFinancialTransactionByID(int id);
        void UpdateFinancialTransaction(FinancialTrnsaction financial);
        List<FinancialTrnsaction> GetListOFUserFinacialTransaction(string Userid);
        List<FinancialTrnsaction> GetAllEmployeeFinancialTransaction(string EmployeeID);
        List<FinancialTrnsaction> GeAllFinancialTrnsactions();
    }
}
