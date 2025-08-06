using EmployeeCRUD.UI.Models;
using EmployeeCRUD.UI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace EmployeeCRUD.UI.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EmployeesController(IHttpClientFactory _httpClientFactory)
        {
            this._httpClientFactory = _httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            try
            {
                var client = _httpClientFactory.CreateClient(); // Create an HTTP client to communicate with the API
                var httpresponse = await client.GetAsync("http://localhost:5053/api/employees"); // API call to get employees
                httpresponse.EnsureSuccessStatusCode(); // Ensure the response is successful, if it is false, it throws an exception
                employees.AddRange(await httpresponse.Content.ReadFromJsonAsync<IEnumerable<EmployeeDTO>>());//Deserialize the response content to a list of EmployeeDTO

            }
            catch (Exception)
            {

                throw;
            }

            //Get all employees from the API and pass to the view
            return View(employees);
        }
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeesviewmodel)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post, // Set the HTTP method to POST
                RequestUri = new Uri("http://localhost:5053/api/employees"), // Set the request URI to the API endpoint
                Content = new StringContent(JsonSerializer.Serialize(addEmployeesviewmodel), Encoding.UTF8, "application/json")// Serialize the view model to JSON)
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();// Send the HTTP request asynchronously
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<EmployeeDTO>();
            if (response is not null)
            {
                return RedirectToAction("Index", "Employees"); // If the response is not null, redirect to the Index action
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<EmployeeDTO>($"http://localhost:5053/api/employees/{id}"); // Get the employee details by ID
            if (response is not null)
            {
                return View(response);
            }
            return View(null);


        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeDTO request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put, // Set the HTTP method to PUT
                RequestUri = new Uri($"http://localhost:5053/api/employees/{request.Id}"), // Set the request URI to the API endpoint with the employee ID
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json") // Serialize the request object to JSON
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage); // Send the HTTP request asynchronously
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<EmployeeDTO>(); // Read the response content as EmployeeDTO
            if (response is not null)
            {
                return RedirectToAction("Index", "Employees"); // If the response is not null, redirect to the Index action
            }
            return View(request); // If the response is null, return the request object to the view
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeDTO request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpResponseMessage=await client.DeleteAsync($"http://localhost:5053/api/employees/{request.Id}");
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<EmployeeDTO>(); // Read the response content as EmployeeDTO
            if (response is not null)
            {
                return RedirectToAction("Index", "Employees"); // If the response is not null, redirect to the Index action
            }
            return View("Edit");

        }

       
    }
                  
    
}
