using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Factor
{
    public class EmployeeWallet
    {
        public EmployeeWallet()
        {
                
        }

        [Key]
        public int EmployeeWalletID { get; set; }

        [ForeignKey("User")]
        [Required]
        public string EmployeeId { get; set; }

        public decimal CreditAmount { get; set; }

        public decimal DebtAmount { get; set; }

        #region Navigation Property

        public User.User User { get; set; }

        #endregion
    }
}
