using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Click_Cart.Models;
using System.Text;
using Click_Cart.Helpers;

namespace Click_Cart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderDetailsController : Controller
    {
        private string OrderURL = "https://localhost:7016/api/Order/";
        HttpClient client = new HttpClient();




        [HttpGet]
        public IActionResult Index()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store";
            List<Order> order = new List<Order>();
            HttpResponseMessage response = client.GetAsync(OrderURL).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Order>>(content);
                if (data != null)
                {
                    order = data;
                }
            }
            return View(order);
        }



        //EDIT
        [HttpGet]
        public IActionResult EditOrder(string tp)
        {
            Order order = new Order();
            var id = int.Parse(EncryptionHelper.Decrypt(tp));
            HttpResponseMessage response = client.GetAsync(OrderURL + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Order>(content);

                if (data != null)
                {
                    order = data;
                    ViewData["OrderData"] = order;
                }

            }
            return View(order);
        }


        [HttpPost]
        public IActionResult EditOrder(Order order)
        {
            if (ModelState.IsValid)
            {

                var data = JsonConvert.SerializeObject(order);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(OrderURL + order.OrderId, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //TempData["success"] = "Order updated successfully!";
                    return RedirectToAction("Index", "OrderDetails");
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid Details");
                return RedirectToAction("EditOrder", "OrderDetails");
            }
        }

    }
}
