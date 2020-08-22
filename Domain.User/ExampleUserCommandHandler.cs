using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Diagnosea.Submarine.Domain.User
{
    public class ExampleUserCommand : IRequest
    {
        
    }
    
    public class ExampleUserCommandHandler : IRequestHandler<ExampleUserCommand>
    {
        public Task<Unit> Handle(ExampleUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}