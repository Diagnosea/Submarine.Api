using System.Threading.Tasks;
using Diagnosea.Submarine.Api.Models.Request;
using Diagnosea.Submarine.Domain.Authentication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diagnosea.Submarine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequestModel authenticationRequestModel)
        {
            var jwt = await _mediator.Send(new GenerateJwtQuery
            {
                PrivateSigningKey = authenticationRequestModel.PrivateSigningKey,
                Audience = authenticationRequestModel.Audience,
                Issuer = authenticationRequestModel.Issuer
            }, HttpContext.RequestAborted);

            return Ok(jwt);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}