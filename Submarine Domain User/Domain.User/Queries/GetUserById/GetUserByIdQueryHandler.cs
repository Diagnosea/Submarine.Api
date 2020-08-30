using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.User.Entities;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserEntity>
    {
        private readonly IMongoCollection<UserEntity> _userCollection;
        
        public GetUserByIdQueryHandler(IMongoDatabase mongoDatabase)
        {
            _userCollection = mongoDatabase.GetEntityCollection<UserEntity>();
        }
        
        public async Task<UserEntity> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var projection = new ProjectionDefinitionBuilder<UserEntity>()
                .Include(x => x.Id)
                .Include(x => x.EmailAddress);

            return await _userCollection
                .Find(x => x.Id == request.Id)
                .Project<UserEntity>(projection)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}