using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.EmployeeReservation
{
    public class DataReservation
    {
        public DataReservation()
        {

        }

        [Key]
        public int DataReservationID { get; set; }

        [Display(Name = "تایخ رزرو")]
        [Required]
        public DateTime ReservationDateTime { get; set; }

        [Required]
        [ForeignKey("User")]
        public string EmployeeID { get; set; }


        #region Navigation Properties

        public User.User User { get; set; }
        public HourReservation HourReservation { get; set; }

        #endregion
    }
}
