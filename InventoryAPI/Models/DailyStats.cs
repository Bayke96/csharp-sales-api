using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace InventoryAPI.Models
{
    [Table("daily_stats")]
    public class DailyStats
    {
        [Column("stats_id")]
        [Key]
        public int ID { get; private set; }

        [Column("date"), Index(IsUnique = true), Required]
        public DateTime salesDate { get; set; } = DateTime.Today;

        public int mostSold { get; set; }

        [Column("most_sold")]
        [ForeignKey("mostSold")]
        [JsonIgnore]
        public virtual Product Product { get; set; }

        [Column("items_sold"), Required]
        public int productsSold { get; set; }

    }
}