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
        public int ID { get; private set; }

        [Column("month"), Index(IsUnique = true), Required]
        [MinLength(3), MaxLength(50)]
        public string salesDate { get; set; } = DateTime.Now.ToString("MMM-YYYY");

        [Column("monthly_sales"), Required]
        public int productsSold { get; set; }

    }
}