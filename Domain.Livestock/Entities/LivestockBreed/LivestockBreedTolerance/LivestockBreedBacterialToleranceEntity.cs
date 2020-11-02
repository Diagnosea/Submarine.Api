using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedTolerance
{
    public class LivestockBreedBacterialToleranceEntity : LivestockBreedToleranceEntity
    {
        public Bacteria Bacteria { get; set; }
        public int Measurement { get; set; }
        public Metric Metric { get; set; }
    }
}