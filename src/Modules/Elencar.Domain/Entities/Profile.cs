using System;
using System.Collections.Generic;

namespace Elencar.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public decimal Fee { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int GenreId { get; set; }
        public virtual Genre  Genre { get; set; }
        public int ActorId { get; set; }
        public virtual Actor Actor { get; set; }

    }
}