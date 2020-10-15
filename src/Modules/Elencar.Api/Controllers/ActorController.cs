using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elencar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorAppService _actorAppService;

        public ActorController(IActorAppService actorAppService)
        {
            _actorAppService = actorAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ActorInput actorInput)
        {
            var item = await _actorAppService
                                    .Insert(actorInput)
                                    .ConfigureAwait(false);
            if (item == null)
            {
                return BadRequest("Actor Invalid");
            }
            return Created("Actor Created",item);
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
