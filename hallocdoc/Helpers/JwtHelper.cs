using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace hallocdoc.Helpers
{
    public class JwtHelper
    {
        public static string GenerateJwtToken(string secretKey, string issuer, string audience,string email,int? role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string roletype = "";
            if (role == 1)
                roletype = "admin";
            else if (role == 2) roletype = "provider";
            else if (role == 3) roletype = "user";

            var claims = new List<Claim>
            {

               new Claim(ClaimTypes.Email, email),

              new Claim(ClaimTypes.Role, roletype),

              
         };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
   
}
