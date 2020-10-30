using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.License.Entities;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.License.Queries.GetLicenseById
{
    public class GetLicenseByIdQueryHandler : IRequestHandler<GetLicenseByIdQuery, LicenseEntity>
    {
        private readonly IMongoCollection<LicenseEntity> _licenseCollection;

        public GetLicenseByIdQueryHandler(IMongoDatabase database)
        {
            _licenseCollection = database.GetEntityCollection<LicenseEntity>();
        }
        
        public async Task<LicenseEntity> Handle(GetLicenseByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = new FilterDefinitionBuilder<LicenseEntity>()
                .Eq(x => x.Id, request.LicenseId);

            var projection = new ProjectionDefinitionBuilder<LicenseEntity>()
                .Include(x => x.Id)
                .Include(x => x.UserId)
                .Include(x => x.Products);
            
            return await _licenseCollection
                .Find(filter)
                .Project<LicenseEntity>(projection)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}