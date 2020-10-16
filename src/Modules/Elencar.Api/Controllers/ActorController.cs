using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elencar.Application.AppElencar.Input;
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
        public async Task<IActionResult> Post([FromBody] ActorInput actorInput)
        {
            //var hasAccount = await _actorAppService.HasActor(actorInput.Email);
            //if (hasAccount)
            //{
            //    return Conflict("Actor already enrolled");
            //}
            var item = await _actorAppService
                                    .Insert(actorInput)
                                    .ConfigureAwait(false);
            //if (item == null)
            //{
            //    return BadRequest("Actor Invalid");
            //}
            return CreatedContent("", item);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_actorAppService.Get());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _actorAppService.GetByIdAsync(id).ConfigureAwait(false));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            _actorAppService.Delete(id);
            return NoContent();
        }
    }
}
