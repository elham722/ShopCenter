using Microsoft.AspNetCore.Http;
using ShopCenter.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;


namespace ShopCenter.Application.StaticTools
{
    public static class FileTools
    {
        public static void AddToHard(IFormFile file, string fileName, SiteSections fileFor, out string fileNameWithExtention)
        {
            fileNameWithExtention = fileName + Path.GetExtension(file.FileName).ToLower();

            string filePath;

            if (FileIsImage(file))
            {
                filePath = GetPath(ImagePath(fileNameWithExtention, fileFor));
            }
            else
            {
                filePath = GetPath(VideoPath(fileNameWithExtention, fileFor));
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        public static void DeleteFromHard(string fileName, FileTypeTitle fileType, SiteSections fileFor)
        {
            string filePath = "";

            if (fileType == FileTypeTitle.Video)
            {
                filePath = GetPath(VideoPath(fileName, fileFor));
            }
            else
            {
                filePath = GetPath(ImagePath(fileName, fileFor));
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private static string GetPath(string[] path)
        {
            return Path.Combine(path);
        }

        private static string[] ImagePath(string fileName, SiteSections fileFor)
        {

            switch (fileFor)
            {
                case SiteSections.User:
                    return [Directory.GetCurrentDirectory(), "wwwroot", "images", "users", fileName];

                case SiteSections.ContactUs:
                    return [Directory.GetCurrentDirectory(), "wwwroot","assets", "images", "contactUs", fileName];

            }
            return null;

        }

        private static string[] VideoPath(string fileName, SiteSections fileFor)

        {
            switch (fileFor)
            {
                case SiteSections.ContactUs:
                    return [Directory.GetCurrentDirectory(), "wwwroot", "assets", "videos", "contactUs", fileName];

               
                   
            }
            return null;
        }

        public static bool FileIsImage(IFormFile file)
        {
            List<string> validExtentions = [".png", ".jpg", ".jpeg" , ".webp"];

            string extention = Path.GetExtension(file.FileName).ToLower();

            if (validExtentions.Contains(extention))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool FileIsVideo(IFormFile file)
        {
            List<string> validExtentions = [".mp4", ".mkv"];

            string extention = Path.GetExtension(file.FileName).ToLower();

            if (validExtentions.Contains(extention))
            {
                return true;
            }
            else
                return false;
        }

        public static bool FileSizeIsValid(IFormFile file, FileTypeTitle type)
        {
            var maxSize = 0;
            switch (type)
            {
                case FileTypeTitle.Image:
                    maxSize = 250000;//2M
                    break;
                case FileTypeTitle.Video:
                    maxSize = 5000000;//5M
                    break;
            }

            if (file.Length > maxSize)
            {
                return false;
            }
            return true;
        }

    }
}
