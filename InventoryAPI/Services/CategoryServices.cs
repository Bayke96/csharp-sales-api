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

        public List<Category> GetCategoryOrder(string order, string orderBy)
        {
            List<Category> allCategories = new List<Category>();

            using(var context = new ServicesContext())
            {
                if (order.ToUpper() == "ASC")
                {
                    if (orderBy.ToUpperInvariant() == "ID")
                    {
                        allCategories = context.Categories.OrderBy(b => b.ID).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "NAME")
                    {
                        allCategories = context.Categories.OrderBy(b => b.categoryName).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "AMMOUNT")
                    {
                        allCategories = context.Categories.OrderBy(b => b.ammountProducts).ToList();
                    }
                }
                else
                {
                    if (orderBy.ToUpperInvariant() == "ID")
                    {
                        allCategories = context.Categories.OrderByDescending(b => b.ID).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "NAME")
                    {
                        allCategories = context.Categories.OrderByDescending(b => b.categoryName).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "AMMOUNT")
                    {
                        allCategories = context.Categories.OrderByDescending(b => b.ammountProducts).ToList();
                    }
                }
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

        public Category GetCategory(string name)
        {
            var searchName = name.ToUpper().Trim();
            using (var context = new ServicesContext())
            {
                var category = context.Categories.FirstOrDefault
                    (b => b.categoryName.ToUpper() == searchName);
                return category;
            }
        }

        public Category CreateCategory(Category category)
        {
            using(var context = new ServicesContext())
            {
                // Check if category already exists within database.
                var findExistingCategory = context.Categories.FirstOrDefault
                    (b => b.categoryName.ToUpper() == category.categoryName.ToUpper());

                // Category object corrections
                category.categoryName = category.categoryName.Trim();

                // Category doesn't exist.
                if (findExistingCategory == null)
                {
                    context.Categories.Add(category);
                    context.SaveChanges();

                    var newestCategory = context.Categories.ToArray().Last();
                    return newestCategory;
                }
                // Category already exists
                else
                {
                    return null;
                }
            }
        }

        public Category UpdateCategory(int categoryID, Category category)
        {
            using (var context = new ServicesContext())
            {
                var selectedCategory = context.Categories.FirstOrDefault(b => b.ID == categoryID);

                // Checking if the category does exist within the database.
                if(selectedCategory != null)
                {
                    selectedCategory.categoryName = category.categoryName.Trim();
                    selectedCategory.ammountProducts = category.ammountProducts;
                    context.SaveChanges();

                    return selectedCategory;
                }
                // If it does not, return null.
                else
                {
                    return null;
                }
            }
        }

        public Category DeleteCategory(int categoryID)
        {
            using(var context = new ServicesContext())
            {
                var selectedCategory = context.Categories.FirstOrDefault(b => b.ID == categoryID);

                // Checking if the category exists within the database.
                if(selectedCategory != null)
                {
                    context.Categories.Remove(selectedCategory);
                    context.SaveChanges();

                    return selectedCategory;
                }
                // If it does not, return null.
                else
                {
                    return null;
                }
            }
        }
    }
}