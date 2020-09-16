using Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Authenticate
{
    public static class AuthenticateRequestExtensions
    {
        public static AuthenticateDto ToDto(this AuthenticateRequest request)
        {
            return new AuthenticateDto
            {
                EmailAddress = request.EmailAddress,
                PlainTextPassword = request.Password
            };
        }
    }
}