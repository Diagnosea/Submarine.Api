using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.ValidateAudience
{
    public class ValidateAudienceQuery : IRequest<bool>
    {
        public string AudienceId { get; set; }
    }
}