using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.User
{
   public  class EmployeeInformationPossition
    {
        public EmployeeInformationPossition()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PossitionId { get; set; }

        [Display(Name = "عنوان   ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PossitionName { get; set; }



        #region Navigation Property

        #endregion
    }
}
