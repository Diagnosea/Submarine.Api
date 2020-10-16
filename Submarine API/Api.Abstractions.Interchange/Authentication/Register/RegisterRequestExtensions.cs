using Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Register
{
    public static class RegisterRequestExtensions
    {
        public static RegisterDto ToDto(this RegisterRequest register)
        {
            return new RegisterDto
            {
                EmailAddress = register.EmailAddress,
                PlainTextPassword = register.Password
            };
        }
    }
}