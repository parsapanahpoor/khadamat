using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Factor
{
   public  class RequestForCheckoutStatus
    {
        public RequestForCheckoutStatus()
        {
                
        }

        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
        public int RequestForCheckoutStatusID { get; set; }

        [Display(Name = "عنوان   ")]
        [Required]
        public string StatusTitle { get; set; }

        #region Navigation Property

        public List<RequestForCheckout> RequestForCheckouts { get; set; }

        #endregion
    }
}
