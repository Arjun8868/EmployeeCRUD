using EmployeeCRUD.Data;
using EmployeeCRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.Repository
{
    public class SqlEmployeesRepository : IEmployeesRepository
    {
        private readonly ApplicationDbContext _context;
        public SqlEmployeesRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task<List<Employee>> GetEmployeesAsync(string? FilterOn = null, string? FilterQuery = null)
        {
           
            var employees = _context.Employees.AsQueryable();
            //Filtering
            if (String.IsNullOrWhiteSpace(FilterOn)== false && String.IsNullOrWhiteSpace(FilterQuery) == false)
            {   
                //Filter by Name
                if (FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees.Where(e => e.Name==FilterQuery);
                   
                }
                //Filter by Salary
                if (FilterOn.Equals("Salary", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees.Where(e => e.Salary == Convert.ToDecimal(FilterQuery));

                }

            }
             return employees.ToList();
           
            //return await _context.Employees.ToListAsync();
        }
        public async Task<Employee> GetEmployeesById(int Id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == Id);

        }
        public async Task<Employee> AddEmployeesAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;

        }

        public async Task<Employee> UpdateEmployeesAsync(int id, Employee employee)
        { 
            var existingEmployee= await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if(existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Email = employee.Email;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Salary = employee.Salary;
                _context.Employees.Update(existingEmployee);
                await _context.SaveChangesAsync();
                return existingEmployee;
            }
            else
            {
                return null; 
            }
        }
        public async Task<Employee> DeleteEmployeesAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            else
            {
                return null;
            }
        }
    }
}
