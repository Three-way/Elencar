using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Application.AppElencar.Input
{
    public class UserInput
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool Status { get; private set; }
        public int Role { get; private set; }
    }
}
