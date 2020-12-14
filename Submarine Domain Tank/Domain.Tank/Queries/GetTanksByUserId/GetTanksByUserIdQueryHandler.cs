using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.Tank.Entities;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.Tank.Queries.GetTanksByUserId
{
    public class GetTankByUserIdQueryHandler : IRequestHandler<GetTanksByUserIdQuery, IList<TankEntity>>
    {
        private readonly IMongoCollection<TankEntity> _tankCollection;

        public GetTankByUserIdQueryHandler(IMongoDatabase database)
        {
            _tankCollection = database.GetEntityCollection<TankEntity>();
        }
        
        public async Task<IList<TankEntity>> Handle(GetTanksByUserIdQuery request, CancellationToken cancellationToken)
        {
            var filterDefinition = new FilterDefinitionBuilder<TankEntity>()
                .Eq(x => x.UserId, request.UserId);

            return await _tankCollection
                .Find(filterDefinition)
                .ToListAsync(cancellationToken);
        }
    }
}