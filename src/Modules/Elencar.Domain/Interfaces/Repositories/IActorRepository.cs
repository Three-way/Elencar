using Elencar.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> Get();
        bool EnrolledActor(int id);
        Task<Actor> GetByIdAsync(int id);
        Task<int> Insert(Actor actor);
        Task<int> Update(Actor actor);
        void Delete(int id);
    }
}