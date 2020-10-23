using System;
using System.Linq;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.License;
using Diagnosea.Submarine.Domain.License.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.License.CreateLicense
{
    public static class CreateLicenseRequestExtensions
    {
        public static CreateLicenseDto ToDto(this CreateLicenseRequest createLicense)
        {
            if (!createLicense.UserId.HasValue)
            {
                // TODO HOW HAS IT GOT HERE? IS THERE SOMETHING BETTER I CAN DO HERE.
                throw new Exception("Oh no");
            }

            return new CreateLicenseDto
            {
                UserId = createLicense.UserId.Value,
                Products = createLicense.Products
                    .Select(x => x.ToDto())
                    .ToList()
            };
        }
    }
}