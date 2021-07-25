using Models.Entities.Works;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.User
{
    public class UserSelectedJob
    {
        [Key]
        public int JobCategorySelectedID { get; set; }
        public string Userid { get; set; }
        public int JobCategoryId { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserAvatar { get; set; }

        [Display(Name = "شرح کامل رزومه ")]
        public string ResumeDescription { get; set; }

        #region Relations

        [ForeignKey("Userid")]
        public virtual User User { get; set; }
        public virtual JobCategory JobCategory { get; set; }
        #endregion
    }

}
