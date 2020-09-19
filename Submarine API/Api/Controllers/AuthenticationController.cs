using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication;
using Diagnosea.Submarine.Abstractions.Interchange.Responses;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.Authentication;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Authenticate;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Register;
using Diagnosea.Submarine.Api.Abstractions.Swagger.Examples;
using Diagnosea.Submarine.Domain.Instructors.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

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
        
        /// <summary>
        /// Register for a new user account.
        /// </summary>
        [ActionName(nameof(RegisterAsync))]
        [HttpPost(RouteConstants.Authentication.Register)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(RegisteredResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [SwaggerRequestExample(typeof(RegisterRequest), typeof(RegisterRequestExamplesProvider))]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, ApiVersion version, CancellationToken token)
        {
            var registered = await _authenticationInstructor.RegisterAsync(request.ToDto(), token);
            
            var routeValues = new
            {
                version = version.ToString(),
                userId = registered.UserId.ToString()
            };
            
            return CreatedAtAction("GetUserAsync", "User", routeValues, registered);
        }
        
        
        /// <summary>
        /// Authenticate for a bearer token to use against other resources.
        /// </summary>
        [ActionName(nameof(AuthenticateAsync))]
        [HttpPost(RouteConstants.Authentication.Authenticate)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthenticatedResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponse))]
        [SwaggerRequestExample(typeof(AuthenticateRequest), typeof(AuthenticateRequestExamplesProvider))]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequest request, CancellationToken cancellationToken)
        {
            var authenticated = await _authenticationInstructor.AuthenticateAsync(request.ToDto(), cancellationToken);
            return Ok(authenticated.ToResponse());
        }
    }
}