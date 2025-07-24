using EmployeeCRUD.Models.Entities;

namespace EmployeeCRUD.Repository
{
    public interface IEmployeesRepository
    {
        public Task<List<Employee>> GetEmployeesAsync(string? FilterOn=null, string? FilterQuery=null);
        public Task<Employee> GetEmployeesById(int Id);
        public Task<Employee> AddEmployeesAsync(Employee employee);
        public Task<Employee> UpdateEmployeesAsync(int id, Employee employee);
        public Task<Employee> DeleteEmployeesAsync(int id);
    }
}
