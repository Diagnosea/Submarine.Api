using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Abstractions.Interchange.Authentication;
using Diagnosea.Submarine.Api.Abstractions.Extensions.Authentication;
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

        [HttpPost(RouteConstants.Authentication.Authenticate)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequest request, CancellationToken cancellationToken)
        {
            var authenticated = await _authenticationInstructor.AuthenticateAsync(request.ToDto(), cancellationToken);
            return Ok(authenticated.ToResponse());
        } 
    }
}