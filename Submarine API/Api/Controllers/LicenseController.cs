using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Domain.Instructors.License;
using Microsoft.AspNetCore.Mvc;

namespace Diagnosea.Submarine.Api.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/" + RouteConstants.License.Base)]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseInstructor _licenseInstructor;

        public LicenseController(ILicenseInstructor licenseInstructor)
        {
            _licenseInstructor = licenseInstructor;
        }
    }
}