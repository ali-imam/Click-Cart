using Click_Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Click_Cart.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        private readonly string CartURL = "https://localhost:7016/api/Cart/";
        private readonly string OrderURL = "https://localhost:7016/api/Order/";
        private readonly string ProductURL = "https://localhost:7016/api/Product/";
        private readonly HttpClient client = new HttpClient();


        [Area("Customer")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Order> order = new List<Order>();
            HttpResponseMessage response = client.GetAsync(OrderURL).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Order>>(content);
                if (data != null)
                {
                    order = data.Where(i => i.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
                }
            }

            List<Product> product = new List<Product>();
            HttpResponseMessage response1 = client.GetAsync(ProductURL).Result;
            if (response1.IsSuccessStatusCode)
            {
                string content1 = response1.Content.ReadAsStringAsync().Result;
                var data1 = JsonConvert.DeserializeObject<List<Product>>(content1);
                if (data1 != null)
                {
                    product = data1.Where(p => order.Any(i => i.ProductId == p.ProductId)).ToList();

                }
            }

            // Store data in ViewData
            ViewData["OrderItems"] = order;
            ViewData["Products"] = product;

            return View();
        }
    }
}
