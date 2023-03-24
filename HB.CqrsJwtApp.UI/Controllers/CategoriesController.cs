using HB.CqrsJwtApp.UI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HB.CqrsJwtApp.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CategoriesController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;

            if (token != null)
            {

                var client = httpClientFactory.CreateClient();

                //token'ı requeste ekledik
                client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine(token);

                var response = await client.GetAsync("https://localhost:7257/api/categories");

                if (response.IsSuccessStatusCode)
                {

                    var jsondata = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<List<CategoryListModel>>(jsondata, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    return View(result);

                }

            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;
            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (token != null)
            {

                var response = await client.DeleteAsync($"https://localhost:7257/api/categories/{id}");


            }

            return RedirectToAction("List");

        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateModel vm)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;

            var client = httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsondata = JsonSerializer.Serialize(vm);

            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7257/api/categories", content);


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }

            ModelState.AddModelError("", "Error");
            return View(vm);
        }

        public async Task<IActionResult> Update(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;

            var client = httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await client.GetAsync($"https://localhost:7257/api/categories/{id}");


            var data = JsonSerializer.Deserialize<CategoryListModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryListModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var token = User.Claims.FirstOrDefault(x => x.Type == "accesToken")!.Value;
            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsondata = JsonSerializer.Serialize(vm);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var result = await client.PutAsync("https://localhost:7257/api/categories", content);
            return RedirectToAction("List");
        }

    }
}
