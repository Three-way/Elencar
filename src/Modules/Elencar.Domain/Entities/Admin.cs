using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    public class Admin
    {
        public Admin(string name)

        {
            Id = Guid.NewGuid();
            Name = name;       
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }




        public bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Name))
            {
                valid = false;
            }

            return valid;
        }
    }

    

}
