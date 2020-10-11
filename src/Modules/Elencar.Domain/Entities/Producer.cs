using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    public class Producer : User
    {
        public Producer(string name, string email, string password, Perfil perfil) 
            : base(name, email, password, perfil)

        {
             
        }


        
    }

    

}
