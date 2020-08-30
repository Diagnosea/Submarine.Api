using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Authentication.Settings;
using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.HashText
{
    public class HashTextQueryHandler : IRequestHandler<HashTextQuery, string>
    {
        private readonly ISubmarineAuthenticationSettings _submarineAuthenticationSettings;

        public HashTextQueryHandler(ISubmarineAuthenticationSettings submarineAuthenticationSettings)
        {
            _submarineAuthenticationSettings = submarineAuthenticationSettings;
        }

        public Task<string> Handle(HashTextQuery request, CancellationToken cancellationToken)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(_submarineAuthenticationSettings.SaltingRounds);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.PlainTextPassword, salt);

            return Task.FromResult(hashedPassword);
        }
    }
}