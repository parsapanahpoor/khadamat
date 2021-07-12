using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.User
{
    public  class UserProfile
    {
        public UserProfile()
        {

        }
        [Key]
        public int  UserAvatarId { get; set; }

        public string UserId { get; set; }


        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserAvatar { get; set; }


        #region Relations

        public virtual User User { get; set; }


        #endregion

    }
}
