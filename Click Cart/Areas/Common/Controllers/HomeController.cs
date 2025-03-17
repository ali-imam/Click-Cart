using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Click_Cart.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Click_Cart.Areas.Common.Controllers
{
    public class HomeController : Controller
    {
        private string CategoryURL = "https://localhost:7016/api/Category/";
        private string ProductURL = "https://localhost:7016/api/Product/";
        HttpClient client = new HttpClient();

        public IEnumerable<SelectListItem> categoryName { get; set; }

        [Area("Common")]

        [HttpGet]
        public IActionResult Index()
        {
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


            //HttpResponseMessage Categoryresponse = client.GetAsync(CategoryURL).Result;
            //if (Categoryresponse.IsSuccessStatusCode)
            //{
            //    string Categorycontent = Categoryresponse.Content.ReadAsStringAsync().Result;
            //    var data = JsonConvert.DeserializeObject<List<Category>>(Categorycontent);
            //    if (data != null)
            //    {
            //        categoryName = data.Select(t => new SelectListItem
            //        {
            //            Text = t.CategoryName,
            //            Value = t.CategoryId.ToString()
            //        }).ToList();
            //    }
            //}
            //// Convert teams to SelectListItem for dropdown
            //ViewData["Category"] = categoryName;

            return View(product);
        }
    }
}


