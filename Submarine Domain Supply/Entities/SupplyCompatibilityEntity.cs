using System;

namespace Diagnosea.Submarine.Domain.Supply.Entities
{
    public class SupplyCompatibilityEntity
    {
        public Guid Id { get; set; }
        public Guid SupplyId { get; set; }
        public string Description { get; set; }
        public int Measurement { get; set; }
    }
}