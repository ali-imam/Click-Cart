using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Click_Cart.Models;
using System.Text;

namespace Click_Cart.Areas.Customer.Controllers
{
        [Area("Customer")]
    public class UserHomeController : Controller
    {
        private readonly string CartURL = "https://localhost:7016/api/Cart/";
        private readonly string ProductURL = "https://localhost:7016/api/Product/";
        private readonly HttpClient client = new HttpClient();


        [HttpGet]
        public IActionResult Index()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store";
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


        [HttpPost]
        [ActionName("Index")]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            // Fetch product details
            //HttpResponseMessage productResponse = client.GetAsync($"{ProductURL}{productId}").Result;
            //if (!productResponse.IsSuccessStatusCode)
            //{
            //    return NotFound();
            //}

            //string productContent = productResponse.Content.ReadAsStringAsync().Result;
            //var product = JsonConvert.DeserializeObject<Product>(productContent);

            // Create Cart object
            var cartItem = new Cart
            {
                UserId = (int)HttpContext.Session.GetInt32("UserId"), // Replace with actual logged-in user ID
                ProductId = productId,
                Quantity = quantity
            };

            var jsonData = JsonConvert.SerializeObject(cartItem);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Send the request to the API
            HttpResponseMessage response = client.PostAsync(CartURL, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Cart");
            }
            return BadRequest();
        }
    }
}