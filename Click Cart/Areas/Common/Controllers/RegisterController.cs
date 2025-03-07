using Click_Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Click_Cart.Areas.Common.Controllers
{
    [Area("Common")]
    public class RegisterController : Controller
    {
        private string UserURL = "https://localhost:7016/api/User/";
        HttpClient client = new HttpClient();


        //ADD USER
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {

                var data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(UserURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //TempData["success"] = "User updated successfully!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid Details");
                return RedirectToAction("Index", "Register");
            }
        }
    }
}
