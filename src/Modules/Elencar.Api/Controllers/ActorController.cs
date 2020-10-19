using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Input.ObjectValues;
using Elencar.Application.AppElencar.Interfaces;
using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elencar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : BaseController
    {
        private readonly IActorAppService _actorAppService;

        public ActorController(MediatR.INotificationHandler<DomainNotification> notification, 
            IActorAppService actorAppService)
            : base(notification)
        {
            _actorAppService = actorAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] ActorInput actorInput)
        {
            var item = await _actorAppService
                                    .Insert(actorInput)
                                    .ConfigureAwait(false);
            return CreatedContent("", item);
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _actorAppService.Get());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _actorAppService.GetByIdAsync(id).ConfigureAwait(false));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete([FromRoute] int id)
        {
           _actorAppService.Delete(id);
            return NoContent();

        }

        [HttpPut]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] ActorInputUpdate actorInputUpdate)
        {
            return OkOrNotFound(await _actorAppService.Update(actorInputUpdate));
        }
    }
}
