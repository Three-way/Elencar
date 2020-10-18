using System;

namespace Elencar.Domain.Entities
{
    public class Reservation
    {

        public Reservation(string name, DateTime start, DateTime end, Genre genre, User producer, Actor actor)
        {
            Name = name;
            Start = start;
            End = end;
            Genre = genre;
            Producer = producer;
            Actor = actor;
        }

        public Reservation(int id, string name, DateTime start, DateTime end, Genre genre, User producer, Actor actor)
        {
            Id = id;
            Name = name;
            Start = start;
            End = end;
            Genre = genre;
            Producer = producer;
            Actor = actor;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }

        public virtual Genre Genre { get; private set; }
        public virtual User Producer { get; private set; }
        public virtual Actor Actor { get; private set; }

    }
}
