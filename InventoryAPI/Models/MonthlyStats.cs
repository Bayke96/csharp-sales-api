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

        [Column("month"), Required]
        public int salesMonth { get; set; } = DateTime.Now.Month;

        [Column("year"), Required]
        public int salesYear { get; set; } = DateTime.Now.Year;

        [Column("monthly_sales"), Required]
        public int productsSold { get; set; }

    }
}