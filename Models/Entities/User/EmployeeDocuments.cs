using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.User
{
    public class EmployeeDocuments
    {
        public EmployeeDocuments()
        {

        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Userid { get; set; }

        [Display(Name = "کپی شناسنامه ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EmployeeCertificate { get; set; }

        [Display(Name = "عکس پرسنلی   ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PersonalPicture { get; set; }

        [Display(Name = "شماره حساب بانکی     ")]
        public long? BankAccountNumber { get; set; }

        public int? HomePhoneNumber { get; set; }

        [Display(Name = " توضیحات    ")]
        public string Description { get; set; }

        public int PossitionId { get; set; }

        #region Relations

        public virtual User User { get; set; }
        public virtual EmployeeInformationPossition EmployeeInformationPossition { get; set; }

        #endregion
    }
}
