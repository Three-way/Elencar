using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    public abstract class User : IUser
    {
        protected User(int id, string name, string email, string password, Perfil perfil): this(name, email, password, perfil)
        {
            Id = id;
        }

        protected User(string name, string email, string password, Perfil perfil)
        {
            Name = name;
            Email = email;
            Password = password;
            Perfil = perfil;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public Perfil Perfil { get; protected set; }

        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public void Login(IUser user)
        {

        }

        public virtual bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email)
                    || string.IsNullOrEmpty(Password) || !Perfil.IsValid())
            {
                valid = false;
            }



            return valid;
        }
    }
}
