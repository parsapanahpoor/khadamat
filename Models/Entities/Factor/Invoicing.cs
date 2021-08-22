using Models.Entities.EmployeeReservation;
using Models.Entities.User;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Factor
{
    public class Invoicing
    {
        public Invoicing()
        {

        }

        [Key]
        public int InvoicingID { get; set; }

        [ForeignKey("ReservationOrder")]
        public int ReservationOrderID { get; set; }

        [ForeignKey("PaymentMethod")]
        public int PaymentMethod { get; set; }

        public string EmployeeID { get; set; }

        public string UserID { get; set; }

        [ForeignKey("JobCategory")]
        public int JobCategoryID { get; set; }

        [ForeignKey("HourReservation")]
        public int? HoureReservationID { get; set; }

        [ForeignKey("DataReservation")]
        public int? DateReservationID { get; set; }

        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public bool IsDelete { get; set; }

        public bool IsFinaly { get; set; }


        #region Navigation

        public User.User Employee { get; set; }
        public User.User User { get; set; }
        public JobCategory JobCategory { get; set; }
        public HourReservation HourReservation { get; set; }
        public DataReservation DataReservation { get; set; }
        public ReservationOrder ReservationOrder { get; set; }
        public List<InvoicingDetail> invoicingDetails { get; set; }

        #endregion
    }
}
