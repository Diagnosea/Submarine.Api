using System;
using Diagnosea.Submarine.Abstractions.Enums.Supply;

namespace Diagnosea.Submarine.Domain.Supply.Entities.Supply
{
    public class SupplyEntity
    {
        public Guid Id { get; set; }
        public Guid SupplyStoreId { get; set; }
        public string Name { get; set; }
        public SupplyCategory Category { get; set; }
        public int OwnedQuantity { get; set; }
    }
}