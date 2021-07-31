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

}
