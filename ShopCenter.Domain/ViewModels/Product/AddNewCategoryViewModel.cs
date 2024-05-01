using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.ViewModels.Product
{
    public class AddNewCategoryViewModel
    {
       

        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [Display(Name = "نام دسته بندی جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CategoryTitle { get; set; }


        [Display(Name = "توضیحات دسته بندی ")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? Description { get; set; }

        [Display(Name = "تصویر دسته بندی ")]
        public IFormFile? ImageUrl { get; set; }

        public int? ParentId { get; set; }

        public string? ParentTitle { get; set; }
    }
}
