using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Score
{
   public class Scores
    {
        public Scores()
        {

        }
        [Key]
        public int ScoreID { get; set; }
        public string UserID { get; set; }
        public string EmployeeID { get; set; }
        public int Point { get; set; }

        #region NavigationProperty

        public User.User Employee { get; set; }
        public User.User User { get; set; }

        #endregion
    }
}
