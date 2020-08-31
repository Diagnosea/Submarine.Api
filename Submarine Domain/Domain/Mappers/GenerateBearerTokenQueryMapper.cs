using Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Extensions;

namespace Diagnosea.Submarine.Domain.Mappers
{
    public static class GenerateBearerTokenQueryMapper
    {
        public static GenerateBearerTokenQuery ToGenerateBearerTokenQuery(this UserEntity user, string audienceId)
        {
            return new GenerateBearerTokenQuery
            {
                AudienceId = audienceId,
                Subject = user.Id.ToString(),
                Name = user.UserName,
                Roles = user.Roles.ToPlainText()
            };
        }
    }
}