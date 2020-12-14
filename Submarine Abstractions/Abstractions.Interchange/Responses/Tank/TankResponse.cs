using System;
using System.Collections.Generic;

namespace Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank
{
    public class TankResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TankWaterResponse Water { get; set; }
        public IList<TankLivestockResponse> Livestock { get; set; }
        public IList<TankSupplyResponse> Supplies { get; set; }
    }
}