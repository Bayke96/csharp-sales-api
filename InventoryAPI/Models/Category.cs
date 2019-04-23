using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    [Table("category_list")]
    public class Category
    {
        [Key, Column("cat_id")]
        public int ID { get; private set; }
        
        [Column("cat_name"), Index(IsUnique = true)]
        [ConcurrencyCheck, Required, MinLength(3), MaxLength(50)]
        public string categoryName { get; set; }
        
        [Column("cat_products")]
        [Required]
        public int ammountProducts { get; set; }

    }
}