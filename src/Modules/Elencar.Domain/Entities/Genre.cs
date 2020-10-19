using System;

namespace Elencar.Domain.Entities
{
    public class Genre
    {
        public Genre()
        {
        }

        public Genre(string name)
        {
            Name = name;
        }

        public Genre(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}