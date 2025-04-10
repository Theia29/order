using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderModuleV2.Models
{
    public class CartRequestDto
    {
        [Required]
        public int CustId { get; set; }
        [Required]
        public List<int> ItemIds { get; set; }
        [Required]
        public List<int> ResIds { get; set; }
        [Required]
        public List<int> Quantities { get; set; }
        [Required]
        public List<decimal> Prices { get; set; }
    }
}