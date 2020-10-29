using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.License;
using Diagnosea.Submarine.Abstractions.Interchange.Responses;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.License;
using Diagnosea.Submarine.Api.Abstractions.Attributes;
using Diagnosea.Submarine.Api.Abstractions.Authentication.Attributes;
using Diagnosea.Submarine.Api.Abstractions.Interchange.License.CreateLicense;
using Diagnosea.Submarine.Api.Abstractions.Swagger.Examples;
using Diagnosea.Submarine.Domain.Instructors.License;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Diagnosea.Submarine.Api.Controllers
{
    [ApiController]
    [SubmarineAuthorize(UserRole.Licenser)]
    [DiagnoseaRoute(RouteConstants.License.Base)]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseInstructor _licenseInstructor;

        public LicenseController(ILicenseInstructor licenseInstructor)
        {
            _licenseInstructor = licenseInstructor;
        }

        [HttpGet]
        [ActionName(nameof(GetLicenseAsync))]
        public IActionResult GetLicenseAsync([FromRoute] Guid licenseId)
        {
            throw new SubmarineException(
                SubmarineExceptionCode.Unknown, 
                $"This endpoint is not usable. You gave License ID: '{licenseId}'");
        }
        
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(CreatedLicenseResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ExceptionResponse))]
        [SwaggerRequestExample(typeof(CreateLicenseRequest), typeof(CreateLicenseRequestExamplesProvider))]
        public async Task<IActionResult> CreateLicenseAsync(
            [FromBody] CreateLicenseRequest createLicense, ApiVersion version, CancellationToken token)
        {
            var createdLicense = await _licenseInstructor.CreateAsync(createLicense.ToDto(), token);

            var routeValues = new
            {
                version = version.ToString(),
                licenseId = createdLicense.LicenseId
            };

            return CreatedAtAction(nameof(GetLicenseAsync), routeValues, createdLicense);
        }
    }
}