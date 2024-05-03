using Microsoft.EntityFrameworkCore;
using ShopCenter.Domain.Models.AboutUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.Interfaces
{
    public interface IAboutUsRepository
    {
     
        Task<AboutUs> GetAllAboutUs();

        Task Add(AboutUs aboutUs);

        Task Save();

        void Update(AboutUs aboutUs);

        Task<AboutUs> IsExistAboutUs(int Id);
    }
}
