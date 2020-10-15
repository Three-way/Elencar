using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    public class Actor : User
    {
        public virtual Profile Profile { get; set; }

    }
}
