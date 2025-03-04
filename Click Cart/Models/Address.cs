using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Click_Cart.Models
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
