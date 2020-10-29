using System;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.License;
using Swashbuckle.AspNetCore.Filters;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.Examples
{
    public class CreateLicenseRequestExamplesProvider : IExamplesProvider<CreateLicenseRequest>
    {
        public CreateLicenseRequest GetExamples()
        {
            return new CreateLicenseRequest
            {
                UserId = Guid.NewGuid()
            };
        }
    }
}