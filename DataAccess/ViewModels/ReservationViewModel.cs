using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class AddDateReservationFromEmployee
    {

        public DateTime? DateTime { get; set; }

    }
    public class AddHourReservationFromEmployeeVM
    {
        public int DataReservationID { get; set; }

        [Display(Name = "ساعت شروع رزرو")]
        [MaxLength(40)]
        [Required]
        public string StartHour { get; set; }


        [Display(Name = "ساعت پایان رزرو")]
        [MaxLength(40)]
        [Required]
        public string EndHour { get; set; }
    }
    public class EmployeeReservationViewModel
    {
        public int UserReservationStatus { get; set; }
        public string EmployeeID { get; set; }
        public string UserID { get; set; }
        public int JobCategoryID { get; set; }
        public int? LocationID { get; set; }
        public int HoureReservationID { get; set; }
        public int DateReservationID { get; set; }
        public DateTime DateTimeReservation { get; set; }
        public string Description { get; set; }

    }

}
