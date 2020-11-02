using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedTolerance
{
    public class LivestockBreedCompositeToleranceEntity : LivestockBreedToleranceEntity
    {
        public Composite Type { get; set; }
        public int Measurement { get; set; }
        public Metric Metric { get; set; }
    }
}