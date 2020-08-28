using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries
{
    public class GenerateJwtQuery : IRequest<string>
    {
        public string PrivateSigningKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}