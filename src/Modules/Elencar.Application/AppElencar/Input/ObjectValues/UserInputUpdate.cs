using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Application.AppElencar.Input.ObjectValues
{
    public class UserInputUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public int Role { get; set; }
    }
}
