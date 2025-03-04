using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClickCartAPI.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        [JsonIgnore]
        public User? User { get; set; }
        public DateTime? OrderDate { get; set; } = DateTime.Now;
        public string TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
