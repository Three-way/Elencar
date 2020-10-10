using System;

namespace Elencar.Domain.Entities
{
    public class Genrer
    {
        public Genrer(string descricao)
        {
            Descricao = descricao;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Descricao))
                valid = false;

            return valid;
        }
    }
}