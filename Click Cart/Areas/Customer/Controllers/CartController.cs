﻿using Click_Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Click_Cart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly string CartURL = "https://localhost:7016/api/Cart/";
        private readonly string OrderURL = "https://localhost:7016/api/Order/";
        private readonly string ProductURL = "https://localhost:7016/api/Product/";
        private readonly HttpClient client = new HttpClient();

        // GET: Display the cart
        [HttpGet]
        public IActionResult Index()
        {
            List<Cart> cart = new List<Cart>();
            HttpResponseMessage response = client.GetAsync(CartURL).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Cart>>(content);
                if (data != null)
                {
                    cart = data.Where(i => i.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
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
                    product = data1.Where(p => cart.Any(i => i.ProductId == p.ProductId)).ToList();

                }
            }

            // Store data in ViewData
            ViewData["CartItems"] = cart;
            ViewData["Products"] = product;

            return View();
        }



        [HttpPost]
        public IActionResult Plus(int cartId)
        {
            HttpResponseMessage cartResponse = client.GetAsync($"{CartURL}{cartId}").Result;
            if (cartResponse.IsSuccessStatusCode)
            {
                string cartContent = cartResponse.Content.ReadAsStringAsync().Result;
                var cart = JsonConvert.DeserializeObject<Cart>(cartContent);
                if(cart != null)
                {
                    cart.Quantity += 1;
                    var jsonData = JsonConvert.SerializeObject(cart);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Send the request to the API
                    HttpResponseMessage response = client.PutAsync(CartURL + cartId, content).Result;
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult Minus(int cartId)
        {
            HttpResponseMessage cartResponse = client.GetAsync($"{CartURL}{cartId}").Result;
            if (cartResponse.IsSuccessStatusCode)
            {
                string cartContent = cartResponse.Content.ReadAsStringAsync().Result;
                var cart = JsonConvert.DeserializeObject<Cart>(cartContent);
                if (cart != null && cart.Quantity > 1)
                {
                    cart.Quantity -= 1;
                    var jsonData = JsonConvert.SerializeObject(cart);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Send the request to the API
                    HttpResponseMessage response = client.PutAsync(CartURL + cartId, content).Result;
                }
                else if(cart.Quantity <= 1)
                {
                    HttpResponseMessage response = client.DeleteAsync(CartURL + cartId).Result;
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult Remove(int cartId)
        {
            HttpResponseMessage response = client.DeleteAsync(CartURL + cartId).Result;
            if (response.IsSuccessStatusCode)
            {
                //TempData["success"] = "Category deleted successfully!";
                return RedirectToAction("Index");
            }
            return NotFound();
        }




        [HttpPost]
        public IActionResult Order()
        {
            List<Cart> cart = new List<Cart>();
            HttpResponseMessage response = client.GetAsync(CartURL).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Cart>>(content);
                if (data != null)
                {
                    cart = data.Where(i => i.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
                }
                
            }
            // Generate a unique OrderCode for this order
            string orderCode = "ORD" + DateTime.Now.Ticks;

            foreach (var cartitem in cart)
            {
                // Create Order object
                var orderItem = new Order
                {
                    UserId = (int)HttpContext.Session.GetInt32("UserId"), // Replace with actual logged-in user ID
                    ProductId = cartitem.ProductId,
                    Quantity = cartitem.Quantity,
                    OrderDate = DateTime.Today,
                    Status = "In Progress",
                    OrderCode = orderCode  // Use the same OrderCode for all products in this order
                };




                var jsonData = JsonConvert.SerializeObject(orderItem);
                StringContent content1 = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send the request to the API
                HttpResponseMessage response1 = client.PostAsync(OrderURL, content1).Result;

                if (!response1.IsSuccessStatusCode)
                {
                    return BadRequest($"Failed to place order for Product ID {cartitem.ProductId}");
                }

                //Delete the cart item after placing the order
                HttpResponseMessage deleteResponse = client.DeleteAsync($"{CartURL}+{cartitem.CartId}").Result;
                if (!deleteResponse.IsSuccessStatusCode)
                {
                    return BadRequest($"Failed to remove cart item ID {cartitem.CartId} after ordering.");
                }
            }
            TempData["success"] = "Order placed successfully!";
            return RedirectToAction("Index","Order");
        }




}
}
