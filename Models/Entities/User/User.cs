﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public bool IsAccepted { get; set; }



        #region Relations

        public virtual EmployeeDocuments EmployeeDocuments { get; set; }

        #endregion

    }
}
