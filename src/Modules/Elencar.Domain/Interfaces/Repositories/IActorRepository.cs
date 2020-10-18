using Elencar.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> Get();
        Task<Actor> GetActorByUserId(int id);
        Task<Actor> GetByIdAsync(int id);
        Task<Actor> Insert(Actor actor);
        Task<Actor> Update(Actor actor);
        void Delete(int id);
    }
}