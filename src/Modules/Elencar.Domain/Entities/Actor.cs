using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    class Actor
    {
        public Actor(string name, double cache, List<Genrer> genrer, List<Reservation> reservation)
        {
            Name = name;
            Cache = cache;
            Genrer = genrer;
            Reservation = reservation;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public double Cache { get; private set; }

        public List<Genrer> Genrer { get; private set; }

        public List<Reservation> Reservation { get; private set; }

        public bool IsValid()
        {
            var valid = true;

            if ((string.IsNullOrEmpty(Name)) || (Cache < 0d) || 
                    (Genrer.Count <=0) || (Reservation.Count <= 0))
                   valid = false;
            return valid;
        }
    }

}
