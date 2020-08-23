using Diagnosea.Submarine.Domain.User.Entities;
using MediatR;

namespace Diagnosea.Submarine.Domain.User.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserEntity>
    {
        public string EmailAddress { get; set; }
    }
}