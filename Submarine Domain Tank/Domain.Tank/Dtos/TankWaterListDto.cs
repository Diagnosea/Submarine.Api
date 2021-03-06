﻿using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Water;

namespace Diagnosea.Submarine.Domain.Tank.Dtos
{
    public class TankWaterListDto
    {
        public Guid WaterId { get; set; }
        public WaterCycleStage Stage { get; set; }
        public IList<TankWaterLevelListDto> Levels { get; set; }
    }
}