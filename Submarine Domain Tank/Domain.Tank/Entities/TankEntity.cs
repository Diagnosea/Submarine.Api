using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Domain.Tank.Entities.TankSupply;
using Diagnosea.Submarine.Domain.Tank.Entities.TankWater;

namespace Diagnosea.Submarine.Domain.Tank.Entities
{
    public class TankEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public TankWaterStub Water { get; set; }
        public IList<TankLivestockStub> Livestock { get; set; }
        public IList<TankSupplyStub> Supplies { get; set; }
    }
}