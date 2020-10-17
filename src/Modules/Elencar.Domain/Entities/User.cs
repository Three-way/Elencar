﻿using System;

namespace Elencar.Domain.Entities
{
    public class User
    {

        public User(){}

        public User(int id) 
        {
            Id = id;
        }

        public User(string name, string email, string password, Role roleId)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = roleId;
        }

        public int Id { get;  private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool Status { get; private set; }
        public Role Role { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }


        public void Login(User user)
        {

        }

    }
}
