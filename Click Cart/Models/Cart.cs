using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace Click_Cart.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? User { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
