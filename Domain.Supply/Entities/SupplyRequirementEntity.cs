using System;

namespace Diagnosea.Submarine.Domain.Supply.Entities
{
    public class SupplyRequirementEntity
    {
        public Guid Id { get; set; }
        public Guid SupplyId { get; set; }
        public string Name { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
    }
}