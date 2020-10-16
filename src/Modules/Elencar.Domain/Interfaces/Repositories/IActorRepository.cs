using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> Get(int? quantity = 10, int? idGenre = null, DateTime? startDate = null
            , decimal? budget = null, int? orderbyVal = 0, int orderByRel = 1);
        Task<Actor> GetByIdAsync(int id);
        Task<Actor> Insert(Actor actor);
        Task<Actor> Update(Actor actor);
        Task<bool> HasActor(string email);
        void Delete(int id);
    }
}