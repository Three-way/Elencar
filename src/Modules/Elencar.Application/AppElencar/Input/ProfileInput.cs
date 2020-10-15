using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Application.AppElencar.Input
{
    public class ProfileInput
    {
        public string Bio { get; set; }
        public decimal Fee { get; set; }
        public int GenreId { get; set; }
        public int ActorId { get; set; }

    }
}
