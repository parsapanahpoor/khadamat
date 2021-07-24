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

        #region Relations

        [ForeignKey("Userid")]
        public virtual User User { get; set; }
        public virtual JobCategory JobCategory { get; set; }
        #endregion
    }

}
