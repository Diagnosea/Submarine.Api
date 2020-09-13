using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.License.Extensions;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.License.Commands.InsertLicense
{
    public class InsertLicenseCommandHandler : IRequestHandler<InsertLicenseCommand>
    {
        private readonly IMongoCollection<LicenseEntity> _licenseCollection;

        public InsertLicenseCommandHandler(IMongoDatabase database)
        {
            _licenseCollection = database.GetEntityCollection<LicenseEntity>();
        }

        public async Task<Unit> Handle(InsertLicenseCommand request, CancellationToken cancellationToken)
        {
            await _licenseCollection.InsertOneAsync(request.ToEntity(), null, cancellationToken);
            return new Unit();
        }
    }
}