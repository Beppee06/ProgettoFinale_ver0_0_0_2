﻿using System.ComponentModel.DataAnnotations;

namespace esDef.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User(string email, string password)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
        }

        public User(SimpleUser u)
        {
            Id = Guid.NewGuid();
            Email = u.Email;
            Password = u.Password;
        }
    }


    public class SimpleUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
