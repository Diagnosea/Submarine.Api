using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Domain.Tank.Entities;
using MediatR;

namespace Diagnosea.Submarine.Domain.Tank.Queries.GetTanksByUserId
{
    public class GetTanksByUserIdQuery : IRequest<IList<TankEntity>>
    {
        public Guid UserId { get; set; }
    }
}