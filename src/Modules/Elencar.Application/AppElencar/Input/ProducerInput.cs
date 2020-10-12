using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Application.AppElencar.Input
{
    public class ProducerInput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IdPerfil { get; set; }


        public virtual bool IsValid()
        {
            var valid = true;


            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email)
                    || string.IsNullOrEmpty(Password) || IdPerfil < 0)
            {
                valid = false;
            }

            return valid;
        }
    }
}
