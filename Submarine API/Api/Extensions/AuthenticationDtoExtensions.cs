using Abstractions.Traffic.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Dtos;

namespace Diagnosea.Submarine.Api.Extensions
{
    public static class AuthenticationDtoExtensions
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