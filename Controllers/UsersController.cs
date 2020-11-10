using System.Reflection.Metadata;
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
    [Route("/users")]
    [Controller]
    public class UsersController:Controller
    {
        private readonly AuthContext _context;
        private readonly IConfiguration _configuration;
        public UsersController(AuthContext context, IConfiguration configuration){
            _context=context;
            _configuration=configuration;
        }
        [HttpGet]
        [Route("allusers")]
        [Authorize(Roles="Admin")]
        public async Task<List<string>> AllUsers(){
            AuthRepository repository=new AuthRepository(_context);
            List<string> results= await repository.GetAllUsernames();
            string[] usernames=new string[results.Count];
            foreach(string i in results){
                usernames.Append(i);
            }
            return results;
        }
        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles="Admin")]
        public async Task<string> DeleteAllUsers(){
            AuthRepository repository= new AuthRepository(_context);
            if(await repository.DropAllRecords())
                return "Todos os Registros foram deletados coms sucesso";
            else
                return "Houve um erro ao deletar os registros";
        }
        [HttpGet]
        [Route("welcome")]
        [Authorize(Roles="User,Admin")]
        public string Welcome()=>String.Format("Seja bem vindo, {0}",User.Identity.Name);
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous()=>"Faça login";
    }
}