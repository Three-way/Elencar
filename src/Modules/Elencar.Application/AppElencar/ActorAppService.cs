using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Interfaces;
using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar
{
    public class ActorAppService : ControllerBase, IActorAppService
    {
        private readonly IActorRepository _actorRepository;


        public ActorAppService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<IEnumerable<Actor>> Get()
        {
            return await _actorRepository.Get();
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            return await _actorRepository
                            .GetByIdAsync(id)
                            .ConfigureAwait(false);
        }

        public async Task<Actor> Insert(ActorInput actorInput)
        {

            var actor = new Actor() { Name = actorInput.Name, Email = actorInput.Email, Password = actorInput.Password
                                            , Profile = new Profile() { Bio = actorInput.Bio, Fee = actorInput.Fee
                                            , Genre = new Genre() { Id = actorInput.GenreId} }
            };


            return await _actorRepository.Insert(actor);

        }
        public async Task<Actor> Update(ActorInput actorInput)
        {
            var actor = new Actor() { Name = actorInput.Name, Email = actorInput.Email, Password = actorInput.Password };

            return await _actorRepository.Insert(actor);
        }

        public void Delete(int id)
        {
            _actorRepository.Delete(id);
        }

        
    }
}
