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
using Microsoft.AspNetCore.Http;

namespace AuthAPI.Controllers
{
    [Route("/auth")]
    [Controller]
    public class AuthController:Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AuthContext _context;
        public AuthController(IConfiguration configuration, AuthContext context){
            _configuration=configuration;
            _context=context;
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<dynamic>> Create([FromBody] UserForRegister model){
            AuthRepository repository=new AuthRepository(_context);
            User userToBeAdded=new User(model.Username,model.Role);
            User newUser=await repository.Create(userToBeAdded,model.Password);
            return newUser;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] UserForLogin model){
            AuthRepository repository=new AuthRepository(_context);
            var user=await repository.Login(model.Username,model.Password);
            if(user==null){
                return "Erro";
            }
            var token=TokenService.GenerateToken(user,_configuration,_context);
            return new
            {
                user=user,
                token=token
            };
        }
    }
}