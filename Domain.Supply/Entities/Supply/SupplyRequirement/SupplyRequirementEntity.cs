using System;
using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Supply.Entities.Supply.SupplyRequirement
{
    public class SupplyRequirementEntity
    {
        public Guid Id { get; set; }
        public Guid SupplyId { get; set; }
        public int Measurement { get; set; }
        public Metric Metric { get; set; }
    }
}