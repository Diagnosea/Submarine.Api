using System;
using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.Livestock
{
    public class LivestockFeedEntity
    {
        public Guid SupplyId { get; set; }
        public DateTime Fed { get; set; }
        public Metric Metric { get; set; }
        public int Measurement { get; set; }
    }
}