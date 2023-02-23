using System.ComponentModel.DataAnnotations;

namespace Pryaniky.Models
{
    public class PryanikyItem
    {
        public long Id { get; set; }
        [Required]
        public string Product { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
