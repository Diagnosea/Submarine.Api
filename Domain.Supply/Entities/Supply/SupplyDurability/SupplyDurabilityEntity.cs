using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Supply.Entities.Supply.SupplyDurability
{
    public class SupplyDurabilityEntity {
        public int Measurement { get; set; }
        public Metric Metric { get; set; }
    }
}