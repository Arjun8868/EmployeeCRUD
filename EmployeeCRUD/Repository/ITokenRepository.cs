using Microsoft.AspNetCore.Identity;

namespace EmployeeCRUD.Repository
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
