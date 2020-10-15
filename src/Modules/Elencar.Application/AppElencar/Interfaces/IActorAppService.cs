using Elencar.Application.AppElencar.Input;
using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar.Interfaces
{
    public interface IActorAppService
    {
        Task<IEnumerable<Actor>> Get();
        Task<Actor> GetByIdAsync(int id);
        Task<Actor> Insert(ActorInput actorInput);
        Task<Actor> Update(ActorInput actorInput);
        void Delete(int id);
    }
}
