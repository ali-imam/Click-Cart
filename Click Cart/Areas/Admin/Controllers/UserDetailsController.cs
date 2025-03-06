using Microsoft.AspNetCore.Mvc;
using Click_Cart.Models;
using Newtonsoft.Json;
using System.Security.Policy;
using Click_Cart.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System;
namespace Click_Cart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserDetailsController : Controller
    {
    private string UserURL = "https://localhost:7016/api/User/";
        HttpClient client = new HttpClient();

        

        
        [HttpGet]
        public IActionResult Index()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store";
            List<User> user = new List<User>();
            HttpResponseMessage response = client.GetAsync(UserURL).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<User>>(content);
                if (data != null)
                {
                    user = data;
                }
            }
            return View(user);
        }







        //ADD USER
        [HttpGet]
        public IActionResult AddUser()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {

                var data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(UserURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //TempData["success"] = "User updated successfully!";
                    return RedirectToAction("Index", "UserDetails");
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid Details");
                return RedirectToAction("AddUser", "UserDetails");
            }
            return View();
        }












        //EDIT
        [HttpGet]
        public IActionResult EditUser(string tp)
        {
            User user = new User();
            var id = int.Parse(EncryptionHelper.Decrypt(tp));
            HttpResponseMessage response = client.GetAsync(UserURL + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<User>(content);

                if (data != null)
                {
                    user = data;
                    ViewData["UserData"] = user;
                }

            }
            return View(user);
        }


        [HttpPost]
        public IActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {

                    var data = JsonConvert.SerializeObject(user);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync(UserURL + user.UserId, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //TempData["success"] = "User updated successfully!";
                        return RedirectToAction("Index", "UserDetails");
                    }
                    else
                    {
                        return BadRequest();
                    }
                
            }
            else
            {
                ModelState.AddModelError("", "Invalid Details");
                return RedirectToAction("EditUser", "UserDetails");
            }
            return View();
        }


        [HttpGet]
        public IActionResult DeleteUser(string tp)
        {
            User user = new User();
            //tp is encrypted key;
            var id = int.Parse(EncryptionHelper.Decrypt(tp));
            HttpResponseMessage response = client.GetAsync(UserURL + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<User>(content);
                if (data != null)
                {
                    user = data;
                }

            }
            return View(user);
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public IActionResult DeleteUser(User user)
        {
            if (user.UserId != null)
            {
                HttpResponseMessage response = client.DeleteAsync(UserURL + user.UserId).Result;
                if (response.IsSuccessStatusCode)
                {
                    //TempData["success"] = "User deleted successfully!";
                    return RedirectToAction("Index", "UserDetails", new { area = "Admin" });
                }
            }
            return NotFound();
        }



    }
}
