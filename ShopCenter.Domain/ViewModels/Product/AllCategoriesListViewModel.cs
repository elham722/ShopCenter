using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.ViewModels.Product
{
    public class AllCategoriesListViewModel
    {
        public int Id { get; set; }

        [Display(Name ="نام دسته بندی")]
        public string CategoryTitle { get; set; }

        
        [Display(Name = "  توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "  تصویر")]
        public string? ImageUrl { get; set; }

       
        public int? ParentId { get; set; }

        [Display(Name = "   نام دسته بندی اصلی")]
        public string? FirstCategoryParentTitleName { get; set; }

        public int? GrandParentId { get; set; }

        [Display(Name = "   نام دسته بندی سطح دوم")]
        public string? GrandCategoryTitleName { get; set; }

    }
}
