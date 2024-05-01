using ShopCenter.Domain.Models.Product;
using ShopCenter.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Application.Services.Interface
{
    public interface IProductServices
    {
       
        #region Category

        Task<List<AllCategoriesListViewModel>> GetAllCategories();
        Task<List<Category>> GetAllMainCategories();
        AddCategoryResult AddCategory(AddNewCategoryViewModel newCategory);

        Task<AddNewCategoryViewModel> GetCategoryByIdForAddSubCategory(int id);

        Task<EditCategoryViewModel> GetCategoryDetailsForShowEdit(int categoryId);
        Task<List<Category>> GetAllSecondLevelCategories();

        EditCategoryResult EditCategory(EditCategoryViewModel editCategory);
        Task<bool> DeleteCategory(int id);
        Task RecursiveDeleteSubCategories(Category category);

        #endregion
    }
}
