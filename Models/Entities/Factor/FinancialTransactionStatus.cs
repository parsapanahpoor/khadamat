using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Factor
{
    public class FinancialTransactionStatus
    {
        public FinancialTransactionStatus()
        {

        }

        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
        public int FinancialTransactionStatusID { get; set; }

        [Display(Name = "عنوان   ")]
        [Required]
        public string StatusTitle { get; set; }

        #region Navigation Properties

        public List<FinancialTrnsaction> FinancialTrnsactions { get; set; }

        #endregion
    }
}
