using Microsoft.CodeAnalysis;
using ShopCenter.Application.Generators;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Application.StaticTools;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.AboutUs;
using ShopCenter.Domain.ViewModels.AboutUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Application.Services.Implementation
{
    public class AboutUsServices : IAboutUsServices
    {
        #region Ctor
        private IAboutUsRepository _aboutUsRepository;
        public AboutUsServices(IAboutUsRepository aboutUsRepository)
        {
            _aboutUsRepository = aboutUsRepository;
        }
        #endregion

        public async Task<AboutUsViewModel> GetAboutUs()
        {
            var aboutUs = await _aboutUsRepository.GetAllAboutUs();
           
            var newAbout = new AboutUsViewModel()
            {
                Description = aboutUs is not null ? aboutUs.Description : "",
                Title = aboutUs is not null ? aboutUs.Title : "",
                ImageName = aboutUs is not null ? aboutUs.ImageUrl : "",
                Id = aboutUs is not null ? aboutUs.Id : 0,
                Location = aboutUs is not null ? aboutUs.Location : "",
          
            };
            return newAbout;

        }

        public async Task<AboutUsViewModel> AddOrEditAboutUs(AboutUsViewModel aboutUs)
        {
            var aboutusExist = await _aboutUsRepository.IsExistAboutUs(aboutUs.Id);
            if (aboutusExist != null)
            {
                aboutusExist.Title=aboutUs.Title;
                aboutusExist.Description = aboutUs.Description;
                aboutusExist.Location = aboutUs.Location;
               
                if (aboutUs.ImageUrl != null)
                {
                    if (aboutUs.ImageName != null && aboutUs.ImageName != "default.jpg")
                    {
                        ImageTools.DeleteImageFromHard(aboutUs.ImageName, "aboutus");
                    }
                    string picture=null;
                    ImageTools.AddImageToHard(aboutUs.ImageUrl, NameGenerator.GenerateUniqName().ToString(), "aboutus", out picture);

                    aboutUs.ImageName = picture;
                    aboutusExist.ImageUrl = aboutUs.ImageName;
                }

                _aboutUsRepository.Update(aboutusExist);
                await _aboutUsRepository.Save();
                return aboutUs;

            }




            var newAboutUs = new AboutUs()
            {
                Description = aboutUs.Description,
                Title = aboutUs.Title,
                Location = aboutUs.Location,
            };

            
            if (aboutUs.ImageUrl != null)
            {
            
                if (aboutUs.ImageName != null && aboutUs.ImageName != "default.jpg")
                {
                    ImageTools.DeleteImageFromHard(aboutUs.ImageName, "aboutus");
                }
                string image = null;
                ImageTools.AddImageToHard(aboutUs.ImageUrl, NameGenerator.GenerateUniqName().ToString(), "aboutus", out image);
                aboutUs.ImageName = image;
                newAboutUs.ImageUrl = aboutUs.ImageName;
            }

            await _aboutUsRepository.Add(newAboutUs);
            await _aboutUsRepository.Save();
            return aboutUs;

        }
    }
}
