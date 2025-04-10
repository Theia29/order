using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrderModuleV2.Model;

namespace OrderModuleV2.Model
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("Order")]
        public int? OrderId { get; set; }  

        public Order? Order { get; set; }

        [Required]
        public int CustId { get; set; } 

        [Required]
        public int ItemId { get; set; } 

        [Required]
        public int ResId { get; set; } 

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        [NotMapped]
        public decimal TotalPrice => Price * Quantity;

       
    }
}
