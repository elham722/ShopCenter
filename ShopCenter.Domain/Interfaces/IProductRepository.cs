using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ShopCenter.Domain.Models.Product;
using ShopCenter.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.Interfaces
{
    public interface IProductRepository
    {
        #region Crud
        void Save();

        void AddCategory(Category category);

        Task UpdateCategory(Category category);

        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        #endregion

        #region Category
        Category GetCategoryByIdSync(int id);

        Task<List<Category>> GetSubCategoryListForDelete(int parentId);

        Task<List<AllCategoriesListViewModel>> GetAllCategories();

        Task<List<Category>> GetAllMainCategories();

        bool IsExistMainCategoryById(int categoryId);

        Task<Category> GetCategoryById(int id);

        Task<bool> CategoryHasProduct(int categoryId);

        Task<EditCategoryViewModel> GetCategoryDetailsForShowEditByAdmin(int categoryId);

        Task<List<Category>> GetAllSecondLevelCategories();

        #endregion


    }
}
