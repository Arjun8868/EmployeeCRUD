using AutoMapper;
using EmployeeCRUD.Data;
using EmployeeCRUD.Models.DTO;
using EmployeeCRUD.Models.Entities;
using EmployeeCRUD.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeCRUD.Controllers
{
    //localhost:xxxx/api/employes
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllOrigins")] // Enable CORS for all origins, methods, and headers
    [EnableRateLimiting("fixed")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMapper Mapper;
        private readonly ILogger<EmployeesController> logger;

        public EmployeesController(IEmployeesRepository _employeesRepository, IMapper Mapper, ILogger<EmployeesController> logger)
        {
            this._employeesRepository = _employeesRepository;
            this.Mapper = Mapper;
            this.logger = logger;
        }
        [HttpGet]
        [Authorize(Roles ="Reader, Writer")]
        public async Task<IActionResult> GetEmployees([FromQuery] string? FilterOn, [FromQuery] string? FilterQuery)
        {
            //logger.LogError("Fetching all employees from the database.");
            var employees = await _employeesRepository.GetEmployeesAsync(FilterOn, FilterQuery);

            if(employees!=null)            
                return Ok(employees);           
            else             
                return NotFound("No employees found.");

        }

        [HttpGet]
        [Route("{Id:int}")]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetEmployeesById(int Id)
        {
            var employee = await _employeesRepository.GetEmployeesById(Id);
            if (employee != null)
                return Ok(employee);
            else
                return NotFound("No employees found.");
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddEmployeesAsync([FromBody] AddEmployeesDTO Addemployeesdto)
        {
           var employeedomain = Mapper.Map<Employee>(Addemployeesdto);
           employeedomain=await _employeesRepository.AddEmployeesAsync(employeedomain);

           var empdto = Mapper.Map<EmployeesDTO>(employeedomain);
              if (empdto != null)
                 return CreatedAtAction(nameof(GetEmployeesById), new { Id = empdto.Id }, empdto);
                else
                 return BadRequest("Employee could not be added.");

        }
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateEmployeesAsync([FromRoute]int id, [FromBody] UpdateEmployeesDTO updateemployeesdto)
        {
            var employeedomain = Mapper.Map<Employee>(updateemployeesdto);
            employeedomain = await _employeesRepository.UpdateEmployeesAsync(id, employeedomain);
            if(employeedomain != null)
            {
                var empdto = Mapper.Map<EmployeesDTO>(employeedomain);
                return Ok(empdto);
            }
            else
            {
                return NotFound("Employee not found for update.");
            }

        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteEmployeesAsync([FromRoute] int id)
        {
            var employee = await _employeesRepository.DeleteEmployeesAsync(id);
            if (employee != null)
            {
                var empdto = Mapper.Map<EmployeesDTO>(employee);
                return Ok(empdto);
            }

            else
                return NotFound("Employee not found for deletion.");
        }
       

    }
}
