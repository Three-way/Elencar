using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    public class User
    {

        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public bool Status { get; set; }
        public bool IsProducer { get; set; }
        public DateTime CreatedAt { get;  set; }
        public DateTime UpdatedAt { get;  set; }


        public void Login(User user)
        {

        }

    }
}
