using DataAccess.Design_Pattern.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        #region Repositories
        UserRepository userRepository { get; }
        EmployeeRepository employeeRepository { get; }
        JobCategoryRepository jobCategoryRepository { get; }
        UserSelectedJobRepository userSelectedJobRepository { get; }
        DataReservationRepository dataReservationRepository { get; }
        HourReservationRepository hourReservationRepository { get; }
        LocationAddressRepository locationAddressRepository { get; }
        ReservaitionOrderRepository reservaitionOrderRepository { get; }
        TariffRepository tariffRepository { get; }
        InvoicingDetailsRepository invoicingDetailsRepository { get; }
        InvoicingRepository invoicingRepository { get; }
        FinancialTransactionRepository FinancialTransactionRepository { get; }
        AdminWalletRepository AdminWalletRepository { get; }
        EmployeeWalletRepository EmployeeWalletRepository { get; }
        RequestForCheckoutRepository RequestForCheckoutRepository { get; }

        #endregion


        void SaveChangesDB();
        int SaveChangesDBID();
        Task<int> SaveChangesDBAsync();
    }
}
