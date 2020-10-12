using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    public class Actor : User
    {
        public Actor(int id, string name, string email, string password, Perfil perfil, double cache, List<Genre> genre, List<Reservation> reservation)
            : base(id,name, email, password, perfil)
        {
            Cache = cache;
            Genre = genre;
            Reservation = reservation;
        }

        public Actor(string name, string email, string password, Perfil perfil, double cache, List<Genre> genre, List<Reservation> reservation)
            : base(name, email, password, perfil)
        {
            Cache = cache;
            Genre = genre;
            Reservation = reservation;
        }

        public double Cache { get; private set; }

        public List<Genre> Genre { get; private set; }

        public List<Reservation> Reservation { get; private set; }

        public override bool IsValid()
        {
            var valid = true;

            if (!base.IsValid() ||(Cache < 0d) || 
                    (Genre.Count <=0) || (Reservation.Count <= 0))
                   valid = false;
            return valid;
        }
    }

}
