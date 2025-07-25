﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeCRUD.Repository
{
    public class TokenRepository: ITokenRepository
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<TokenRepository> logger;
        public TokenRepository(IConfiguration configuration, ILogger<TokenRepository> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //Create Claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            string tokenjwt = new JwtSecurityTokenHandler().WriteToken(token);
            int a = 18;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
