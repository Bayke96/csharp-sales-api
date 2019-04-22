using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    [Table("daily_stats")]
    public class DailyStats
    {
        [Column("stats_id")]
        [Key]
        private int ID { get; set; }

        [Column("date"), Index(IsUnique = true)]
        private DateTime salesDate { get; set; } = DateTime.Today;

        private Product Product { get; set; }

        [Column("most_sold")]
        [ForeignKey("Product")]
        private int mostSold { get; set; }

        [Column("items_sold"), Required]
        private int productsSold { get; set; }

    }
}