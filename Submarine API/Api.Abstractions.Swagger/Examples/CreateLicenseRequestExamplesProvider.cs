using System;
using System.Collections.Generic;
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
                UserId = Guid.NewGuid(),
                Products = new List<CreateLicenseProductRequest>
                {
                    new CreateLicenseProductRequest
                    {
                        Name = "Submarine",
                        Expiration = DateTime.UtcNow.AddYears(1)
                    },
                    new CreateLicenseProductRequest
                    {
                        Name = "Sonar",
                        Expiration = DateTime.UtcNow.AddMonths(1)
                    }
                }
            };
        }
    }
}