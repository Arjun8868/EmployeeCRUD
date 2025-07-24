using EmployeeCRUD.Models.DTO;
using EmployeeCRUD.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<IdentityUser> _userManager, ITokenRepository tokenRepository)
        {
            this._userManager = _userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                //Add roles to this user
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login");
                    }

                }

            }
            return BadRequest("Something went wrong");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginRequestDTO.UserName);
            if (identityUser != null)
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(identityUser, loginRequestDTO.Password);
                if (isPasswordValid)
                {
                    //Generate JWT Token
                    var roles = await _userManager.GetRolesAsync(identityUser);
                    if (roles != null && roles.Any())

                    {
                        var jwtToken = tokenRepository.CreateJWTToken(identityUser, roles.ToList());
                        var responseDTO = new LoginResponseDTO()
                        {
                            JWTToken = jwtToken
                        };
                        return Ok(responseDTO);
                    }                    
                   
                }
            }
            return Unauthorized("Invalid username or password");
        }

    }
}
