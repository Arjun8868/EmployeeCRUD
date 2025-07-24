using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUD.Models.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public List<string> Roles { get; set; }
    }
}
