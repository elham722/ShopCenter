using ShopCenter.Domain.ViewModels.AboutUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Application.Services.Interface
{
    public interface IAboutUsServices
    {
        Task<AboutUsViewModel> GetAboutUs();
        Task<AboutUsViewModel> AddOrEditAboutUs(AboutUsViewModel aboutUs);
    }
}
