using System;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.License;
using Diagnosea.Submarine.Domain.License.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.License.CreateLicense
{
    public static class CreateLicenseProductRequestExtensions
    {
        public static CreateLicenseProductDto ToDto(this CreateLicenseProductRequest createLicenseProduct)
        {
            if (!createLicenseProduct.Expiration.HasValue)
            {
                // TODO HOW HAS IT GOT HERE? IS THERE SOMETHING BETTER I CAN DO HERE.
                throw new Exception("Oh no");
            }
            
            return new CreateLicenseProductDto
            {
                Name = createLicenseProduct.Name,
                Expiration = createLicenseProduct.Expiration.Value
            };
        }
    }
}