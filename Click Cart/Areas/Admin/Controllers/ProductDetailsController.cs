using Click_Cart.Areas.Customer.Controllers;
using Click_Cart.Helpers;
using Click_Cart.Models;
using Microsoft.AspNetCore.Hosting;
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


        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductDetailsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }



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
        public IActionResult AddProduct(Product product, IFormFile? ProductImg)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (ProductImg != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProductImg.FileName);
                    string productPath = Path.Combine(wwwRootPath, "images", "products");
                    string fullPath = Path.Combine(productPath, fileName);


                    using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        ProductImg.CopyTo(fileStream);
                    }

                    product.ProductImg = @"\images\products\" + fileName; 
                }

                var data = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(ProductURL, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Product added successfully!";
                    return RedirectToAction("Index", "ProductDetails");
                }
                else
                {
                    return BadRequest();
                }
            }

            ModelState.AddModelError("", "Invalid Details");
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
        public IActionResult EditProduct(Product product, IFormFile? ProductImg)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (ProductImg != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProductImg.FileName);
                    string productPath = Path.Combine(wwwRootPath, "images", "products");
                    string fullPath = Path.Combine(productPath, fileName);

                    using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        ProductImg.CopyTo(fileStream);
                    }

                    product.ProductImg = @"\images\products\" + fileName;
                }

                HttpResponseMessage response = client.GetAsync(ProductURL).Result;

                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<List<Product>>(content);

                    if (data != null)
                    {
                        var temp = data.Find(e => e.ProductId == product.ProductId);
                        if (temp != null)
                        {
                            temp.ProductName = product.ProductName;
                            temp.Description = product.Description;
                            temp.Price = product.Price;
                            temp.StockQuantity = product.StockQuantity;
                            temp.CategoryId = product.CategoryId;

                            if (ProductImg != null)
                            {
                                temp.ProductImg = product.ProductImg;
                            }

                            var productData = JsonConvert.SerializeObject(temp);
                            StringContent productContent = new StringContent(productData, Encoding.UTF8, "application/json");
                            HttpResponseMessage productResponse = client.PutAsync(ProductURL + temp.ProductId, productContent).Result;

                            if (!productResponse.IsSuccessStatusCode)
                            {
                                return BadRequest("Failed to update product.");
                            }
                        }
                    }

                    ViewData["ProductData"] = product;
                }
                else
                {
                    return BadRequest("Failed to retrieve products.");
                }

                TempData["success"] = "Product updated successfully!";
                return RedirectToAction("Index", "ProductDetails");
            }
            else
            {
                ModelState.AddModelError("", "Invalid details entered");
                return View();
            }
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
                    TempData["success"] = "Product deleted successfully!";
                    return RedirectToAction("Index", "ProductDetails", new { area = "Admin" });
                }
            }
            return NotFound();
        }

    }
}
