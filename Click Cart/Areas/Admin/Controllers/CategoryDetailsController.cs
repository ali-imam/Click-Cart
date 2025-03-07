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
    public class CategoryDetailsController : Controller
    {
        private string CategoryURL = "https://localhost:7016/api/Category/";
        HttpClient client = new HttpClient();




        [HttpGet]
        public IActionResult Index()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store";
            List<Category> category = new List<Category>();
            HttpResponseMessage response = client.GetAsync(CategoryURL).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Category>>(content);
                if (data != null)
                {
                    category = data;
                }
            }
            return View(category);
        }







        //ADD Category
        [HttpGet]
        public IActionResult AddCategory()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {

                var data = JsonConvert.SerializeObject(category);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(CategoryURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //TempData["success"] = "Category updated successfully!";
                    return RedirectToAction("Index", "CategoryDetails");
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid Details");
                return RedirectToAction("AddCategory", "CategoryDetails");
            }
        }












        //EDIT
        [HttpGet]
        public IActionResult EditCategory(string tp)
        {
            Category category = new Category();
            var id = int.Parse(EncryptionHelper.Decrypt(tp));
            HttpResponseMessage response = client.GetAsync(CategoryURL + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Category>(content);

                if (data != null)
                {
                    category = data;
                    ViewData["CategoryData"] = category;
                }

            }
            return View(category);
        }


        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {

                var data = JsonConvert.SerializeObject(category);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(CategoryURL + category.CategoryId, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //TempData["success"] = "Category updated successfully!";
                    return RedirectToAction("Index", "CategoryDetails");
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid Details");
                return RedirectToAction("EditCategory", "CategoryDetails");
            }
        }


        [HttpGet]
        public IActionResult DeleteCategory(string tp)
        {
            Category category = new Category();
            //tp is encrypted key;
            var id = int.Parse(EncryptionHelper.Decrypt(tp));
            HttpResponseMessage response = client.GetAsync(CategoryURL + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Category>(content);
                if (data != null)
                {
                    category = data;
                }

            }
            return View(category);
        }

        [HttpPost]
        [ActionName("DeleteCategory")]
        public IActionResult DeleteCategory(Category category)
        {

                HttpResponseMessage response = client.DeleteAsync(CategoryURL + category.CategoryId).Result;
                if (response.IsSuccessStatusCode)
                {
                    //TempData["success"] = "Category deleted successfully!";
                    return RedirectToAction("Index", "CategoryDetails", new { area = "Admin" });
                }

            return NotFound();
        }



    }
}
