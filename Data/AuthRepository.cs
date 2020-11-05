using System.Text;
using System.Threading.Tasks;
using AuthAPI.Models;
using System.Text.Encodings;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthAPI.Data
{
    public class AuthRepository : iAuthRepository
    {
        private readonly AuthContext _context;
        public AuthRepository(AuthContext context){
            _context=context;
        }
        public async Task<User> Create(User user, string password)
        {
            byte[] passwordHash,passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);
            user.PasswordHash=passwordHash;
            user.PasswordSalt=passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async void CreateMany((User user,string password)[] values){
            foreach(var v in values){
                byte[] passwordHash,passwordSalt;
                CreatePasswordHash(v.password,out passwordHash,out passwordSalt);
                v.user.PasswordHash=passwordHash;
                v.user.PasswordSalt=passwordSalt;
                await _context.Users.AddAsync(v.user);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<User> Login(string username, string password)
        {
            var user=await _context.Users.FirstOrDefaultAsync(x=>x.Username==username);
            if(user==null){
                return null;
            }
            else if(!VerifyPassword(password,user.PasswordHash,user.PasswordSalt)){
                return null;
            }
            else{
                user.PasswordHash=null;
                user.PasswordSalt=null;
                return user;
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x=>x.Username==username)){
                return true;
            }
            else{
                return false;
            }
        }

        private async Task<List<User>> GetAllUsers(){
            var users= await _context.Users.ToListAsync();
            return users;
        }
        public async Task<List<string>> GetAllUsernames(){
            List<string> usernames=new List<string>();
            var users=await GetAllUsers();
            foreach(User u in users){
                usernames.Add(u.Username);
            }
            return usernames;
        }
        private void CreatePasswordHash(string password,out byte[] hash,out byte[] salt){
            using(var hmac=new System.Security.Cryptography.HMACSHA512()){
                salt=hmac.Key;
                hash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassword(string password,byte[] hash,byte[] salt){
            using(var hmac= new System.Security.Cryptography.HMACSHA512(salt)){
                var computedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;i<computedHash.Length;i++){
                    if(computedHash[i]!=hash[i]){
                        return false;
                    }
                }
                return true;
            }
        }
    }
}