using System.Threading;
using System.Threading.Tasks;
using Abstractions.Traffic.Authentication;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Api.Extensions;
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
            var token = await _authenticationInstructor.AuthenticateAsync(request.ToDto(), cancellationToken);
            return Ok(token);
        } 
    }
}