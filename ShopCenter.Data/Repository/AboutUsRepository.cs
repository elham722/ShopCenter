using Microsoft.EntityFrameworkCore;
using ShopCenter.Data.Context;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.AboutUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Data.Repository
{
    public class AboutUsRepository: IAboutUsRepository
    {
        #region Ctor

        private ShopCenterDBContext _dbContext;
        public AboutUsRepository(ShopCenterDBContext dBContext)
        {
            _dbContext = dBContext; 
        }

        #endregion

        public async Task Add(AboutUs aboutUs)
        {
            await _dbContext.AboutUs.AddAsync(aboutUs);
        }

        public Task Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void  Update(AboutUs aboutUs)
        {
            _dbContext.AboutUs.Update(aboutUs);
        }

        public async Task<AboutUs> GetAllAboutUs()
        {
            return await _dbContext.AboutUs.FirstOrDefaultAsync();
        }

        public async Task<AboutUs> IsExistAboutUs(int Id)
        {
            return await _dbContext.AboutUs.FirstOrDefaultAsync(a => a.Id == Id);
        }

    }
}
