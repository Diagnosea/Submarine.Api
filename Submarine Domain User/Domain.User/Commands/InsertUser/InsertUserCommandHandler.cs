using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Extensions;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.User.Commands.InsertUser
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand>
    {
        private readonly IMongoCollection<UserEntity> _userCollection;

        public InsertUserCommandHandler(IMongoDatabase mongoDatabase)
        {
            _userCollection = mongoDatabase.GetEntityCollection<UserEntity>();
        }

        public async Task<Unit> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            await _userCollection.InsertOneAsync(request.ToEntity(), null, cancellationToken);
            return new Unit();
        }
    }
}