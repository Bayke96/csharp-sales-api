using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{

    [Table("product_list")]
    public class Product
    {
        [Column("p_id")]
        [Key]
        private int ID { get; set; }

        private Category Category { get; set; }

        [Column("c_id")]
        [ForeignKey("Category")]
        private int categoryID { get; set; }

        [Column("p_name"), Required]
        [Index(IsUnique = true), MinLength(3)]
        private string productName { get; set; }

        [Column("p_description"), Required]
        [MinLength(4), StringLength(255)]
        private string productDescription { get; set; }

        [Column("p_price"), Required]
        [Range(0.0, Double.MaxValue)]
        private decimal productPrice { get; set; }

        [Column("p_ammount")]
        [Range(0, Int32.MaxValue)]
        private int productAmmount { get; set; } = 0;

    }
}