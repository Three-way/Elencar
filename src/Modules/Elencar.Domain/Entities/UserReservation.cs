using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Domain.Entities
{
    public class UserReservation
    {
        public UserReservation(int reservationId, int producerId, int actorId)
        {
            ReservationId = reservationId;
            ProducerId = producerId;
            ActorId = actorId;
        }

        public int ReservationId { get; set; }
        public int ProducerId { get; set; }
        public int ActorId { get; set; }
    }
}
