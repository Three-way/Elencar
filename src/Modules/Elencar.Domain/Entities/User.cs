using System;
using System.Text.RegularExpressions;

namespace Elencar.Domain.Entities
{
    public class User
    {

        public User(){}

        public User(int id) 
        {
            Id = id;
        }

        public User(int id,string name, string email, bool status,Role roleId)
        {
            Id = id;
            Name = name;
            Email = email;
            Status = status;
            Role = roleId;
        }


        public User(int id, string name, string email, Role roleId)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = roleId;
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

        public bool IsValidEmail(string email)
        {
            string emailToValidate = email;

            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Password))
            {
                valid = false;
            }

            return valid;
        }

    }
}
