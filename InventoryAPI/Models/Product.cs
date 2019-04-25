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

    [Table("product_list")]
    public class Product
    {
        [Column("p_id")]
        [Key]
        public int ID { get; private set; }

        [Column("c_id")]
        public int categoryID { get; set; }

        [ForeignKey("categoryID")]
        [JsonIgnore]
        public virtual Category Category { get; set; }

        [Column("p_name"), Required]
        [Index(IsUnique = true), MinLength(3), MaxLength(255)]
        public string productName { get; set; }

        [Column("p_description"), Required]
        [MinLength(4), StringLength(255)]
        public string productDescription { get; set; }

        [Column("p_price"), Required]
        [Range(0.0, Double.MaxValue)]
        public decimal productPrice { get; set; }

        [Column("p_ammount")]
        [Range(0, Int32.MaxValue)]
        public int productAmmount { get; set; } = 0;

        public Product()
        {

        }

    }
}