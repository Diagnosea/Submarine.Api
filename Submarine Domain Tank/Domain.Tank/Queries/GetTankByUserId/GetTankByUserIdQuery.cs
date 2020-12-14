using System;
using Diagnosea.Submarine.Domain.Tank.Entities;
using MediatR;

namespace Diagnosea.Submarine.Domain.Tank.Queries.GetTankByUserId
{
    public class GetTankByUserIdQuery : IRequest<TankEntity>
    {
        public Guid UserId { get; set; }
    }
}