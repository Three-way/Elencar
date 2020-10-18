using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Application.AppElencar.Input
{
    public class ReservationInput
    {
        public string Name { get;  set; }
        public DateTime Start { get;  set; }
        public DateTime End { get;  set; }
        public int GenreId { get;  set; }
        public int ProducerId { get;  set; }
        public int ActorId { get;  set; }
    }
}
