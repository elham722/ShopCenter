using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.ViewModels.ContactUs.Admin
{
    public class ContactUsAnswerEmailViewModel
    {
        public string Email { get; set; }

        public string Massage { get; set; }

        public DateTime CreatDateTime { get; set; }

        public string Answer { get; set; }

        public string Subject { get; set; }
    }
}
