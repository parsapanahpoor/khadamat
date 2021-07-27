using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.User
{
    public  class EmployeeStatus
    {
        public EmployeeStatus()
        {

        }

        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
        public int EmployeeStatusID { get; set; }

        [Display(Name = "عنوان فارسی وضعیت  ")]
        public string PersianStatus { get; set; }

        [Display(Name = "عنوان لاتین  وضعیت  ")]
        public string EnglishStatus { get; set; }


        #region NavigationProperty

        public List<User> User { get; set; }

        #endregion
    }
}
