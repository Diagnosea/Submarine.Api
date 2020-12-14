using System;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Tank;

namespace Diagnosea.Submarine.Domain.Tank.Dtos
{
    public class TankSupplyDto
    {
        public Guid SupplyId { get; set; }
        public string Name { get; set; }
        public TankSupplyComponent Component { get; set; }
        public Metric Metric;
        public int Quantity { get; set; }
    }
}