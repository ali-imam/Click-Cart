using Microsoft.AspNetCore.Mvc;

namespace Click_Cart.Areas.Common.Controllers
{
    public class RegisterController : Controller
    {
        [Area("Common")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
