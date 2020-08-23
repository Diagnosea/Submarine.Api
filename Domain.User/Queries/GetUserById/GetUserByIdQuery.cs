using System;
using Diagnosea.Submarine.Domain.User.Entities;
using MediatR;

namespace Diagnosea.Submarine.Domain.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserEntity>
    {
        public Guid Id { get; set; }
    }
}