using Microsoft.AspNetCore.Identity;
using Models.Entities.EmployeeReservation;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.User
{
    public class User : IdentityUser
    {
        public User()
        {

        }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }



        [Display(Name = "تاریخ ثبت نام")]
        [DisplayFormat(DataFormatString ="{0 : yyyy/MM/dd}")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserAvatar { get; set; }


        [Display(Name = "کد فعال سازی")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ActiveCode { get; set; }
        [Display(Name = "کد فراموشی رمز عبور ")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ForgotPasswordCode { get; set; }

        public bool IsDelete { get; set; }
        public  bool? IsAccepted { get; set; }

        [ForeignKey("EmployeeStatus")]
        public int EmployeeStatusID { get; set; }



        #region Relations

        public virtual EmployeeDocuments EmployeeDocuments { get; set; }
        public virtual List<UserSelectedJob> UserSelected { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public List<HourReservation> HourReservation { get; set; }
        public List<DataReservation> DataReservation { get; set; }
        public List<Location> Location { get; set; }
        public List<ReservationOrder> ReservationOrderEmployee { get; set; }
        public List<ReservationOrder> ReservationOrderUser { get; set; }
        public List<Invoicing> InvoicingEmployee { get; set; }
        public List<Invoicing> InvoicingUser { get; set; }
        public List<FinancialTrnsaction> FinancialTrnsactionsUser { get; set; }
        public List<FinancialTrnsaction> FinancialTrnsactionsEmployee { get; set; }
        public List<Score.Scores> ScoresUser { get; set; }
        public List<Score.Scores> ScoresEmployee { get; set; }
        public List<EmployeeWallet> EmployeeWallets { get; set; }
        public List<RequestForCheckout> RequestForCheckouts { get; set; }

        #endregion

    }
}
