using Models.Entities.User;
using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.EmployeeReservation
{
    public class ReservationOrder
    {
        public ReservationOrder()
        {

        }

        [Key]
        public int ReservationOrderID { get; set; }

        [ForeignKey("UserReserveStatus")]
        public int UserReservationStatus { get; set; }

        //[ForeignKey("User")]
        public string EmployeeID { get; set; }

        //[ForeignKey("User")]
        public string UserID { get; set; }

        [ForeignKey("JobCategory")]
        public int JobCategoryID { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }

        [ForeignKey("HourReservation")]
        public int HoureReservationID { get; set; }

        [ForeignKey("DataReservation")]
        public int DateReservationID { get; set; }

        public DateTime DateTimeReservation { get; set; }

        public string Description { get; set; }

        #region Navigation Property

        public UserReserveStatus UserReserveStatus { get; set; }
        public User.User Employee { get; set; }
        public User.User User { get; set; }
        public JobCategory  JobCategory { get; set; }
        public Location Location { get; set; }
        public HourReservation HourReservation { get; set; }
        public DataReservation DataReservation { get; set; }

        #endregion
    }
}
