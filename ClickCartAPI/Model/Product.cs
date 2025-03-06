using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClickCartAPI.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        [JsonIgnore]
        public Category? Category { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ProductImg { get; set; }
    }
}
