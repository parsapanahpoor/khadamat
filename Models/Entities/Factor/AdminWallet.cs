using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Factor
{
    public class AdminWallet
    {
        public AdminWallet()
        {
                
        }

        [Key]
        public int AdminWalletID { get; set; }

        public decimal WalletAmount { get; set; }

        public decimal DebtAmount { get; set; }

        public decimal CreditAmount { get; set; }

        #region Navigation Property

        #endregion
    }
}
