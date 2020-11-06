using System;

namespace Diagnosea.Submarine.Domain.Supply.Entities
{
    public class SupplyUsageEntity
    {
        public DateTime Used { get; set; }
        public int Measurement { get; set; }
    }
}