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
            void Add(ContactUs contactUs);

            ContactUs GetContactUs(int id);

            List<ContactUsListViewModel> GetContactUsList();

            void Update(ContactUs contactUs);

            bool IsExistContactUs(int id);

            void Save();
        }
    
}
