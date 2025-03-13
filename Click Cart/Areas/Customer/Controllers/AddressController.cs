using Click_Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Click_Cart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AddressController : Controller
    {

        private readonly string CartURL = "https://localhost:7016/api/Cart/";
        private readonly string AddressURL = "https://localhost:7016/api/Address/";
        private readonly HttpClient client = new HttpClient();


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAddress(Address address)
        {
             address.UserId = (int)HttpContext.Session.GetInt32("UserId"); // Replace with actual logged-in user ID
            if (ModelState.IsValid)
            {

                var data = JsonConvert.SerializeObject(address);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(AddressURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Address added successfully!";
                    return RedirectToAction("Index", "UserHome");
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid Details");
                return RedirectToAction("Index", "Address");
            }
        }
    }
}
