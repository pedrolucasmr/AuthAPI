using System;
using System.ComponentModel.DataAnnotations;
using AuthAPI.Data;
using System.Threading;

namespace AuthAPI.Models
{
    #nullable enable
    public class User
    {
        [Key]
        public int ID {get;set;}
        public string Username {get;set;}
        public byte[]?  PasswordHash {get;set;}
        public byte[]? PasswordSalt {get;set;}
        public string Role {get;set;}

        public User(string username, string role){
            this.Username=username;
            this.Role=role;
        }
        public User(string username,string role,byte[] passwordHash, byte[] passwordSalt){
            this.Username=username;
            this.Role=role;
            this.PasswordHash=passwordHash;
            this.PasswordSalt=passwordSalt;
        }
    }
}