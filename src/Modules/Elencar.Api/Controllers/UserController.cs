using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elencar.Application.AppElencar.Input;
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
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public UserController(INotificationHandler<DomainNotification> notification,IUserAppService userAppService)
            : base(notification)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] UserInput userInput)
        {
            var item = await _userAppService
                                    .Insert(userInput)
                                    .ConfigureAwait(false);
            return CreatedContent("", item);
        }

        [HttpGet] 
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            return OkOrNoContent(_userAppService.Get());
        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
        {
            return OkOrNotFound(await _userAppService
                                        .GetByIdAsync(id)
                                        .ConfigureAwait(false));
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] UserInput userInput)
        {
            return OkOrNotFound(await _userAppService.Update(userInput));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete([FromRoute] int id)
        {
            _userAppService.Delete(id);
            return NoContent();

        }
    }
}
