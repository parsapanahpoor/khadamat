using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.User
{
   public  class Comment
    {
        public Comment()
        {

        }
        [Key]
        public int CommentID { get; set; }
        public string UserID { get; set; }
        public string EmployeeID { get; set; }
        public string CommentBody { get; set; }
        public DateTime CreateDate { get; set; }
        public int? ParentID { get; set; }
        public bool IsDelete { get; set; }

        #region NavigationProperty

        public User Employee { get; set; }
        public User User { get; set; }

        #endregion
    }
}
