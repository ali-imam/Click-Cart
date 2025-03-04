using System.ComponentModel.DataAnnotations;

namespace ClickCartAPI.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
