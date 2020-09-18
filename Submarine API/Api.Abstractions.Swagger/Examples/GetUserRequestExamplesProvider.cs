using System;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.User;
using Swashbuckle.AspNetCore.Filters;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.Examples
{
    public class GetUserRequestExamplesProvider : IExamplesProvider<GetUserRequest>
    {
        public GetUserRequest GetExamples()
        {
            return new GetUserRequest
            {
                UserId = Guid.NewGuid(),
            };
        }
    }
}