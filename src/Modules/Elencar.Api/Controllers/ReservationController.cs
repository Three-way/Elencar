using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Input.ObjectValue;
using Elencar.Application.AppElencar.Interfaces;
using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elencar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : BaseController
    {
        private readonly IReservationAppService _reservationAppService;
        public ReservationController(INotificationHandler<DomainNotification> notification,
            IReservationAppService reservationAppService) : base(notification)
        {
            _reservationAppService = reservationAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] ReservationInput reservationInput)
        {
            var item = await _reservationAppService
                                    .Insert(reservationInput)
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
            return Ok(await _reservationAppService.Get());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            return Ok(await _reservationAppService.GetByIdAsync(id));
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] ReservationInputUpdate reservationInputUpdate)
        {
            return Ok(await _reservationAppService.Update(reservationInputUpdate));
        }



        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete([FromRoute] int id)
        {
            _reservationAppService.Delete(id);
            return NoContent();

        }

    }
}
