using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.User
{
   public  class Location
    {
        public Location()
        {

        }
        [Key]
        public int LocationID { get; set; }

        [Required]
        [ForeignKey("User")]
        public String UserID { get; set; }

        [Required]
        [Display(Name = "آدرس")]
        public string LocationAddress { get; set; }

        [Display(Name = "کد پستی ")]
        [MaxLength(50)]
        public string PostalCode { get; set; }

        #region NAvigation Property

        public User User { get; set; }

        #endregion
    }
}
