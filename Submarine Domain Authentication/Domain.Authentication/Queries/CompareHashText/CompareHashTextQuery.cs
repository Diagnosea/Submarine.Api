using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText
{
    public class CompareHashTextQuery : IRequest<bool>
    {
        public string Text { get; set; }
        
        public string Hash { get; set; }
    }
}