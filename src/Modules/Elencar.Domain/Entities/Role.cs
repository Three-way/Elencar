using System;

namespace Elencar.Domain.Entities
{
    public class Role
    {
        public Role(){}

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Role(int id, string name, DateTime createdAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
        }

        public Role(int id)
        {
            Id = id;
        }

        public Role(string name)
        {
            Name = name;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get;}
    }
}
