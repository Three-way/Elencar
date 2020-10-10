using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    public class Producer
    {
        public Producer(string name)

        {
            Name = name;    
        }

        public int Id { get; private set; }
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
