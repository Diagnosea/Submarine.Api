using System;
using System.Collections.Generic;
using Domain.Tank.Entities.TankSupply;
using Domain.Tank.Entities.TankWater;

namespace Domain.Tank.Entities
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