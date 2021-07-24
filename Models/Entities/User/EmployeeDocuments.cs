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

        [Display(Name = " نام و نام خانوادگی    ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FamilyName { get; set; }

        [Display(Name = " نام پدر    ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FatherName { get; set; }

        [Display(Name = "  شماره شناسنامه     ")]
        public long? CertificateCode { get; set; }

        [Display(Name = "  تاریخ تولد      ")]
        public DateTime BirthDay { get; set; }

        [Display(Name = "  کد ملی       ")]
        public long? PersonalCode { get; set; }

        [Display(Name = "   تاریخ صدور شناسنامه       ")]
        public DateTime PersonalCodeDate { get; set; }

        [Display(Name = "   محل صدور شناسنامه       ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PersonalCodeLocation  { get; set; }

        [Display(Name = "   محل تولد       ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string BirthdayLocation { get; set; }

        [Display(Name = "   دین        ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Religion { get; set; }

        [Display(Name = "   وضعیت تاهل         ")]
        public bool? MaritalStatus { get; set; }

        [Display(Name = "   وضعیت نظام وظیفه          ")]
        public bool? MilitaryService { get; set; }

        [Display(Name = "   توضیح معافیت سربازی         ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string MilitaryServiceDesCription { get; set; }

        [Display(Name = "   سلامت روحی و جسمی           ")]
        public bool? HealthStatus { get; set; }

        [Display(Name = "   سلامت روحی و جسمی           ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string HealthStatusDescription { get; set; }

        [Display(Name = "   آخرین مدرک تحصیلی           ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LastEducationalCertificate { get; set; }

        [Display(Name = "   رشته ی تحصیلی           ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FieldOfStudy { get; set; }

        [Display(Name = " آخرین تجربه ی کاری   ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LastWorkExperience { get; set; }

        [Display(Name = " سمت / شغل    ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string JobRank { get; set; }

        [Display(Name = " سابقه ی بازداشت   ")]
        public bool? HistoryOfDetention { get; set; }

        [Display(Name = "   توضیحات درباره ی سابقه ی بازداشت")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string HistoryOfDetentionDescription { get; set; }

        [Display(Name = " تایید فرم   ")]
        public bool? Obligation { get; set; }

        [Display(Name = " توضیحات    ")]
        public string Description { get; set; }

        public int PossitionId { get; set; }

        #region Relations

        public virtual User User { get; set; }
        public virtual EmployeeInformationPossition EmployeeInformationPossition { get; set; }

        #endregion
    }
}
