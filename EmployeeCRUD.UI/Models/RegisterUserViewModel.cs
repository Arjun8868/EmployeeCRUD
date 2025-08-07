using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUD.UI.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public List<string> Roles { get; set; } = new List<string>();
    }
}
