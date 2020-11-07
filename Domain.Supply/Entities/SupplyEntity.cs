using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Supply;

namespace Diagnosea.Submarine.Domain.Supply.Entities
{
    /// <summary>
    /// A unit of supplies.
    /// </summary>
    public class SupplyEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public SupplyCategory Category { get; set; }
        public string Name { get; set; }
        public Metric MeasuredIn { get; set; }
        public int Quantity { get; set; }
        public IList<SupplyUsageEntity> Usages { get; set; }
        public IList<SupplyCompatibilityEntity> Compatibilities { get; set; }
        public IList<SupplyRequirementEntity> Requirements { get; set; }
    }
}