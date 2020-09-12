using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Api.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.Instructors.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diagnosea.Submarine.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/" + RouteConstants.User.Base)]
    public class UserController : ControllerBase
    {
        private readonly IUserInstructor _userInstructor;

        public UserController(IUserInstructor userInstructor)
        {
            _userInstructor = userInstructor;
        }
        
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAsync([FromRoute] Guid userId, CancellationToken token)
        {
            var user = await _userInstructor.GetAsync(userId, token);
            return Ok(user.ToResponse());
        }
    }
}