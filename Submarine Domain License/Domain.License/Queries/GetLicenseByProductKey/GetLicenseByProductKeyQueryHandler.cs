using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.License.Entities;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.License.Queries.GetLicenseByProductKey
{
    public class GetLicenseByProductKeyQueryHandler : IRequestHandler<GetLicenseByProductKeyQuery, LicenseEntity>
    {
        private readonly IMongoCollection<LicenseEntity> _licenseCollection;

        public GetLicenseByProductKeyQueryHandler(IMongoDatabase database)
        {
            _licenseCollection = database.GetEntityCollection<LicenseEntity>();
        }
        
        public async Task<LicenseEntity> Handle(GetLicenseByProductKeyQuery request, CancellationToken cancellationToken)
        {
            var filter = new FilterDefinitionBuilder<LicenseEntity>()
                .ElemMatch(x => x.Products, product => product.Key == request.ProductKey);

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