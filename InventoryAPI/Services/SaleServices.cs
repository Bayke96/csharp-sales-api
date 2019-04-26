using InventoryAPI.DB_Config;
using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace InventoryAPI.Services
{
    public class SaleServices
    {
        public List<Sale> GetSales()
        {
            using (var context = new ServicesContext())
            {
                var allSales = context.Sales.ToList();
                return allSales;
            }
        }

        public List<Sale> GetSaleOrder(string order, string orderBy)
        {
            List<Sale> allSales = new List<Sale>();

            using (var context = new ServicesContext())
            {
                if (order.ToUpper(CultureInfo.InvariantCulture) == "ASC")
                {
                    if (orderBy.ToUpperInvariant() == "ID")
                    {
                        allSales = context.Sales.OrderBy(b => b.ID).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "DATE")
                    {
                        allSales = context.Sales.OrderBy(b => b.saleDate).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "DESCRIPTION")
                    {
                        allSales = context.Sales.OrderBy(b => b.saleDescription).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "TOTAL")
                    {
                        allSales = context.Sales.OrderBy(b => b.saleTotal).ToList();
                    }
                }
                else
                {
                    if (orderBy.ToUpperInvariant() == "ID")
                    {
                        allSales = context.Sales.OrderByDescending(b => b.ID).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "DATE")
                    {
                        allSales = context.Sales.OrderByDescending(b => b.saleDate).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "DESCRIPTION")
                    {
                        allSales = context.Sales.OrderByDescending(b => b.saleDescription).ToList();
                    }
                    if (orderBy.ToUpperInvariant() == "TOTAL")
                    {
                        allSales = context.Sales.OrderByDescending(b => b.saleTotal).ToList();
                    }
                }
                return allSales;
            }
        }

        public Sale GetSale(int id)
        {
            using (var context = new ServicesContext())
            {
                var sale = context.Sales.FirstOrDefault(b => b.ID == id);
                return sale;
            }
        }

        public Sale CreateSale(Sale sale)
        {
            using (var context = new ServicesContext())
            {
                // Check if there's already a sales stat for this day.
                var findDailyStat = context.dailyStats.FirstOrDefault
                    (b => b.salesDate == DateTime.Now);

                // If there is, sum one to the counter.
                if (findDailyStat != null)
                {
                    findDailyStat.productsSold++;
                }
                // If there isn't, create a new stat for this day.
                else
                {
                    context.dailyStats.Add(new DailyStats());
                }

                // Sale object corrections
                sale.saleDescription = sale.saleDescription.Trim();

                context.Sales.Add(sale);
                context.SaveChanges();
                return sale;
            }
        }

        public Sale UpdateSale(int saleID, Sale sale)
        {
            using (var context = new ServicesContext())
            {
                var selectedSale = context.Sales.FirstOrDefault(b => b.ID == saleID);

                // Checking if the sale does exist within the database.
                if (selectedSale != null)
                {
                    selectedSale.saleDate = sale.saleDate;
                    selectedSale.saleDescription = sale.saleDescription.Trim();
                    selectedSale.saleTotal = sale.saleTotal;
                    context.SaveChanges();

                    return selectedSale;
                }
                // If it does not, return null.
                else
                {
                    return null;
                }
            }
        }

        public Sale DeleteSale(int saleID)
        {
            using (var context = new ServicesContext())
            {
                var selectedSale = context.Sales.FirstOrDefault(b => b.ID == saleID);

                // Checking if the category exists within the database.
                if (selectedSale != null)
                {
                    // Minus one from the counter of the daily and monthly sale stats.
                    var selectedDailyStat = context.dailyStats.FirstOrDefault(b => b.salesDate == selectedSale.saleDate);
                    var selectedMonthlyStat = context.monthlyStats.FirstOrDefault
                        (b => b.salesMonth == selectedSale.saleDate.Month && b.salesYear == selectedSale.saleDate.Year);

                    if (selectedDailyStat != null) selectedDailyStat.productsSold--;
                    if (selectedMonthlyStat != null) selectedMonthlyStat.productsSold--;

                    context.Sales.Remove(selectedSale);
                    context.SaveChanges();

                    return selectedSale;
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