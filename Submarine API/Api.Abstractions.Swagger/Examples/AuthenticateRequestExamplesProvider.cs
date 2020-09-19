using Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication;
using Swashbuckle.AspNetCore.Filters;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.Examples
{
    public class AuthenticateRequestExamplesProvider : IExamplesProvider<AuthenticateRequest>
    {
        public AuthenticateRequest GetExamples()
        {
            return new AuthenticateRequest
            {
                EmailAddress = "example.user@example.com",
                Password = "your password"
            };
        }
    }
}