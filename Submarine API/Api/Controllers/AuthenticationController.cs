using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Authenticate;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Register;
using Diagnosea.Submarine.Domain.Instructors.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diagnosea.Submarine.Api.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/" + RouteConstants.Authentication.Base)]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationInstructor _authenticationInstructor;

        public AuthenticationController(IAuthenticationInstructor authenticationInstructor)
        {
            _authenticationInstructor = authenticationInstructor;
        }

        [HttpPost(RouteConstants.Authentication.Register)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken token)
        {
            var registered = await _authenticationInstructor.RegisterAsync(request.ToDto(), token);
            return CreatedUser(RouteConstants.Version1, registered.UserId, registered);
        }
        

        [HttpPost(RouteConstants.Authentication.Authenticate)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequest request, CancellationToken cancellationToken)
        {
            var authenticated = await _authenticationInstructor.AuthenticateAsync(request.ToDto(), cancellationToken);
            return Ok(authenticated.ToResponse());
        }
        
        public CreatedAtActionResult CreatedUser(string version, Guid userId, object value)
            => CreatedAtAction("GetUserAsync", "User", new {version, userId}, value);
    }
}