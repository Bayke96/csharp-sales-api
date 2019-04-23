using InventoryAPI.DB_Config;
using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Services
{
    public class CategoryServices
    {

        public List<Category> GetCategory()
        {
            using(var context = new ServicesContext())
            {
                var allCategories = context.Categories.OrderBy(b => b.ID).ToList();
                return allCategories;
            }
        }

        public Category GetCategory(int id)
        {
            using(var context = new ServicesContext())
            {
                var category = context.Categories.SingleOrDefault(b => b.ID == id);
                return category;
            }
        }

        public Category CreateCategory(Category category)
        {
            using(var context = new ServicesContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
            return category;
        }

        public Category UpdateCategory(int categoryID, Category category)
        {
            using(var context = new ServicesContext())
            {
                var selectedCategory = context.Categories.SingleOrDefault(b => b.ID == categoryID);

                selectedCategory.categoryName = category.categoryName;
                selectedCategory.ammountProducts = category.ammountProducts;
                context.SaveChanges();

                return selectedCategory;
            }
            
        }

        public Category DeleteCategory(int categoryID)
        {
            using(var context = new ServicesContext())
            {
                var selectedCategory = context.Categories.SingleOrDefault(b => b.ID == categoryID);

                context.Categories.Remove(selectedCategory);
                context.SaveChanges();

                return selectedCategory;
            }
        }

    }
}