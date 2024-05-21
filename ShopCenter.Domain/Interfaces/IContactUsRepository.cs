using ShopCenter.Domain.Models.ContactUs;
using ShopCenter.Domain.ViewModels.ContactUs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.Interfaces
{
    public interface IContactUsRepository
    {
            Task Add(ContactUs contactUs);

            Task<ContactUs> GetContactUs(int id);

            Task<List<ContactUsListViewModel>> GetContactUsList();

            void Update(ContactUs contactUs);

            Task<bool> IsExistContactUs(int id);

            Task Save();
        }
    
}
