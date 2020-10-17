using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Application.AppElencar.Input
{
    public class ActorInput
    {
        public string Bio { get; private set; }
        public decimal Fee { get; private set; }
        public bool Status { get; private set; }
        public int userId { get; private set; }
    }
}
