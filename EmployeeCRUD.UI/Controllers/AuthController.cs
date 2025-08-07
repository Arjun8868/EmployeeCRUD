using EmployeeCRUD.UI.Models;
using EmployeeCRUD.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace EmployeeCRUD.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthController(IHttpClientFactory _httpClientFactory)
        {
            this._httpClientFactory = _httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel registeruserviewmodel)
        {
            var client=_httpClientFactory.CreateClient();
            var httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:5053/api/Auth/Register"),
                Content= new StringContent(JsonSerializer.Serialize(registeruserviewmodel), Encoding.UTF8, "application/json")
            };
            var httpResponse = await client.SendAsync(httpRequest);

            if (httpResponse.IsSuccessStatusCode)
            {
                var response = await httpResponse.Content.ReadAsStringAsync();
                if (response.Contains("User was registered! Please login"))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                string error = await httpResponse.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Registration failed: {error}");
            }

            return View(registeruserviewmodel);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel loginuserviewmodel)
        {
           var client = _httpClientFactory.CreateClient();
            var httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:5053/api/Auth/Login"),
                Content = new StringContent(JsonSerializer.Serialize(loginuserviewmodel), Encoding.UTF8, "application/json")
            };
            var httpResponse = await client.SendAsync(httpRequest);
            if (httpResponse.IsSuccessStatusCode)
            {
                var response = await httpResponse.Content.ReadFromJsonAsync<LoginResponseDTO>();
                if (response != null && !string.IsNullOrEmpty(response.JWTToken))
                {
                    HttpContext.Session.SetString("JWToken", response.JWTToken);
                    return RedirectToAction("Index", "Employees");
                }
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(loginuserviewmodel);
        }

    }
}
