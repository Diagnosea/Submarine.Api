using Abstractions.Exceptions;
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
                throw new SubmarineMappingException(
                    $"No Expiration Value for Licensed Product '{createLicenseProduct.Name}'",
                    MappingExceptionMessages.Failed);
            }
            
            return new CreateLicenseProductDto
            {
                Name = createLicenseProduct.Name,
                Expiration = createLicenseProduct.Expiration.Value
            };
        }
    }
}