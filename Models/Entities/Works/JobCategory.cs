﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Works
{
    public class JobCategory
    {
        public JobCategory()
        {

        }

        [Key]
        public int JobCategoryId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }

        [Display(Name = "لوگو برای خدمت ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string JobLogo { get; set; }

        [Display(Name = " درصد کمیسیون ")]
        public int? Percent { get; set; }

        #region Relations

        //public virtual List<VideoSelectedCategory> VideoSelectedCategory { get; set; }

        #endregion
    }
}