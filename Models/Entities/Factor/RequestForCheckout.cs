using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Factor
{
    public  class RequestForCheckout
    {
        public RequestForCheckout()
        {
                
        }

        [Key]
        public int RequestForCheckoutID { get; set; }

        [ForeignKey("RequestForCheckoutStatus")]
        public int RequestForCheckoutStatusID { get; set; }

        public decimal Price { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public string CartBankNumber { get; set; }

        [Required]
        public string OwnerBankCart { get; set; }

        #region Navigation Propert

        public RequestForCheckoutStatus RequestForCheckoutStatus { get; set; }

        #endregion
    }
}
