using ShopCenter.Domain.ViewModels.ContactUs.Admin;
using ShopCenter.Domain.ViewModels.ContactUs.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Application.Services.Interface
{
    public interface IContactUsServices
    {
            AddContactUsReturns AddContactUs(ContactUsViewModel contactUs);

            List<ContactUsListViewModel> GetContactUsList();

            ContactUsAdminSideViewModel GetContactUs(int contactUsId);

            bool AnswerToContactUs(int contactUsId, string answer);

            bool ChangeContactUsStatus(int contactUsId);
       
    }
}
