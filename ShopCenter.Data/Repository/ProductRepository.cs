using Microsoft.EntityFrameworkCore;
using ShopCenter.Data.Context;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.Product;
using ShopCenter.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Data.Repository
{
    public class ProductRepository : IProductRepository
    {

        #region Ctor
        private ShopCenterDBContext _dbContext;
        public ProductRepository(ShopCenterDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        #endregion

        #region Crud
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
        }

        public async Task UpdateCategory(Category category)
        {
            _dbContext.Categories.Update(category);
        }


        public async Task AddProduct(Product product)
        {
            _dbContext.Products.AddAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
        }

        #endregion

        #region Category
        public async Task<List<AllCategoriesListViewModel>> GetAllCategories()
        {
            var categoris = await _dbContext.Categories.Where(c => !c.IsDelete).Select(c => new AllCategoriesListViewModel()
            {
                Id = c.Id,
                CategoryTitle = c.CategoryTitle,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                ParentId = c.ParentId,
                FirstCategoryParentTitleName = c.ParentId != null ? _dbContext.Categories.FirstOrDefault(p => p.Id == c.ParentId).CategoryTitle : "بدون والد",
            }).ToListAsync();

            foreach (var category in categoris)
            {
                if (category.ParentId != null)
                {
                    category.GrandParentId = _dbContext.Categories.FirstOrDefault(p => p.Id == category.ParentId)?.ParentId;
                    category.GrandCategoryTitleName = category.GrandParentId != null ? _dbContext.Categories.FirstOrDefault(p => p.Id == category.GrandParentId).CategoryTitle : "بدون والد";
                }
            }


            return categoris;

        }

        public async Task<List<Category>> GetAllMainCategories()
        {
            return await _dbContext.Categories.Where(c => c.ParentId == null).ToListAsync();
        }

        public async Task<List<Category>> GetAllSecondLevelCategories()
        {
            return await _dbContext.Categories
         .Where(c => c.ParentId != null &&
                     _dbContext.Categories.Any(parent => parent.Id == c.ParentId && parent.ParentId == null))
         .ToListAsync();
        }

        public bool IsExistMainCategoryById(int categoryId)
        {
            return _dbContext.Categories.Any(r => r.Id == categoryId);
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Category GetCategoryByIdSync(int id)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        }

        public async Task<bool> CategoryHasProduct(int categoryId)
        {
            return await _dbContext.Products
                         .Include(p => p.Category).ThenInclude(p => p.Parent)
                         .ThenInclude(p => p.Parent.Parent)
                         .Where(p => p.Category.Parent.Parent.Id == categoryId ||
                                     p.Category.Parent.Id == categoryId ||
                                     p.Category.Id == categoryId &&
                                     p.IsDelete == false).AnyAsync();

        }

        public async Task<EditCategoryViewModel> GetCategoryDetailsForShowEditByAdmin(int categoryId)
        {
            var category = await _dbContext.Categories.Where(c => !c.IsDelete).Select(c => new EditCategoryViewModel()
            {
                Id = c.Id,
                CategoryTitle = c.CategoryTitle,
                Description = c.Description,
                ImageName = c.ImageUrl,
                ParentId = c.ParentId,
                FirstCategoryParentTitleName = c.ParentId != null ? _dbContext.Categories.FirstOrDefault(p => p.Id == c.ParentId).CategoryTitle : "بدون والد",
            }).FirstOrDefaultAsync(r => r.Id == categoryId);

            if (category.ParentId != null)
            {
                category.GrandParentId = _dbContext.Categories.FirstOrDefault(p => p.Id == category.ParentId)?.ParentId;
                category.GrandCategoryTitleName = category.GrandParentId != null ? _dbContext.Categories.FirstOrDefault(p => p.Id == category.GrandParentId).CategoryTitle : "بدون والد";

            }
            return category;
        }

        public async Task<List<Category>> GetSubCategoryListForDelete(int parentId)
        {
            return await _dbContext.Categories.Where(c => c.ParentId == parentId).ToListAsync();
        }

        #endregion

    }
}
