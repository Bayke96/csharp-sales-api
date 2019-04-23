using InventoryAPI.DB_Config;
using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Services
{
    public class MonthlyStatsServices
    {
        public List<MonthlyStats> GetMonthlyStats()
        {
            using (var context = new ServicesContext())
            {
                var allMonthlyStats = context.monthlyStats.OrderBy(b => b.ID).ToList();
                return allMonthlyStats;
            }
        }

        public MonthlyStats GetMonthlyStats(int id)
        {
            using (var context = new ServicesContext())
            {
                var monthlyStat = context.monthlyStats.SingleOrDefault(b => b.ID == id);
                return monthlyStat;
            }
        }

        public MonthlyStats CreateMonthlyStat(MonthlyStats stat)
        {
            using (var context = new ServicesContext())
            {
                context.monthlyStats.Add(stat);
                context.SaveChanges();
            }
            return stat;
        }

        public MonthlyStats UpdateMonthlyStat(int statID, MonthlyStats stat)
        {
            using (var context = new ServicesContext())
            {
                var selectedMStat = context.monthlyStats.SingleOrDefault(b => b.ID == statID);

                selectedMStat.salesDate = stat.salesDate;
                selectedMStat.productsSold = stat.productsSold;
                context.SaveChanges();

                return selectedMStat;
            }

        }

        public MonthlyStats DeleteMonthlyStat(int statID)
        {
            using (var context = new ServicesContext())
            {
                var selectedMStat = context.monthlyStats.SingleOrDefault(b => b.ID == statID);

                context.monthlyStats.Remove(selectedMStat);
                context.SaveChanges();

                return selectedMStat;
            }
        }

    }
}