using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Domain.ViewModels.ContactUs.Client
{
    public class ContactUsViewModel
    {
        [MaxLength(100)]
        [Display(Name = "نام")]
        public string? FullName { get; set; }

        [MaxLength(100)]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(11)]
        [Display(Name = "شماره تماس")]
        public string? Phone { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Massage { get; set; }

        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public ContactUsSubjectTitle Subject { get; set; }

        [Display(Name = "فایل پیوست")]
        public IFormFile? AttachFile { get; set; }

    }
}
