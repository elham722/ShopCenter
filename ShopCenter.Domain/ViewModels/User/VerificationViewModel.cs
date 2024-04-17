using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.ViewModels.User
{
    public class VerificationViewModel
    {
        [Display(Name = "کد تایید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(10000, 99999, ErrorMessage = "کد تایید پنج رقمی را به صورت صحیح وارد کنید")]
        public int VerificationCode { get; set; }
    }
}
