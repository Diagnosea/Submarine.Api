using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Water;

namespace Diagnosea.Submarine.Domain.Tank.Entities.TankWater
{
    public class TankWaterStub
    {
        public Guid Id { get; set; }
        public WaterCycleStage Stage { get; set; }
        public IList<TankWaterLevelStub> Levels { get; set; }
    }
}