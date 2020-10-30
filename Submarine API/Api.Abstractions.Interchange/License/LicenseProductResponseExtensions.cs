using Diagnosea.Submarine.Abstractions.Interchange.Responses.License;
using Diagnosea.Submarine.Domain.License.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.License
{
    public static class LicenseProductResponseExtensions
    {
        public static LicenseProductResponse ToResponse(this LicenseProductDto licenseProduct)
        {
            return new LicenseProductResponse
            {
                Name = licenseProduct.Name,
                Expiration = licenseProduct.Expiration
            };
        }
    }
}