using HB.CqrsJwtApp.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace HB.CqrsJwtApp.UI.Controllers
{
    public class AccountController : Controller
    {

        private readonly IHttpClientFactory httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel vm) 
        {



            if (ModelState.IsValid)
            {

                var client = httpClientFactory.CreateClient();

                var content = new StringContent(JsonSerializer.Serialize(vm), Encoding.UTF8, "application/json");

             var response = await client.PostAsync("https://localhost:7257/api/Auth/Login", content);
                if (response.IsSuccessStatusCode)
                {
                   var tokenModel = JsonSerializer.Deserialize<JwtTokenResponseModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
                   {
                       PropertyNamingPolicy=JsonNamingPolicy.CamelCase
                   });

                    JwtSecurityTokenHandler handler = new();
                    var token = handler.ReadJwtToken(tokenModel!.Token);


                    //daha sonraki isteklerde accesTokeni kullanıcı claimlerinde tutup
                   //istek atacağım yerde claim'i çağırıp ondan token bilgisi alıp
                   //istek header'a eklemek için claim olarak ekliyorum
                   //bu claimler ilede oturum açtığım için bende kalıyor
                   //aynı zamanda token'dan gelen claimleride ekliyorum
                    var claims = token.Claims.ToList();
                    claims.Add(new Claim("accesToken", tokenModel.Token));

                    var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                    var authProps = new AuthenticationProperties()
                    {
                        ExpiresUtc = tokenModel.ExpireDate,
                        IsPersistent = true,

                    };

                  await  HttpContext.SignInAsync(
                      JwtBearerDefaults.AuthenticationScheme,
                      new ClaimsPrincipal(claimsIdentity),
                      authProps);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                }
              
            }

            return View(vm);

        }
    }
}
