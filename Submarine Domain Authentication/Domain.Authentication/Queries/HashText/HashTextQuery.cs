using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.HashText
{
    public class HashTextQuery : IRequest<string>
    {
        public string Text { get; set; }
    }
}