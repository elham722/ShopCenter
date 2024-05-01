using Microsoft.AspNetCore.Http;
using ShopCenter.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Application.StaticTools
{
    public static class ImageTools

    {  
        /// <summary>
     /// 
     /// </summary>
     /// <param name="imageFile"></param>
     /// <param name="imageName"></param>
     /// <param name="imageFor">user | contactus | product </param>
     /// <param name="imageNameWithExtention"></param>
        public static void AddImageToHard(IFormFile imageFile, string imageName, string imageFor, out string imageNameWithExtension)
        {
            imageNameWithExtension = imageName + Path.GetExtension(imageFile.FileName).ToLower();
            string imagePath = GetPath(ImagePath(imageNameWithExtension , imageFor));
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="imageFor">user | contactUs | product</param>  
        public static void DeleteImageFromHard(string imageName, string imageFor)
        {
            string imagePath = GetPath(ImagePath(imageName, imageFor));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        private static string GetPath(string[] path)
        {
            return Path.Combine(path);
        }

        private static string[] ImagePath(string fileName, string imageFor)
        {
          
            switch (imageFor.ToLower())
            {
                case "user":
                  return [Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "avatar", fileName];
                case "category":
                    return [Directory.GetCurrentDirectory(), "wwwroot", "Admin", "img", "Category", fileName];


            }
            return null;
            
        }

        public static bool ImageExtensionIsValid(IFormFile imageFile)
        {
            List<string> validExtensions = [".png", ".jpg", ".jpeg"];
            string extention = Path.GetExtension(imageFile.FileName).ToLower();
            if (validExtensions.Contains(extention)) { return true; }
            else
                return false;
        }
        public static bool ImageSizeIsValid(IFormFile imageFile)
        {
            var maxSize= 250000;//2M
            if(imageFile.Length > maxSize)
            {
                return false;
            }
                return true;
        }
        public static string[] ImageExtension()
        {
            return [".jpg" , ".jpeg", ".png"];
        }

    }
}
