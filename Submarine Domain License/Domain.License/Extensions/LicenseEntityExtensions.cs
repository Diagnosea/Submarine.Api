using System.Linq;
using Diagnosea.Submarine.Domain.License.Dtos;
using Diagnosea.Submarine.Domain.License.Entities;

namespace Diagnosea.Submarine.Domain.License.Extensions
{
    public static class LicenseEntityExtensions
    {
        public static LicenseDto ToDto(this LicenseEntity license)
        {
            return new LicenseDto
            {
                Id = license.Id,
                Products = license.Products.Select(x => x.ToDto()).ToList()
            };
        }
    }
}