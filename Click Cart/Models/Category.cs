using System.ComponentModel.DataAnnotations;

namespace Click_Cart.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, MinLength(3), MaxLength(15)]
        public string CategoryName { get; set; }
    }
}
