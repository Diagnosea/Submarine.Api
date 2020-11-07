using System;
using Diagnosea.Submarine.Abstractions.Enums.Water;

namespace Domain.Water.Entities
{
    public class WaterCycleEntity
    {
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
        public WaterCycleStage Stage { get; set; }
    }
}