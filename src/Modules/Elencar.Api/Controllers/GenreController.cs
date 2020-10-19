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
    public class GenreController : BaseController
    {
        private readonly IGenreAppService _genreAppService;

        public GenreController(MediatR.INotificationHandler<DomainNotification> notification, 
            IGenreAppService genreAppService)
            : base(notification)
        {
            _genreAppService = genreAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] GenreInput genreInput)
        {
            var item = await _genreAppService
                                    .Insert(genreInput)
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
            return Ok(await _genreAppService.Get());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _genreAppService.GetByIdAsync(id).ConfigureAwait(false));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete([FromRoute] int id)
        {
            _genreAppService.Delete(id);
            return NoContent();

        }
    }
}
