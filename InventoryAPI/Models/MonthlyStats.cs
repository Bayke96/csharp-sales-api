using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    [Table("monthly_stats")]
    public class MonthlyStats
    {
        [Column("stats_id")]
        [Key]
        private int ID { get; set; }

        [Column("month"), Index(IsUnique = true)]
        private string salesDate { get; set; } = DateTime.Now.ToString("MMM-YYYY");

        [Column("monthly_sales"), Required]
        private int productsSold { get; set; }

    }
}