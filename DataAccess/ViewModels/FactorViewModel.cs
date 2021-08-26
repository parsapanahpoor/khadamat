using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class InvoicingDetail
    {
        public int ReservationOrderID { get; set; }

        public int? TariffID { get; set; }

        public Double Price { get; set; }

        public string Description { get; set; }

        public int? PerCent { get; set; }

        public bool IsDelete { get; set; }
    }

    public class FirstStepForInvoicing
    {
        public int ReservationOrderID { get; set; }

        public string Description { get; set; }

        public int PaymentMethodId { get; set; }
    }
}
