using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.User;
using Diagnosea.Submarine.Abstractions.Interchange.Responses;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.User;
using Diagnosea.Submarine.Api.Abstractions.Authentication.Attributes;
using Diagnosea.Submarine.Api.Abstractions.Interchange.User;
using Diagnosea.Submarine.Domain.Instructors.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        
        /// <summary>
        /// Get a user by their ID.
        /// </summary>
        [ActionName(nameof(GetUserAsync))]
        [HttpGet("{userId}")]
        [SubmarineAuthorize(UserRole.Administrator)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponse))]
        public async Task<IActionResult> GetUserAsync([FromRoute] GetUserRequest request, CancellationToken token)
        {
            var user = await _userInstructor.GetAsync(request.UserId, token);
            return Ok(user.ToResponse());
        }
    }
}