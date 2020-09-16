using Diagnosea.Submarine.Abstractions.Interchange.Responses.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Authenticate
{
    public static class AuthenticatedDtoExtensions
    {
        public static AuthenticatedResponse ToResponse(this AuthenticatedDto authenticated)
        {
            return new AuthenticatedResponse
            {
                BearerToken = authenticated.BearerToken
            };
        }
    }
}