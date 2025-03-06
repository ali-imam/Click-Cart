using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Click_Cart.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
    }
}
