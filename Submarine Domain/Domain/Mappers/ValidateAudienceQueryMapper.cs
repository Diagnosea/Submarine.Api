using Diagnosea.Submarine.Domain.Authentication.Dtos;
using Diagnosea.Submarine.Domain.Authentication.Queries.ValidateAudience;

namespace Diagnosea.Submarine.Domain.Mappers
{
    public static class ValidateAudienceQueryMapper
    {
        public static ValidateAudienceQuery ToValidateAudienceQuery(this AuthenticationDto authentication)
        {
            return new ValidateAudienceQuery
            {
                AudienceId = authentication.AudienceId
            };
        }
    }
}