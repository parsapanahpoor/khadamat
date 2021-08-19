using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Factor
{
    public  class PaymentMethod
    {
        public PaymentMethod()
        {

        }

        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
        public int PaymentMethodID { get; set; }

        [Display(Name = "عنوان فارسی روش  ")]
        public string MethodTitle { get; set; }


        #region MyRegion

        #endregion

    }
}
