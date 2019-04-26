using InventoryAPI.DB_Config;
using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Services
{
    public class SaleServices
    {
        public List<Sale> GetSale()
        {
            using (var context = new ServicesContext())
            {
                var allSales = context.Sales.OrderBy(b => b.ID).ToList();
                return allSales;
            }
        }

        public Sale GetSale(int id)
        {
            using (var context = new ServicesContext())
            {
                var sale = context.Sales.SingleOrDefault(b => b.ID == id);
                return sale;
            }
        }

        public Sale CreateSale(Sale sale)
        {
            using (var context = new ServicesContext())
            {
                context.Sales.Add(sale);
                context.SaveChanges();
            }
            return sale;
        }

        public Sale UpdateSale(int saleID, Sale sale)
        {
            using (var context = new ServicesContext())
            {
                var selectedSale = context.Sales.SingleOrDefault(b => b.ID == saleID);

                selectedSale.saleDate = sale.saleDate;
                selectedSale.saleDescription = sale.saleDescription;
                selectedSale.saleTotal = sale.saleTotal;
                context.SaveChanges();

                return selectedSale;
            }

        }

        public Sale DeleteSale(int saleID)
        {
            using (var context = new ServicesContext())
            {
                var selectedSale = context.Sales.SingleOrDefault(b => b.ID == saleID);

                context.Sales.Remove(selectedSale);
                context.SaveChanges();

                return selectedSale;
            }
        }

    }
}