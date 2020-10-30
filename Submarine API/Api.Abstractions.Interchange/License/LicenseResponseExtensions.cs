using System.Linq;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.License;
using Diagnosea.Submarine.Domain.License.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.License
{
    public static class LicenseResponseExtensions
    {
        public static LicenseResponse ToResponse(this LicenseDto license)
        {
            return new LicenseResponse
            {
                Id = license.Id,
                Products = license.Products.Select(x => x.ToResponse()).ToList()
            };
        }
    }
}