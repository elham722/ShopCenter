using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Domain.ViewModels.ContactUs.Admin
{
    public class ContactUsAdminSideViewModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "نام کاربر :")]
        public string? FullName { get; set; }

        [MaxLength(100)]
        [Display(Name = "ایمیل :")]
        public string Email { get; set; }

        [MaxLength(11)]
        [Display(Name = "شماره تماس :")]
        public string? Phone { get; set; }

        [Display(Name = "پیام")]
        public string Massage { get; set; }

        [Display(Name = "پاسخ")]
        [Required(ErrorMessage = "لطفا {0} پیام را بنویسید")]
        public string Answer { get; set; }

        public DateTime? AnswerDateTime { get; set; }

        [Display(Name = "موضوع :")]
        public ContactUsSubjectTitle Subject { get; set; }

        [Display(Name = "پیوست :")]
        public string? FileName { get; set; }

        public FileTypeTitle? FileType { get; set; }

        [Display(Name = "وضعیت :")]
        public ContactUsStatus Status { get; set; }

        [Display(Name = "تاریخ :")]
        public DateTime CreateDate { get; set; }
    }
}
