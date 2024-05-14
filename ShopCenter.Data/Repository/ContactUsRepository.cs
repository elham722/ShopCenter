using ShopCenter.Data.Context;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.ContactUs;
using ShopCenter.Domain.ViewModels.ContactUs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Data.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        private ShopCenterDBContext _context;

        public ContactUsRepository(ShopCenterDBContext context)
        {
            _context = context;
        }

        public void Add(ContactUs contactUs)
        {
            _context.Add(contactUs);
        }

        public List<ContactUsListViewModel> GetContactUsList()
        {
            return _context.ContactUs.Select(c => new ContactUsListViewModel()
            {
                Id = c.Id,
                Email = c.Email,
                subject = c.Subject,
                CreateDate = c.CreateDate,
                Status = c.Status,
                FileType = c.FileType

            }).OrderByDescending(c => c.Id).ToList();

        }

        public ContactUs GetContactUs(int id)
        {
            return _context.ContactUs.FirstOrDefault(c => c.Id == id);
        }

        public void Update(ContactUs contactUs)
        {
            _context.Update(contactUs);
        }

        public bool IsExistContactUs(int id)
        {
            return _context.ContactUs.Any(c => c.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }


    }

}
