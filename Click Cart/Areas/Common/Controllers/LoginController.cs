using Click_Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace Click_Cart.Areas.Common.Controllers
{
        [Area("Common")]
    public class LoginController : Controller
    {
        public InputModel input { get; set; }
        public class InputModel
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
        private string UserURL = "https://localhost:7016/api/User/";
        HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.Clear(); // Clears session
            //to prevent browser from caching the login page and forcing to reload fresh one
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return View();
        }



        [HttpPost]
        public IActionResult Index(InputModel LoginObj)
        {
            User user = new User();
            HttpResponseMessage response = client.GetAsync(UserURL).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<User>>(content);
                if (data != null)
                {
                    user = data.Find(e => e.Email == LoginObj.Email);
                    if (user != null && user.Password.Equals(LoginObj.Password))
                    {
                        if (user.Role == "Admin")
                        {
                            HttpContext.Session.SetInt32("UserId", user.UserId);
                            HttpContext.Session.SetString("UserRole", "Admin");

                            return RedirectToAction("Index", "UserDetails", new { area = "Admin" });
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("UserId", user.UserId);
                            HttpContext.Session.SetString("UserRole", "User");
                           
                            return RedirectToAction("Index", "UserHome", new { area = "Customer" });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid credentials!!");
                    }
                }
            }
            return View();
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
