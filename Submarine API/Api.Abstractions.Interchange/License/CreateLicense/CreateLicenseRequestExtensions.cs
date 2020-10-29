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
                throw new SubmarineMappingException(
                    $"No UserId Value for License",
                    MappingExceptionMessages.Failed);
            }

            return new CreateLicenseDto
            {
                UserId = createLicense.UserId.Value
            };
        }
    }
}