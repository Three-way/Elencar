using System;

namespace Elencar.Domain.Entities
{
    public class Genre
    {
        public Genre()
        {
        }

        public Genre(string description)
        {
            Description = description;
        }

        public Genre(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get;  }

    }
}