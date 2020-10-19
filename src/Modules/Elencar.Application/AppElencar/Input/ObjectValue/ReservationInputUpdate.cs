using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Application.AppElencar.Input.ObjectValue
{
    public class ReservationInputUpdate
    {
        public int ReservationId { get; set; }
        public string Name { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int GenreId { get; set; }
        public int ProducerId { get; set; }
        public int ActorId { get; set; }
    }
}
