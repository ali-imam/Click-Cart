using System.ComponentModel.DataAnnotations;

namespace Click_Cart.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, MinLength(3), MaxLength(25)]
        public string CategoryName { get; set; }
    }
}
