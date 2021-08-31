using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Factor
{
    public class FinancialTrnsaction
    {
        public FinancialTrnsaction()
        {

        }
        [Key]
        public int FinancialTransactionID { get; set; }

        [ForeignKey("FinancialTransactionStatus")]
        public int FinancialTransactionStatusID { get; set; }

        [ForeignKey("Invoicing")]
        public int? InvoicingId { get; set; }

        public decimal Price { get; set; }

        public string EmployeeID { get; set; }

        public string UserID { get; set; }

        [MaxLength(250)]
        public string BankRecepiet { get; set; }

        [MaxLength(250)]
        public string BankTransferNumber { get; set; }

        [MaxLength(250)]
        public string ReciverPerson { get; set; }

        [MaxLength(250)]
        public string DepositeFromPerson { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        #region Navigation Property

        public FinancialTransactionStatus FinancialTransactionStatus { get; set; }
        public Factor.Invoicing Invoicing { get; set; }
        public User.User Employee { get; set; }
        public User.User User { get; set; }

        #endregion
    }
}
