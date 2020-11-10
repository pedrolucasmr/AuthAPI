using System;
using System.Security.Authentication;
using System.Reflection.PortableExecutable;
using System.IO.Enumeration;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AuthAPI.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using AuthAPI.Data;

namespace AuthAPI.Services
{
    public static class TokenService
    {
        public static string GenerateToken(User user, IConfiguration configuration,AuthContext context){
            AuthRepository repository=new AuthRepository(context);
            JwtSecurityTokenHandler tokenHandler=new JwtSecurityTokenHandler();
            byte[] key=System.Text.Encoding.UTF8.GetBytes(configuration["hashKey"]);
            SecurityTokenDescriptor tokenDescriptor=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name,user.Username.ToString()),
                    new Claim(ClaimTypes.Role,user.Role.ToString())
                }),
                Expires=DateTime.UtcNow.AddHours(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
            };
            JwtSecurityToken token=tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}