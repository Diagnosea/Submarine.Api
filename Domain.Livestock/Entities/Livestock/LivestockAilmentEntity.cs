using System;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.Livestock
{
    public class LivestockAilmentEntity
    {
        public Ailment Ailment { get; set; }
        public DateTime Diagnosed { get; set; }
        public LivestockAilmentProgression Progression { get; set; }
        public DateTime? Recovered { get; set; }
    }
}