using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUD.Models.Entities
{
    public class Employee
    {
        [Key]//Optional
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; } 
        public long? Phone { get; set; }

        public decimal Salary { get; set; }

    }
}
