using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClickCartAPI.Model
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]

        [JsonIgnore]
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        [JsonIgnore]
        public Product? Product { get; set; }
        public DateTime? OrderDate { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
        public string SubTotal { get; set; }
    }
}
