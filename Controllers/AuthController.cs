using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthAPI.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using AuthAPI.Services;
using AuthAPI.Data;
using Microsoft.Extensions.Configuration;

namespace AuthAPI.Controllers
{
    public class AuthController
    {
        private readonly IConfiguration _configuration;
        private readonly AuthContext _context;
        public AuthController(IConfiguration configuration, AuthContext context){
            _configuration=configuration;
            _context=context;

        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] string username,[FromBody] string password){
            AuthRepository repository=new AuthRepository(_context);
            var user=await repository.Login(username,password);
            var token=TokenService.GenerateToken(user,_configuration);
            return new
            {
                user=user,
                token=token
            };
        }
    }
}