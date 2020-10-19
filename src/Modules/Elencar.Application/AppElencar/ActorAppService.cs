using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Input.ObjectValues;
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


        public ActorAppService(ISmartNotification notification, IActorRepository actorRepository)
        {
            _notification = notification;
            _actorRepository = actorRepository;
        }

        public async Task<IEnumerable<Actor>> Get()
        {
            return await _actorRepository.Get();
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            return await _actorRepository
                            .GetByIdAsync(id);
        }

        public async Task<Actor> Insert(ActorInput actorInput)
        {
           
            var actor = new Actor(actorInput.Bio,actorInput.Fee,new User(actorInput.userId));
         
            if (actor == default)
            {
                _notification.NewNotificationBadRequest("Invalid Actor!");
                return default;
            }

            var id = await _actorRepository.Insert(actor);

            if (id == default)
            {
                _notification.NewNotificationBadRequest("Usuário já é ator");
                return default;
            }

            return await _actorRepository.GetByIdAsync(id);
        }
        public async Task<Actor> Update(ActorInputUpdate actorInput)
        {
            var actor = new Actor(actorInput.Id, actorInput.Bio, actorInput.Fee);

            var id =  await _actorRepository.Update(actor);

            return await _actorRepository.GetByIdAsync(id);
        }

        public void Delete(int id)
        {
            _actorRepository.Delete(id);
        }

    }
}
