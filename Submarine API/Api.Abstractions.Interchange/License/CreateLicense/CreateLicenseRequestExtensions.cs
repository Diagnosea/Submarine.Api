using Abstractions.Exceptions;
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
                throw new MappingException(
                    $"No UserId Value for License",
                    ExceptionMessages.Mapping.Failed);
            }

            return new CreateLicenseDto
            {
                UserId = createLicense.UserId.Value
            };
        }
    }
}