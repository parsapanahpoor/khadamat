using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.EmployeeReservation
{
    public class UserReserveStatus
    {
        public UserReserveStatus()
        {

        }

        [Key]
        public int UserReservationStatus { get; set; }

        [Display(Name = "عنوان فارسی ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string StatusPersianTitle { get; set; }

        [Display(Name = "عنوان لاتین ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string StatusEnglishTitle { get; set; }

        #region Navigation Property


        #endregion
    }
}
