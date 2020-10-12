namespace Elencar.Domain.Entities
{
    public class Perfil
    {
        public Perfil(string description)
        {
            Description = description;
        }

        public Perfil(int id,string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }

        public bool IsValid()
        {
            if (Id < 0 || string.IsNullOrEmpty(Description))
                return false;

            return true;
        }

    }
}