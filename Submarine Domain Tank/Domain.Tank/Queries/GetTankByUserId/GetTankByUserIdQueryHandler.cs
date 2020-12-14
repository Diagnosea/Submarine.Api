using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.Tank.Entities;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.Tank.Queries.GetTankByUserId
{
    public class GetTankByUserIdQueryHandler : IRequestHandler<GetTankByUserIdQuery, TankEntity>
    {
        private readonly IMongoCollection<TankEntity> _tankCollection;

        public GetTankByUserIdQueryHandler(IMongoDatabase database)
        {
            _tankCollection = database.GetEntityCollection<TankEntity>();
        }
        
        public async Task<TankEntity> Handle(GetTankByUserIdQuery request, CancellationToken cancellationToken)
        {
            var filterDefinition = new FilterDefinitionBuilder<TankEntity>()
                .Eq(x => x.UserId, request.UserId);

            return await _tankCollection
                .Find(filterDefinition)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}