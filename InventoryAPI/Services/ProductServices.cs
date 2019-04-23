using InventoryAPI.DB_Config;
using InventoryAPI.Models;
using System;
using System.Collections.Generic;
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
                var allProducts = context.Products.OrderBy(b => b.ID).ToList();
                return allProducts;
            }
        }

        public Product GetProduct(int id)
        {
            using (var context = new ServicesContext())
            {
                var product = context.Products.SingleOrDefault(b => b.ID == id);
                return product;
            }
        }

        public Product CreateProduct(Product product)
        {
            using (var context = new ServicesContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
            return product;
        }

        public Product UpdateProduct(int productID, Product product)
        {
            using (var context = new ServicesContext())
            {
                var selectedProduct = context.Products.SingleOrDefault(b => b.ID == productID);

                selectedProduct.categoryID = product.categoryID;
                selectedProduct.productName = product.productName;
                selectedProduct.productDescription = product.productDescription;
                selectedProduct.productPrice = product.productPrice;
                selectedProduct.productAmmount = product.productAmmount;
                context.SaveChanges();

                return selectedProduct;
            }

        }

        public Product DeleteProduct(int productID)
        {
            using (var context = new ServicesContext())
            {
                var selectedProduct = context.Products.SingleOrDefault(b => b.ID == productID);

                context.Products.Remove(selectedProduct);
                context.SaveChanges();

                return selectedProduct;
            }
        }

    }
}