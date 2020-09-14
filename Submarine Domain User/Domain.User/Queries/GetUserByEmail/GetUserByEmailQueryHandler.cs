using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.User.Entities;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.User.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserEntity>
    {
        private readonly IMongoCollection<UserEntity> _userCollection;

        public GetUserByEmailQueryHandler(IMongoDatabase mongoDatabase)
        {
            _userCollection = mongoDatabase.GetEntityCollection<UserEntity>();
        }
        
        public async Task<UserEntity> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var projection = new ProjectionDefinitionBuilder<UserEntity>()
                .Include(x => x.Id)
                .Include(x => x.EmailAddress)
                .Include(x => x.Password)
                .Include(x => x.UserName)
                .Include(x => x.Roles);

            return await _userCollection
                .Find(x => x.EmailAddress == request.EmailAddress)
                .Project<UserEntity>(projection)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}