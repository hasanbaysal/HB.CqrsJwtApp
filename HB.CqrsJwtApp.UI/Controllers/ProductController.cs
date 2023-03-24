using HB.CqrsJwtApp.UI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HB.CqrsJwtApp.UI.Controllers
{

    [Authorize(Roles = "Admin,Member")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> List()
        {

            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;

            //https://localhost:7257/api/products


            var client = httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

            var result = await client.GetAsync("https://localhost:7257/api/products");


            if (result.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<List<ProductListModel>>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                return View(data);
            }

            return View(null);


        }
        public async Task<IActionResult> Delete(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;

            //https://localhost:7257/api/products


            var client = httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

            var result = await client.DeleteAsync($"https://localhost:7257/api/products/{id}");



            return RedirectToAction("List");
        }

        public async Task<IActionResult> Create()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;

            //https://localhost:7257/api/products


            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

            //categories list 

            var result =await client.GetAsync("https://localhost:7257/api/categories");


            var jsondata = await result.Content.ReadAsStringAsync();

         var categoryList  = JsonSerializer.Deserialize<List<CategoryListModel>>(jsondata, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });


            var selectList = new SelectList(categoryList, "Id", "Definition");

            ViewBag.selectList = selectList;


            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel vm)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;

            ////https://localhost:7257/api/products


            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

            var serializedData=JsonSerializer.Serialize(vm);
            StringContent content = new StringContent(serializedData, Encoding.UTF8, "application/json");

            await client.PostAsync("https://localhost:7257/api/products", content);

          

            return RedirectToAction("List");
        }


        
        public async Task<IActionResult> Update(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;

            //https://localhost:7257/api/products


            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

            //categories list 

            var result = await client.GetAsync("https://localhost:7257/api/categories");


            var jsondata = await result.Content.ReadAsStringAsync();

            var categoryList = JsonSerializer.Deserialize<List<CategoryListModel>>(jsondata, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });





          

            var productGetResult =   await client.GetAsync($"https://localhost:7257/api/products/{id}");

            var productvm = JsonSerializer.Deserialize<ProductListModel>(await productGetResult.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });




            var selectList = new SelectList(categoryList, "Id", "Definition",productvm!.CategoryId);

            ViewBag.selectList = selectList;


            return View(productvm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductListModel vm)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;

            //https://localhost:7257/api/products


            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

            var jsondata =JsonSerializer.Serialize(vm);
            StringContent content = new(jsondata, Encoding.UTF8, "application/json");

            await client.PutAsync("https://localhost:7257/api/products", content);


            return RedirectToAction("List");
            
        }

    }
}
