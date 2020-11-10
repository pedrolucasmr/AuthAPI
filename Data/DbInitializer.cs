using System.Threading.Tasks;
using System.Linq;
using AuthAPI.Models;
using System;
using System.Collections.Generic;

namespace AuthAPI.Data
{
    public class DbInitializer
    {
        public async static Task Initialize(AuthContext _context){
            AuthRepository repository=new AuthRepository(_context);
            _context.Database.EnsureCreated();
            if(_context.Users.Any()){
                 return;
            }
            UserForRegister user1=new UserForRegister(){Username="johnHughes",Role="Admin",Password="12345678"};
            UserForRegister user2=new UserForRegister(){Username="Fatima13",Role="User",Password="87654321"};
            UserForRegister user3=new UserForRegister(){Username="RobertTheBoss",Role="User",Password="thebossgameplay"};
            List<UserForRegister> users=new List<UserForRegister>(){user1,user2,user3};
            await repository.CreateMany(users);
        }
    }
}