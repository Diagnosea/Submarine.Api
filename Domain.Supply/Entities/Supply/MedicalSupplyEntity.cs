using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Supply;

namespace Diagnosea.Submarine.Domain.Supply.Entities.Supply
{
    public class MedicalSupplyEntity : SupplyEntity
    {
        public IList<Ailment> Treats { get; set; }

        public MedicalSupplyEntity()
        {
            Category = SupplyCategory.Medical;
        }
    }
}