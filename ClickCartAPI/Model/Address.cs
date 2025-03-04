using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ClickCartAPI.Model
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        [JsonIgnore]
        public User? User { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
    }
}
