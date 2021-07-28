using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.EmployeeReservation
{
    public class ReservationStatus
    {
        public ReservationStatus()
        {

        }


        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
        public int ReservationStatusID { get; set; }

        [Display(Name = "عنوان فارسی وضعیت  ")]
        public string PersianStatus { get; set; }

        [Display(Name = "عنوان لاتین  وضعیت  ")]
        public string EnglishStatus { get; set; }



        #region Navigation Properties

        public List<HourReservation> HourReservations { get; set; }

        #endregion
    }

}
