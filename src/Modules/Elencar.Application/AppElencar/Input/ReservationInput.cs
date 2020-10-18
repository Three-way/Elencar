using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Application.AppElencar.Input
{
    public class ReservationInput
    {
        public string Name { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public int GenreId { get; private set; }
        public int ProducerId { get; private set; }
        public int ActorId { get; private set; }
    }
}
