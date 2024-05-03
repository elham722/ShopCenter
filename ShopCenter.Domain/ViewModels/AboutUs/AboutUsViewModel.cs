using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.ViewModels.AboutUs
{
    public class AboutUsViewModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }


        public string? Location { get; set; }

        public string? ImageName { get; set; }

        public IFormFile? ImageUrl { get; set; }
    }
}
