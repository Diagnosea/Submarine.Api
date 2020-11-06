using System;
using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Supply.Entities.Supply.SupplyCompatibility
{
    public class SupplyCompatibilityEntity
    {
        public Guid Id { get; set; }
        public Guid SupplyId { get; set; }
        public string Description { get; set; }
        public int Measurement { get; set; }
        public Metric Metric { get; set; }
    }
}