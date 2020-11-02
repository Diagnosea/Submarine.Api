using System;

namespace Diagnosea.Submarine.Domain.Supply.Entities
{
    public class SupplyCompatibilityEntity
    {
        public Guid Id { get; set; }
        public Guid SupplyId { get; set; }
        public string Name { get; set; }
    }
}