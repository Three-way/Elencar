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
    public class ProducerController : ControllerBase
    {
        private readonly IProducerAppService _producerAppService;

        public ProducerController(IProducerAppService producerAppService)
        {
            _producerAppService = producerAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProducerInput producerInput)
        {
            var item = await _producerAppService
                                    .Insert(producerInput)
                                    .ConfigureAwait(false);
            if (item == null)
            {
                return BadRequest("Producer Invalid");
            }
            return Created("Producer Created",item);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_producerAppService.Get().ToList());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _producerAppService.GetbyIdAsync(id).ConfigureAwait(false));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            _producerAppService.Delete(id);
            return NoContent();
        }
    }
}
