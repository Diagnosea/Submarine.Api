using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText
{
    public class CompareHashTextQueryHandler : IRequestHandler<CompareHashTextQuery, bool>
    {
        public Task<bool> Handle(CompareHashTextQuery request, CancellationToken cancellationToken)
        {
            var isValidHash = BCrypt.Net.BCrypt.Verify(request.Text, request.Hash);
            return Task.FromResult(isValidHash);
        }
    }
}