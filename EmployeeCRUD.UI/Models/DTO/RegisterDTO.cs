using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUD.UI.Models.DTO
{
    public class RegisterDTO
    {
       
        public string UserName { get; set; }

       
        public string Password { get; set; }
      
        public List<string> Roles { get; set; }
    }
}
