using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Click_Cart.Models;

namespace Click_Cart.Areas.Common.Controllers
{
    public class HomeController : Controller
    {
        private string ProductURL = "https://localhost:7016/api/Product/";
        HttpClient client = new HttpClient();

        [Area("Common")]

        [HttpGet]
        public IActionResult Index()
        {
            //HttpContext.Session.Clear();
            //Response.Headers["Cache-Control"] = "no-cache, no-store";
            List<Product> product = new List<Product>();
            HttpResponseMessage response = client.GetAsync(ProductURL).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Product>>(content);
                if (data != null)
                {
                    product = data;
                }
            }
            return View(product);
        }
    }
}
