using Microsoft.AspNetCore.Mvc;
using ShopCenter.Application.Generators;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Application.StaticTools;
using ShopCenter.Data.Migrations;
using ShopCenter.Data.Repository;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.Product;
using ShopCenter.Domain.Models.User;
using ShopCenter.Domain.ViewModels.Product;
using ShopCenter.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Application.Services.Implementation
{
    public class ProductServices : IProductServices
    {
        #region Ctor
        private IProductRepository _repository;
        public ProductServices(IProductRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Category

        public async Task<List<AllCategoriesListViewModel>> GetAllCategories()
        {
            List<AllCategoriesListViewModel> categories = await _repository.GetAllCategories();
            return categories;
        }
        public async Task<List<Category>> GetAllMainCategories()
        {
            List<Category> categories = await _repository.GetAllMainCategories();
            return categories;
        }

        public async Task<List<Category>> GetAllSecondLevelCategories()
        {
            List<Category> categories = await _repository.GetAllSecondLevelCategories();
            return categories;
        }

        public AddCategoryResult AddCategory(AddNewCategoryViewModel newCategory)
        {
            Category category = new Category()
            {
                CategoryTitle = newCategory.CategoryTitle,
                Description = newCategory.Description,
            };
            string picture;
            if (newCategory.ImageUrl != null)
            {
                if (!ImageTools.ImageExtensionIsValid(newCategory.ImageUrl))
                {
                    return AddCategoryResult.ImageExensionInvalid;
                }
                if (!ImageTools.ImageSizeIsValid(newCategory.ImageUrl))
                {
                    return AddCategoryResult.ImageSizeInvalid;
                }
                ImageTools.AddImageToHard(newCategory.ImageUrl, NameGenerator.GenerateUniqName(), "category", out picture);

                category.ImageUrl = picture;
            }

            if (newCategory.ParentId != null)
            {
                if (_repository.IsExistMainCategoryById(newCategory.ParentId.Value))
                {
                    category.ParentId = newCategory.ParentId.Value;
                }

            }

            _repository.AddCategory(category);
            _repository.Save();

            return AddCategoryResult.Done;
        }


        public EditCategoryResult EditCategory(EditCategoryViewModel editCategory)
        {
            var category = _repository.GetCategoryByIdSync(editCategory.Id);
            if (category != null)
            {
                category.CategoryTitle=editCategory.CategoryTitle;
                category.Description=editCategory.Description;
                if(editCategory.ParentId != null)
                {
                    if(category.ParentId != editCategory.ParentId)
                    {
                        category.ParentId=editCategory.ParentId;
                    }
                }
                if (editCategory.GrandParentId != null)
                {
                    if (category.ParentId != editCategory.GrandParentId)
                    {
                        category.ParentId = editCategory.GrandParentId;
                    }
                }
                if (editCategory.ImageUrl != null)
                {
                    if (!ImageTools.ImageExtensionIsValid(editCategory.ImageUrl))
                    {
                        return EditCategoryResult.ImageExensionInvalid;
                    }
                    if (!ImageTools.ImageSizeIsValid(editCategory.ImageUrl))
                    {
                        return EditCategoryResult.ImageSizeInvalid;
                    }

                    if (editCategory.ImageName != null && editCategory.ImageName != "default.jpg")
                    {
                        ImageTools.DeleteImageFromHard(editCategory.ImageName, "category");
                    }
                    string image = null;
                    ImageTools.AddImageToHard(editCategory.ImageUrl, NameGenerator.GenerateUniqName().ToString(), "category", out image);
                    editCategory.ImageName = image;
                    category.ImageUrl = editCategory.ImageName;
                }
            }
            _repository.UpdateCategory(category);
            _repository.Save();
            return EditCategoryResult.Done;

        }


        public async Task<bool> DeleteCategory(int id)
        {
            Category categoryToDelete = await _repository.GetCategoryById(id);

            if (categoryToDelete == null)
                return false;

            if (await _repository.CategoryHasProduct(categoryToDelete.Id))
            {
                return false;
            }
            await RecursiveDeleteSubCategories(categoryToDelete);

            categoryToDelete.IsDelete = true;
            if (categoryToDelete.ImageUrl != null)
            {

                ImageTools.DeleteImageFromHard(categoryToDelete.ImageUrl, "category");
               
            }
            _repository.UpdateCategory(categoryToDelete);
             _repository.Save();

            return true;
        }

        public async Task RecursiveDeleteSubCategories(Category category)
        {
            List<Category> subCategories = await _repository.GetSubCategoryListForDelete(category.Id);
            if (subCategories != null)
            {
                foreach (var subCategory in subCategories)
                {
                    await RecursiveDeleteSubCategories(subCategory);
                    subCategory.IsDelete = true;
                     _repository.UpdateCategory(subCategory);
                }
            }
        }

        public async Task<AddNewCategoryViewModel> GetCategoryByIdForAddSubCategory(int id)
        {
            var category = await _repository.GetCategoryById(id);
            if(category != null)
            {
                var newSubCategory = new AddNewCategoryViewModel()
                {
                    ParentId = id,
                    ParentTitle = category.CategoryTitle
                };
                return newSubCategory;
            }
            return null;
        }

        public async Task<EditCategoryViewModel> GetCategoryDetailsForShowEdit(int categoryId)
        {
            var category = await _repository.GetCategoryDetailsForShowEditByAdmin(categoryId);
            if(category == null)
            {
                return null;
            }
            return category;
        }

        #endregion
    }
}
