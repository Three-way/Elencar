using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class UserReservationController : BaseController
    {
        private readonly IUserReservationAppService _userReservationAppService;
        public UserReservationController(INotificationHandler<DomainNotification> notification,
            IUserReservationAppService userReservationAppService) : base(notification)
        {
            _userReservationAppService = userReservationAppService;
        }

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetActorReservations([FromRoute] int userId)
        {
            return Ok(await _userReservationAppService.GetReservationsByUserIdAsync(userId).ConfigureAwait(false));
        }
    }
}
