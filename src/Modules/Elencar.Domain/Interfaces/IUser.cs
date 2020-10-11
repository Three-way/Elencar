using System;

namespace Elencar.Domain.Entities
{
    public interface IUser
    {
        DateTime CreatedAt { get; }
        string Email { get; }
        int Id { get; }
        string Name { get; }
        string Password { get; }
        Perfil Perfil { get; }
        DateTime UpdatedAt { get; }

        bool IsValid();
        void Login(IUser user);
    }
}