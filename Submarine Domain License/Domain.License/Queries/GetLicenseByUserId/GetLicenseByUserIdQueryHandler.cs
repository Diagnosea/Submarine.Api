using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.License.Entities;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.License.Queries.GetLicenseByUserId
{
    public class GetLicenseByUserIdQueryHandler : IRequestHandler<GetLicenseByUserIdQuery, LicenseEntity>
    {
        private readonly IMongoCollection<LicenseEntity> _licenseCollection;

        public GetLicenseByUserIdQueryHandler(IMongoDatabase database)
        {
            _licenseCollection = database.GetEntityCollection<LicenseEntity>();
        }
        
        public async Task<LicenseEntity> Handle(GetLicenseByUserIdQuery request, CancellationToken cancellationToken)
        {
            var filter = new FilterDefinitionBuilder<LicenseEntity>()
                .Eq(x => x.UserId, request.UserId);

            var projection = new ProjectionDefinitionBuilder<LicenseEntity>()
                .Include(x => x.Id)
                .Include(x => x.UserId);

            return await _licenseCollection
                .Find(filter)
                .Project<LicenseEntity>(projection)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}