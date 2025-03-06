using Click_Cart.Helpers;
using Click_Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Click_Cart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailsController : Controller
    {
        private string ProductURL = "https://localhost:7016/api/Product/";
        HttpClient client = new HttpClient();




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


        //ADD Product
        [HttpGet]
        public IActionResult AddProduct()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {

                var data = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(ProductURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //TempData["success"] = "Product updated successfully!";
                    return RedirectToAction("Index", "ProductDetails");
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid Details");
                return RedirectToAction("AddProduct", "ProductDetails");
            }
            return View();
        }












        //EDIT
        [HttpGet]
        public IActionResult EditProduct(string tp)
        {
            Product product = new Product();
            var id = int.Parse(EncryptionHelper.Decrypt(tp));
            HttpResponseMessage response = client.GetAsync(ProductURL + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Product>(content);

                if (data != null)
                {
                    product = data;
                    ViewData["ProductData"] = product;
                }

            }
            return View(product);
        }


        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {

                var data = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(ProductURL + product.ProductId, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //TempData["success"] = "Product updated successfully!";
                    return RedirectToAction("Index", "ProductDetails");
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid Details");
                return RedirectToAction("EditProduct", "ProductDetails");
            }
            return View();
        }


        [HttpGet]
        public IActionResult DeleteProduct(string tp)
        {
            Product product = new Product();
            //tp is encrypted key;
            var id = int.Parse(EncryptionHelper.Decrypt(tp));
            HttpResponseMessage response = client.GetAsync(ProductURL + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Product>(content);
                if (data != null)
                {
                    product = data;
                }

            }
            return View(product);
        }

        [HttpPost]
        [ActionName("DeleteProduct")]
        public IActionResult DeleteProduct(Product product)
        {
            if (product.ProductId != null)
            {
                HttpResponseMessage response = client.DeleteAsync(ProductURL + product.ProductId).Result;
                if (response.IsSuccessStatusCode)
                {
                    //TempData["success"] = "Product deleted successfully!";
                    return RedirectToAction("Index", "ProductDetails", new { area = "Admin" });
                }
            }
            return NotFound();
        }

    }
}
