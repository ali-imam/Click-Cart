using Microsoft.AspNetCore.Mvc;

namespace Click_Cart.Areas.User.Controllers
{
    public class UserHomeController : Controller
    {
        [Area("User")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
