using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Genre Genre { get; set; }


    }
}
