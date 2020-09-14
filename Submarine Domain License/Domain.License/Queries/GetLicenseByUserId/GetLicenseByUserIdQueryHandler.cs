using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.License.Entities;
using MediatR;

namespace Diagnosea.Submarine.Domain.License.Queries.GetLicenseByUserId
{
    public class GetLicenseByUserIdQueryHandler : IRequestHandler<GetLicenseByUserIdQuery, LicenseEntity>
    {
        public Task<LicenseEntity> Handle(GetLicenseByUserIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}