using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Water;

namespace Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank
{
    public class TankWaterResponse
    {
        public Guid WaterId { get; set; }
        public WaterCycleStage Stage { get; set; }
        public IList<TankWaterLevelResponse> Levels { get; set; }
    }
}