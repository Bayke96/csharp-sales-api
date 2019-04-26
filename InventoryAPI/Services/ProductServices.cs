using InventoryAPI.DB_Config;
using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace InventoryAPI.Services
{
    public class ProductServices
    {
        public List<Product> GetProduct()
        {
            using (var context = new ServicesContext())
            {
                var allProducts = context.Products.ToList();
                return allProducts;
            }
        }

        public List<Product> GetProductOrder(string order, string orderBy)
        {
            List<Product> allProducts = new List<Product>();

            using (var context = new ServicesContext())
            {
                if (order.ToUpper(CultureInfo.InvariantCulture) == "ASC")
                {
                    if (orderBy.ToUpperInvariant() == "ID")
                    {
                        allProducts = context.Products.OrderBy(b => b.ID).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "NAME")
                    {
                        allProducts = context.Products.OrderBy(b => b.productName).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "DESCRIPTION")
                    {
                        allProducts = context.Products.OrderBy(b => b.productDescription).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "PRICE")
                    {
                        allProducts = context.Products.OrderBy(b => b.productPrice).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "AMMOUNT")
                    {
                        allProducts = context.Products.OrderBy(b => b.productAmmount).ToList();
                    }
                }
                else
                {
                    if (orderBy.ToUpperInvariant() == "ID")
                    {
                        allProducts = context.Products.OrderByDescending(b => b.ID).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "NAME")
                    {
                        allProducts = context.Products.OrderByDescending(b => b.productName).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "DESCRIPTION")
                    {
                        allProducts = context.Products.OrderByDescending(b => b.productDescription).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "PRICE")
                    {
                        allProducts = context.Products.OrderByDescending(b => b.productPrice).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "AMMOUNT")
                    {
                        allProducts = context.Products.OrderByDescending(b => b.productAmmount).ToList();
                    }
                }
                return allProducts;
            }
        }

        public Product GetProduct(int id)
        {
            using (var context = new ServicesContext())
            {
                var product = context.Products.FirstOrDefault(b => b.ID == id);
                return product;
            }
        }

        public Product GetProduct(string name)
        {
            string searchName = name.ToUpper(CultureInfo.InvariantCulture).Trim();
            using (var context = new ServicesContext())
            {
                var product = context.Products.FirstOrDefault
                    (b => b.productName.ToUpper() == searchName);
                return product;
            }
        }

        public Product CreateProduct(Product product)
        {
            using (var context = new ServicesContext())
            {
                // Check if product already exists within database.
                var findExistingProduct = context.Products.FirstOrDefault
                    (b => b.productName.ToUpper() == product.productName.ToUpper());

                // Product object corrections
                product.productName = product.productName.Trim();
                product.productDescription = product.productDescription.Trim();

                // Product doesn't exist.
                if (findExistingProduct == null)
                {
                    // Sum one to the counter of the product's category
                    var category = context.Categories.Single(b => b.ID == product.categoryID);
                    category.ammountProducts++;

                    context.Products.Add(product);
                    context.SaveChanges();

                    var newestProduct = context.Products.ToArray().Last();
                    return newestProduct;
                }
                // Product already exists
                else
                {
                    return null;
                }
            }
        }

        public Product UpdateProduct(int productID, Product product)
        {
            using (var context = new ServicesContext())
            {
                var selectedProduct = context.Products.FirstOrDefault(b => b.ID == productID);

                // Checking if the product does exist within the database.
                if (selectedProduct != null)
                {
                    // Update category counters if a new category has been set for the product.
                    if(selectedProduct.categoryID != product.categoryID)
                    {
                        var oldCategory = context.Categories.Single(b => b.ID == selectedProduct.categoryID);
                        oldCategory.ammountProducts--;
                        var newCategory = context.Categories.Single(b => b.ID == product.categoryID);
                        newCategory.ammountProducts++;
                    }
                    
                    selectedProduct.categoryID = product.categoryID;
                    selectedProduct.productName = product.productName.Trim();
                    selectedProduct.productDescription = product.productDescription.Trim();
                    selectedProduct.productAmmount = product.productAmmount;
                    selectedProduct.productPrice = product.productPrice;
                    context.SaveChanges();

                    return selectedProduct;
                }
                // If it does not, return null.
                else
                {
                    return null;
                }
            }
        }

        public Product DeleteProduct(int productID)
        {
            using (var context = new ServicesContext())
            {
                var selectedProduct = context.Products.FirstOrDefault(b => b.ID == productID);

                // Checking if the category exists within the database.
                if (selectedProduct != null)
                {
                    // Minus one from the counter of the product's category.
                    var selectedCategory = context.Categories.FirstOrDefault(b => b.ID == selectedProduct.categoryID);
                    selectedCategory.ammountProducts--;

                    context.Products.Remove(selectedProduct);
                    context.SaveChanges();

                    return selectedProduct;
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