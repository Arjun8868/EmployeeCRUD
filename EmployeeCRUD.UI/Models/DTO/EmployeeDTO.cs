using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUD.UI.Models.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
    
        public string Name { get; set; }

        public string Email { get; set; }
        public long? Phone { get; set; }

        public decimal Salary { get; set; }
    }
}
