using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUD.Models.DTO
{
    public class EmployeesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long? Phone { get; set; }

        public decimal Salary { get; set; }
    }
}
