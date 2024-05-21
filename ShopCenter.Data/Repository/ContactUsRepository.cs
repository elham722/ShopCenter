using Microsoft.EntityFrameworkCore;
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
        #region Ctor
        private ShopCenterDBContext _context;

        public ContactUsRepository(ShopCenterDBContext context)
        {
            _context = context;
        }
        #endregion

        public async Task Add(ContactUs contactUs)
        {
           await _context.AddAsync(contactUs);
        }

        public async Task<List<ContactUsListViewModel>> GetContactUsList()
        {
            return  _context.ContactUs.Select(c => new ContactUsListViewModel()
            {
                Id = c.Id,
                Email = c.Email,
                subject = c.Subject,
                CreateDate = c.CreateDate,
                Status = c.Status,
                FileType = c.FileType

            }).OrderByDescending(c => c.Id).ToList();

        }

        public async Task<ContactUs> GetContactUs(int id)
        {
            return await _context.ContactUs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(ContactUs contactUs)
        {
            _context.Update(contactUs);
        }

        public async Task<bool> IsExistContactUs(int id)
        {
            return await _context.ContactUs.AnyAsync(c => c.Id == id);
        }

        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }


    }

}
