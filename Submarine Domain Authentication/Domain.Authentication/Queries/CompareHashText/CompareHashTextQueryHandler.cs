using System.Threading;
using System.Threading.Tasks;
using BCrypt.Net;
using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText
{
    public class CompareHashTextQueryHandler : IRequestHandler<CompareHashTextQuery, bool>
    {
        public Task<bool> Handle(CompareHashTextQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var isValidHash = BCrypt.Net.BCrypt.Verify(request.Text, request.Hash);
                return Task.FromResult(isValidHash);
            }
            catch (SaltParseException)
            {
                return Task.FromResult(false);
            }
        }
    }
}