using Models.Entities.Factor;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Works
{
    public class JobCategory
    {
        public JobCategory()
        {

        }

        [Key]
        public int JobCategoryId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }

        [Display(Name = "لوگو برای خدمت ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string JobLogo { get; set; }

  

        #region Relations

        public virtual List<UserSelectedJob> UserSelected { get; set; }
        public virtual List<EmployeeReservation.ReservationOrder> ReservationOrder { get; set; }
        public virtual List<Invoicing> Invoicing { get; set; }

        #endregion
    }
}
