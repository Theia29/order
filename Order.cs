using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using OrderModuleV2.Model;

namespace OrderModuleV2.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int CustId { get; set; } 

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; } 

        [Required]
        public string OrderStatus { get; set; } = "Pending";

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Cart> CartItems { get; set; } 

    }
}
