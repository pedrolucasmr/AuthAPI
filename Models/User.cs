using System;
using System.ComponentModel.DataAnnotations;
using AuthAPI.Data;
using System.Threading;

namespace AuthAPI.Models
{
    public class User
    {
        [Key]
        public int ID {get;set;}
        public string Username {get;set;}
        public byte[]?  PasswordHash {get;set;}
        public byte[]? PasswordSalt {get;set;}
        public string Role {get;set;}
    }
}