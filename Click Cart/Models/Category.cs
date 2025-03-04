using System.ComponentModel.DataAnnotations;

namespace Click_Cart.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
