using System.Linq;
using AuthAPI.Models;
using System;

namespace AuthAPI.Data
{
    public class DbInitializer
    {
        public static void Initialize(AuthContext _context){
            AuthRepository repository=new AuthRepository(_context);
            _context.Database.EnsureCreated();
            if(_context.Users.Any()){
                return;
            }
            User user1=new User(){Username="johnHughes"};
            User user2=new User(){Username="Fatima83"};
            User user3=new User(){Username="RobertTheBoss"};
            (User user,string passWord)[] usersToBeAdded=new (User user, string passWord)[]{
                (user1,"12345678"),(user2,"19720405"),(user3,"thebossgameplay")
            };
            repository.CreateMany(usersToBeAdded);
        }
    }
}