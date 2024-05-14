using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Domain.ViewModels.ContactUs.Admin
{
    public class ContactUsListViewModel
    {
        public int Id { get; set; }

        public ContactUsSubjectTitle subject { get; set; }

        public string Email { get; set; }

        public DateTime CreateDate { get; set; }

        public ContactUsStatus Status { get; set; }

        public FileTypeTitle? FileType { get; set; }
    }
}
