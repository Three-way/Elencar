using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Interfaces;
using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Marraia.Notifications.Interfaces;
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
        private readonly ISmartNotification _notification;
        private readonly IGenreRepository _genreRepository;


        public ActorAppService(ISmartNotification notification, IActorRepository actorRepository, IGenreRepository genreRepository)
        {
            _notification = notification;
            _actorRepository = actorRepository;
            _genreRepository = genreRepository;
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
           
            var actor = new Actor(actorInput.Bio,actorInput.Fee,new User(actorInput.userId));


         
            if (actor == default)
            {
                _notification.NewNotificationBadRequest("Invalid Actor!");
                return default;
            }


            return await _actorRepository.Insert(actor);

        }
        public async Task<Actor> Update(ActorInput actorInput)
        {
            var actor = new Actor(actorInput.Bio, actorInput.Fee, new User(actorInput.userId));

            return await _actorRepository.Insert(actor);
        }

        public void Delete(int id)
        {
            _actorRepository.Delete(id);
        }

        public Task<Actor> EnrolledActor(int id)
        {
            throw new NotImplementedException();
        }
    }
}
