using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    class Reservation
    {
        public Reservation(DateTime start, DateTime end, List<Genre> genres, Producer producer)
        {
            Start = start;
            End = end;
            Genres = genres;
            Producer = producer;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public int Id { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public List<Genre> Genres { get; private set; }
        public Producer Producer { get; private set; }
        public bool Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public bool IsValid()
        {
            var valid = true;

            if ((Start < DateTime.Today) || (End < Start || End < DateTime.Today) 
                    || (Genres.Count <= 0) || (!Producer.IsValid()))
                valid = false;

            return valid;
        }
    }
}
