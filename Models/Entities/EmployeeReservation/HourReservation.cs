using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities.User;

namespace Models.Entities.EmployeeReservation
{
    public  class HourReservation
    {
        public HourReservation()
        {

        }

        [Key]
        public int HourReservationID { get; set; }

        [Display(Name = "ساعت شروع رزرو")]
        [MaxLength(40)]
        [Required]
        public string StartHourReservation { get; set; }

        [Display(Name = "ساعت پایان رزرو")]
        [MaxLength(40)]
        [Required]
        public string EndHourReservation { get; set; }

        [Display(Name = "ساعت شروع رزرو")]
        public int StartHourReservationInt { get; set; }

        [Display(Name = "ساعت پایان رزرو")]
        public int EndHourReservationInt { get; set; }

        [Display(Name = " توضیحات    ")]
        public string Description { get; set; }

        [Required]
        [ForeignKey("ReservationStatus")]
        public int ReservationStatusID { get; set; }

        [Required]
        [ForeignKey("DataReservation")]
        public int DataReservationID { get; set; }

        [Required]
        [ForeignKey("User")]
        public string EmployeeID { get; set; }

        #region Naqvigation Properties

        public ReservationStatus ReservationStatus { get; set; }
        public User.User User { get; set; }
        public DataReservation DataReservation { get; set; }

        #endregion
    }
}
