using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ShopCenter.Application.Services.Implementation;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Data.Migrations;
using ShopCenter.Domain.Models.Product;
using ShopCenter.Domain.Models.User;
using ShopCenter.Domain.ViewModels.Product;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        #region Ctor

        private IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        #endregion

        #region Categories

        public async Task<IActionResult> GetAllCategories(string categoryTitle)
        {
            List<AllCategoriesListViewModel> allCategories = await _productServices.GetAllCategories();
            if (!string.IsNullOrEmpty(categoryTitle))
            {
                allCategories = allCategories.Where(u => u.CategoryTitle.Contains(categoryTitle)).ToList();
            }
          
            return View(allCategories);
        }

        [Route("addnewcategory")]
        public async Task<IActionResult> AddNewCategory()
        {
            ViewData["MainCategories"] = await _productServices.GetAllMainCategories();
            return PartialView("_AddCategory");
        }

        [HttpPost("submitaddnewcategory")]
        public IActionResult SubmitAddNewCategory(AddNewCategoryViewModel newCategory)
        {
            var result = _productServices.AddCategory(newCategory);
            switch (result)
            {
                case AddCategoryResult.ImageSizeInvalid:
                    return new JsonResult(new { status = "ImageSizeInvalid" });

                case AddCategoryResult.ImageExensionInvalid:
                    return new JsonResult(new { status = "ImageExensionInvalid" });

                case AddCategoryResult.Done:
                    return new JsonResult(new { status = "Success" });
            }
        
            return new JsonResult(new { status = "Error" });
            
        }


        [HttpGet("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {

                var result = await _productServices.DeleteCategory(categoryId);

                if (result)
                {
                    return new JsonResult(new { status = "Success" });
                }

                return new JsonResult(new { status = "Error" });
            
        }


        [Route("AddSubNewCategory")]
        public async Task<IActionResult> AddSubNewCategory(int CategoryId)
        {
           var parentCategory = await _productServices.GetCategoryByIdForAddSubCategory(CategoryId);
            return PartialView("_AddSubCategory", parentCategory);
        }

        [HttpPost("SubmitAddSubNewCategory")]
        public IActionResult SubmitAddSubNewCategory(AddNewCategoryViewModel newCategory)
        {

            var result = _productServices.AddCategory(newCategory);
            switch (result)
            {
                case AddCategoryResult.ImageSizeInvalid:
                    return new JsonResult(new { status = "ImageSizeInvalid" });

                case AddCategoryResult.ImageExensionInvalid:
                    return new JsonResult(new { status = "ImageExensionInvalid" });

                case AddCategoryResult.Done:
                    return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }



        [Route("EditCategory")]
        public async Task<IActionResult> EditCategory(int CategoryId)
        {
            ViewData["MainCategories"] = await _productServices.GetAllMainCategories();
            ViewData["SubCategories"] = await _productServices.GetAllSecondLevelCategories();
            var category= await _productServices.GetCategoryDetailsForShowEdit(CategoryId);

            return PartialView("_EditCategory", category);
        }


        [HttpPost("SubmitEditCategory")]
        public IActionResult SubmitEditCategory(EditCategoryViewModel editCategory)
        {

            var result = _productServices.EditCategory(editCategory);
            switch (result)
            {
                case EditCategoryResult.ImageSizeInvalid:
                    return new JsonResult(new { status = "ImageSizeInvalid" });

                case EditCategoryResult.ImageExensionInvalid:
                    return new JsonResult(new { status = "ImageExensionInvalid" });

                case EditCategoryResult.Done:
                    return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }


        #endregion
    }
}
