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
        public static List<DailyStats> GetDailyStats()
        {
            using (var context = new ServicesContext())
            {
                var allDailyStats = context.dailyStats.ToList();
                return allDailyStats;
            }
        }

        public static DailyStats GetDailyStat(int id)
        {
            using (var context = new ServicesContext())
            {
                var dailyStat = context.dailyStats.FirstOrDefault(b => b.ID == id);
                return dailyStat;
            }
        }

        public static DailyStats CreateDailyStat(DailyStats dailyStat)
        {
            using (var context = new ServicesContext())
            {
                // Check if there's already a daily stat for this day.
                var findDailyStat = context.dailyStats.FirstOrDefault
                    (b => b.salesDate == dailyStat.salesDate);

                // If there isn't, create one.
                if (findDailyStat == null)
                {
                    context.dailyStats.Add(dailyStat);
                    context.SaveChanges();
                    return dailyStat;
                }
                // If there is, return null and let the controller handle it.
                else
                {
                    return null;
                }
            }
        }

        public static DailyStats UpdateDailyStat(int statID, DailyStats dailyStat)
        {
            using (var context = new ServicesContext())
            {
                var selectedStat = context.dailyStats.FirstOrDefault(b => b.ID == statID);

                // Checking if the stat does exist within the database.
                if (selectedStat != null)
                {
                    selectedStat.salesDate = dailyStat.salesDate;
                    selectedStat.productsSold = dailyStat.productsSold;
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

        public static DailyStats DeleteDailyStat(int statID)
        {
            using (var context = new ServicesContext())
            {
                var selectedDailyStat = context.dailyStats.FirstOrDefault(b => b.ID == statID);

                // Checking if the daily stats exists within the database.
                if (selectedDailyStat != null)
                {
                    // Remove daily sales from the monthly stats counter.
                    var selectedMonthlyStat = context.monthlyStats.FirstOrDefault
                        (b => b.salesMonth == selectedDailyStat.salesDate.Month && 
                        b.salesYear == selectedDailyStat.salesDate.Year);

                    if (selectedMonthlyStat != null) { selectedMonthlyStat.productsSold -= selectedDailyStat.productsSold; }

                    context.dailyStats.Remove(selectedDailyStat);
                    context.SaveChanges();

                    return selectedDailyStat;
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