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
            Task<AddContactUsReturns> AddContactUs(ContactUsViewModel contactUs);

            Task<List<ContactUsListViewModel>> GetContactUsList();

        Task<ContactUsAdminSideViewModel> GetContactUs(int contactUsId);

            Task<bool> AnswerToContactUs(int contactUsId, string answer);

            Task<bool> ChangeContactUsStatus(int contactUsId);
       
    }
}
