using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Api.Abstractions.Authentication.Attributes;
using Diagnosea.Submarine.Api.Abstractions.Interchange.User;
using Diagnosea.Submarine.Domain.Instructors.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diagnosea.Submarine.Api.Controllers
{
    [ApiController]
    [SubmarineAuthorize]
    [Route("v{version:apiVersion}/" + RouteConstants.User.Base)]
    public class UserController : ControllerBase
    {
        private readonly IUserInstructor _userInstructor;

        public UserController(IUserInstructor userInstructor)
        {
            _userInstructor = userInstructor;
        }
        
        [ActionName(nameof(GetUserAsync))]
        [HttpGet("{userId}")]
        [SubmarineAuthorize(UserRole.Administrator)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAsync([FromRoute] Guid userId, CancellationToken token)
        {
            var user = await _userInstructor.GetAsync(userId, token);
            return Ok(user.ToResponse());
        }
    }
}