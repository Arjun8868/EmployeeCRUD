using EmployeeCRUD.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUD.UI.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EmployeesController(IHttpClientFactory _httpClientFactory)
        {
            this._httpClientFactory = _httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            try
            {
                var client = _httpClientFactory.CreateClient(); // Create an HTTP client to communicate with the API
                var httpresponse = await client.GetAsync("http://localhost:5053/api/employees"); // API call to get employees
                httpresponse.EnsureSuccessStatusCode(); // Ensure the response is successful, if it is false, it throws an exception
                employees.AddRange(await httpresponse.Content.ReadFromJsonAsync<IEnumerable<EmployeeDTO>>()); 
              
            }
            catch (Exception)
            {

                throw;
            }




            //Get all employees from the API and pass to the view
            return View(employees);
        }
    }
}
