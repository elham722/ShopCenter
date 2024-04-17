using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "تکرار رمز عبور با رمز عبور مطابقت ندارد")]
        public string RepeatNewPassword { get; set; }
    }
}
