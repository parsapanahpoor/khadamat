using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Works
{
    public  class Tariff
    {
        public Tariff()
        {

        }

        [Key]
        public int TariffId { get; set; }

        [Display(Name = "عنوان تعرفه ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string TariffTitle { get; set; }

        [Display(Name = " درصد تخفیف  ")]
        public int? TariffPercent { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        [Display(Name = "تعرفه اصلی")]
        public int? ParentId { get; set; }



        #region Navigations
        public virtual List<InvoicingDetail> invoicingDetails { get; set; }

        #endregion

    }
}
