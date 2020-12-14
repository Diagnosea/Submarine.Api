using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Api.Abstractions.Attributes;
using Diagnosea.Submarine.Api.Abstractions.Authentication.Attributes;
using Diagnosea.Submarine.Api.Abstractions.Authentication.Extensions;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Tank;
using Diagnosea.Submarine.Domain.Instructors.Tank;
using Microsoft.AspNetCore.Mvc;

namespace Diagnosea.Submarine.Api.Controllers
{
    [ApiController]
    [SubmarineAuthorize(UserRole.Standard)]
    [DiagnoseaRoute(RouteConstants.Tank.Base)]
    public class TankController : ControllerBase
    {
        private readonly ITankInstructor _tankInstructor;

        public TankController(ITankInstructor tankInstructor)
        {
            _tankInstructor = tankInstructor;
        }

        [HttpGet(RouteConstants.Tank.Me)]
        public async Task<IActionResult> GetTankByUserId(CancellationToken token)
        {
            var tanks = await _tankInstructor.GetByUserIdAsync(User.GetId(), token);
            return Ok(tanks.Select(tank => tank.ToResponse()));
        }
    }
}