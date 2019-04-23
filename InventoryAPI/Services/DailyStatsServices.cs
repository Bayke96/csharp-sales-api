using InventoryAPI.DB_Config;
using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Services
{
    public class DailyStatsServices
    {

        public List<DailyStats> GetDailyStats()
        {
            using (var context = new ServicesContext())
            {
                var allDailyStats = context.dailyStats.OrderBy(b => b.ID).ToList();
                return allDailyStats;
            }
        }

        public DailyStats GetDailyStats(int id)
        {
            using (var context = new ServicesContext())
            {
                var dailyStat = context.dailyStats.SingleOrDefault(b => b.ID == id);
                return dailyStat;
            }
        }

        public DailyStats CreateDailyStat(DailyStats stat)
        {
            using (var context = new ServicesContext())
            {
                context.dailyStats.Add(stat);
                context.SaveChanges();
            }
            return stat;
        }

        public DailyStats UpdateDailyStat(int statID, DailyStats stat)
        {
            using (var context = new ServicesContext())
            {
                var selectedDStat = context.dailyStats.SingleOrDefault(b => b.ID == statID);

                selectedDStat.salesDate = stat.salesDate;
                selectedDStat.Product = stat.Product;
                selectedDStat.mostSold = stat.mostSold;
                selectedDStat.productsSold = stat.productsSold;

                context.SaveChanges();
                return selectedDStat;
            }

        }

        public DailyStats DeleteDailyStat(int statID)
        {
            using (var context = new ServicesContext())
            {
                var selectedDStat = context.dailyStats.SingleOrDefault(b => b.ID == statID);

                context.dailyStats.Remove(selectedDStat);
                context.SaveChanges();

                return selectedDStat;
            }
        }

    }
}