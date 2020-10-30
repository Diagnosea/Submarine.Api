using Diagnosea.Submarine.Domain.License.Dtos;
using Diagnosea.Submarine.Domain.License.Entities;

namespace Diagnosea.Submarine.Domain.License.Extensions
{
    public static class LicenseProductEntityExtensions
    {
        public static LicenseProductDto ToDto(this LicenseProductEntity licenseProduct)
        {
            return new LicenseProductDto
            {
                Name = licenseProduct.Name,
                Expiration = licenseProduct.Expiration
            };
        }
    }
}