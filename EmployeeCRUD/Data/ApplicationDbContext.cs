using Microsoft.EntityFrameworkCore;
using EmployeeCRUD.Models.Entities; // Ensure this namespace matches your project structure

namespace EmployeeCRUD.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
      
    }
   
}
