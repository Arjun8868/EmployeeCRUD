using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUD.Models.DTO
{
    public class UpdateEmployeesDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public long? Phone { get; set; }

        public decimal Salary { get; set; }
    }
}
