using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.Authentication.Entities;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.ValidateAudience
{
    public class ValidateAudienceQueryHandler : IRequestHandler<ValidateAudienceQuery, bool>
    {
        private readonly IMongoCollection<AudienceEntity> _audienceCollection;

        public ValidateAudienceQueryHandler(IMongoDatabase database)
        {
            _audienceCollection = database.GetEntityCollection<AudienceEntity>();
        }
        
        public async Task<bool> Handle(ValidateAudienceQuery request, CancellationToken cancellationToken)
        {
            var audience = await _audienceCollection
                .Find(x => x.Id == request.AudienceId)
                .FirstOrDefaultAsync(cancellationToken);

            return audience != null;
        }
    }
}