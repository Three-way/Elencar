using System.Threading.Tasks;
using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Interfaces;
using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elencar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleAppService _roleAppService;

        public RoleController(MediatR.INotificationHandler<DomainNotification> notification, 
            IRoleAppService roleAppService)
            : base(notification)
        {
            _roleAppService = roleAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] RoleInput roleInput)
        {
            var item = await _roleAppService
                                    .Insert(roleInput)
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
            return Ok(await _roleAppService.Get());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _roleAppService.GetByIdAsync(id).ConfigureAwait(false));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete([FromRoute] int id)
        {
           _roleAppService.Delete(id);
            return NoContent();

        }
    }
}
