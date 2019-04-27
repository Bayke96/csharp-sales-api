using InventoryAPI.DB_Config;
using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Services
{
    public static class MonthlyStatsServices
    {
        public static List<MonthlyStats> GetMonthlyStats()
        {
            using (var context = new ServicesContext())
            {
                var allMonthlyStats = context.monthlyStats.ToList();
                return allMonthlyStats;
            }
        }

        public static MonthlyStats GetMonthlyStat(int id)
        {
            using (var context = new ServicesContext())
            {
                var monthlyStat = context.monthlyStats.FirstOrDefault(b => b.ID == id);
                return monthlyStat;
            }
        }

        public static MonthlyStats CreateMonthlyStat(MonthlyStats monthlyStat)
        {
            using (var context = new ServicesContext())
            {
                // Check if there's already a stat for this month.
                var findMonthlyStat = context.monthlyStats.FirstOrDefault
                    (b => b.salesMonth == monthlyStat.salesMonth && b.salesYear == monthlyStat.salesYear);

                // If there isn't, create one.
                if (findMonthlyStat == null)
                {
                    context.monthlyStats.Add(monthlyStat);
                    context.SaveChanges();
                    return monthlyStat;
                }
                // If there is, return null and let the controller handle it.
                else
                {
                    return null;
                }
            }
        }

        public static MonthlyStats UpdateMonthlyStat(int statID, MonthlyStats monthlyStat)
        {
            using (var context = new ServicesContext())
            {
                var selectedStat = context.monthlyStats.FirstOrDefault(b => b.ID == statID);

                // Checking if the stat does exist within the database.
                if (selectedStat != null)
                {
                    selectedStat.salesMonth = monthlyStat.salesMonth;
                    selectedStat.salesYear = monthlyStat.salesYear;
                    context.SaveChanges();

                    return selectedStat;
                }
                // If it does not, return null.
                else
                {
                    return null;
                }
            }
        }

        public static MonthlyStats DeleteMonthlyStat(int statID)
        {
            using (var context = new ServicesContext())
            {
                var selectedMonthlyStat = context.monthlyStats.FirstOrDefault(b => b.ID == statID);

                // Checking if the monthly stats exists within the database.
                if (selectedMonthlyStat != null)
                {
                    context.monthlyStats.Remove(selectedMonthlyStat);
                    context.SaveChanges();

                    return selectedMonthlyStat;
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