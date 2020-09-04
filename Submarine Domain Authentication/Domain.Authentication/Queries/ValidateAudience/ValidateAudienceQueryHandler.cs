using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.Authentication.Entities;
using MediatR;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.ValidateAudience
{
    // Will in future be swapped out for licensing.
    public class ValidateAudienceQueryHandler : IRequestHandler<ValidateAudienceQuery, bool>
    {
        private readonly IMongoCollection<AudienceEntity> _audienceCollection;

        public ValidateAudienceQueryHandler(IMongoDatabase database)
        {
            _audienceCollection = database.GetEntityCollection<AudienceEntity>();
        }
        
        public async Task<bool> Handle(ValidateAudienceQuery request, CancellationToken cancellationToken)
        {
            var filterDefinitionBuilder = new FilterDefinitionBuilder<AudienceEntity>();
            var filter = filterDefinitionBuilder.Eq(x => x.Id, request.AudienceId);
            
            var audience = await _audienceCollection
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);

            return audience != null;
        }
    }
}