using System.ComponentModel.DataAnnotations;

namespace ClickCartAPI.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, MinLength(3), MaxLength(15)]
        public string CategoryName { get; set; }
    }
}
