using System;
using System.Collections.Generic;
using Domain.Water.Entities.WaterLevel;

namespace Domain.Water.Entities
{
    public class WaterEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public IList<WaterCycleEntity> Cycles { get; set; }
        public IList<WaterLevelEntity> Levels { get; set; }
    }
}