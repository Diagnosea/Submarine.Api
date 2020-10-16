using Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication;
using Swashbuckle.AspNetCore.Filters;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.Examples
{
    public class RegisterRequestExamplesProvider : IExamplesProvider<RegisterRequest>
    {
        public RegisterRequest GetExamples()
        {
            return new RegisterRequest
            {
                EmailAddress = "example.user@example.com",
                Password = "A strong password"
            };
        }
    }
}