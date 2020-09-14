using Diagnosea.Submarine.Domain.License.Entities;
using MediatR;

namespace Diagnosea.Submarine.Domain.License.Queries.GetLicenseByProductKey
{
    public class GetLicenseByProductKeyQuery : IRequest<LicenseEntity>
    {
        public string ProductKey { get; set; }
    }
}