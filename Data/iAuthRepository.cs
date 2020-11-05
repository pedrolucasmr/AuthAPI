using System;
using System.Threading.Tasks;
using AuthAPI.Models;

namespace AuthAPI.Data
{
    public interface iAuthRepository
    {
         Task<User> Create(User user, string password);
         Task<User> Login(string username,string password);
         Task<bool> UserExists (string username);
    }
}