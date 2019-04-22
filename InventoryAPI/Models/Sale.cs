using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    [Table("sale_list")]
    public class Sale
    {
        [Column("s_id")]
        [Key]
        private int ID;

        [Column("s_date")]
        private DateTime saleDate { get; set; } = new DateTime();

        [Column("s_description"), Required]
        [MinLength(4), StringLength(255)]
        private string saleDescription { get; set; }

        [Column("s_total"), Required]
        [Range(0.0, Double.MaxValue)]
        private decimal saleTotal { get; set; }

    }
}