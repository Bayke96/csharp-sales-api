using InventoryAPI.DB_Config;
using InventoryAPI.Models;
using Newtonsoft.Json;
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
                var allCategories = context.Categories.ToList();
                return allCategories;
            }
        }

        public Category GetCategory(int id)
        {
            using(var context = new ServicesContext())
            {
                var category = context.Categories.FirstOrDefault(b => b.ID == id);
                return category;
            }
        }

        public Category CreateCategory(Category category)
        {
            using(var context = new ServicesContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();

                var newestCategory = context.Categories.ToArray().Last();
                return newestCategory;
            }
        }

        public Category UpdateCategory(int categoryID, Category category)
        {
            using(var context = new ServicesContext())
            {
                var selectedCategory = context.Categories.FirstOrDefault(b => b.ID == categoryID);

                if(selectedCategory != null)
                {
                    selectedCategory.categoryName = category.categoryName;
                    selectedCategory.ammountProducts = category.ammountProducts;
                    context.SaveChanges();
                }

                return selectedCategory;
            }
            
        }

        public Category DeleteCategory(int categoryID)
        {
            using(var context = new ServicesContext())
            {
                var selectedCategory = context.Categories.FirstOrDefault(b => b.ID == categoryID);

                if(selectedCategory != null)
                {
                    context.Categories.Remove(selectedCategory);
                    context.SaveChanges();
                }

                return selectedCategory;
            }
        }

    }
}