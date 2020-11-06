using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Supply;
using Diagnosea.Submarine.Domain.Supply.Entities.Supply.SupplyCompatibility;
using Diagnosea.Submarine.Domain.Supply.Entities.Supply.SupplyDurability;
using Diagnosea.Submarine.Domain.Supply.Entities.Supply.SupplyQuantity;
using Diagnosea.Submarine.Domain.Supply.Entities.Supply.SupplyRequirement;

namespace Diagnosea.Submarine.Domain.Supply.Entities.Supply
{
    public class SupplyEntity
    {
        public Guid Id { get; set; }
        public Guid SupplyStoreId { get; set; }
        public string Name { get; set; }
        public SupplyCategory Category { get; set; }
        public SupplyDurabilityEntity Durability { get; set; }
        public SupplyQuantityEntity Owned { get; set; }
        public IList<SupplyCompatibilityEntity> Compatibilities { get; set; }
        public IList<SupplyRequirementEntity> Requirements { get; set; }
    }
}