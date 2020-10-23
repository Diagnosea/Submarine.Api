using Diagnosea.Submarine.Abstractions.Interchange.Responses.License;
using Diagnosea.Submarine.Domain.License.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.License.CreateLicense
{
    public static class CreatedLicenseDtoExtensions
    {
        public static CreatedLicenseResponse ToResponse(this CreatedLicenseDto createdLicense)
        {
            return new CreatedLicenseResponse
            {
                LicenseId = createdLicense.LicenseId
            };
        }
    }
}