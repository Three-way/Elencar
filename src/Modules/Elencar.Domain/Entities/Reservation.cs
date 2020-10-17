using System;

namespace Elencar.Domain.Entities
{
    public class Reservation
    {

        public Reservation(){}
        public Reservation(string name, DateTime start, DateTime end, Genre genre)
        {
            Name = name;
            Start = start;
            End = end;
            Genre = genre;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public virtual Genre Genre { get; set; }


    }
}
