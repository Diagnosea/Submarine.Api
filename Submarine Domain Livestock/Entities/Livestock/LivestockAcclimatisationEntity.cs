using System;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.Livestock
{
    public abstract class LivestockAcclimatisationEntity
    {
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
    }
}