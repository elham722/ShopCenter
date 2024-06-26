﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.ViewModels.User
{
    public class ForgetPasswordViewModel
    {
        [Display(Name = "ایمیل یا شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EmailOrPhoneNumber { get; set; }
    }
}
