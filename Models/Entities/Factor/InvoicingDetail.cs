using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Factor
{
    public class InvoicingDetail
    {
        public InvoicingDetail()
        {

        }

        [Key]
        public int InvoicingDetailID { get; set; }

        [ForeignKey("Invoicing")]
        public int InvoicingID { get; set; }

        [ForeignKey("JobCategory")]
        public int? JobCategoryID { get; set; }

        public Double Price { get; set; }

        public string Description { get; set; }

        public int PerCent { get; set; }

        public bool IsDelete { get; set; }


        #region Navigation

        public Invoicing Invoicing { get; set; }
        public JobCategory JobCategory { get; set; }

        #endregion
    }
}
