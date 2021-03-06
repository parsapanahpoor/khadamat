using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.ViewModels;
using Models.Entities.EmployeeReservation;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public  interface IInvoicingRepository : IGernericRepository<Invoicing>
    {
        Invoicing AddInvoicingByReservationOrderInformations(ReservationOrder reservation , FirstStepForInvoicing first);
        Invoicing GetInvoicingByID(int id);
        void CloseInvoicingFromEmployeePanel(Invoicing invoicing);
        Invoicing GetInvoicingByReservationOrderID(int reservationID);
        void UpdateInvoicing(Invoicing invoicing);
        bool ISExistInvoicingWithHourReservationID(int id);
        bool IsInvoicingFinallyByHourID(int HourID);
        Invoicing GetInvoicingByHourID(int id);
        void SubmitInvoicingFromAdminPanel(Invoicing invoicing);
        bool IsInvoicingPayed(Invoicing invoicing);
        List<ReservationOrder> IsPayedReservations();
        List<ReservationOrder> IsFinallyReservations();
        List<ReservationOrder> IsCloseReservations();
    }
}
