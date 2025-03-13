using Click_Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;

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
                user.Password = HashPassword(user.Password);
                var data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(UserURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Account created successfully!";
                    return RedirectToAction("Index", "Login");
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









        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                //compute hash
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (var b in hashedBytes)
                {
                    //convert hashedBytes to its hexadecimal equivalent
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
