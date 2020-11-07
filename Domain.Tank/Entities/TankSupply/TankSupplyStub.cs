using System;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Tank;

namespace Domain.Tank.Entities.TankSupply
{
    public class TankSupplyStub
    {
        public Guid SupplyId { get; set; }
        public TankSupplyComponent Component { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Metric Metric { get; set; }
    }
}