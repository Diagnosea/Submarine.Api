using System.Collections.Generic;
using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken
{
    public class GenerateBearerTokenQuery : IRequest<string>
    {
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Audience { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}